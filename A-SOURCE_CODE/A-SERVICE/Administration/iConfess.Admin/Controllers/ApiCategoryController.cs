using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Admin.ViewModels.ApiCategory;
using iConfess.Database.Models.Tables;
using log4net;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.Resources;
using Shared.ViewModels.Accounts;
using Shared.ViewModels.Categories;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/category")]
    [ApiAuthorize]
    public class ApiCategoryController : ApiParentController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiCategoryController(IUnitOfWork unitOfWork,
            ITimeService timeService, IIdentityService identityService,
            ILog log) : base(unitOfWork)
        {
            _timeService = timeService;
            _identityService = identityService;
            _log = log;
        }

        #endregion

        #region Properties
        
        /// <summary>
        ///     Service which handles time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        ///     Service which is for logging.
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        ///     Service which analyzes identity in request.
        /// </summary>
        private readonly IIdentityService _identityService;

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate a category with specific information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> InitiateCategory([FromBody] InitiateCategoryViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCategoryViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Account validate

                // Find account information attached in the current request.
                var account = _identityService.FindAccount(Request.Properties);
                
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Record duplicate check

                var findCategoryConditions = new FindCategoriesViewModel();
                findCategoryConditions.Name = new TextSearch();
                findCategoryConditions.Name.Value = parameters.Name;
                findCategoryConditions.Name.Mode = TextComparision.EqualIgnoreCase;

                // Category has been created before.
                var category = await UnitOfWork.RepositoryCategories.FindCategoryAsync(findCategoryConditions);
                if (category != null)
                {
                    _log.Error($"Category with name : {parameters.Name} has been created before.");
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CategoryDuplicated);
                }

                #endregion

                #region Record initialization

                // Find current time on system.
                var systemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                // Find the id of requester.
                //Initiate new category
                category = new Category();
                category.CreatorIndex = account.Id;
                category.Created = systemTime;
                category.Name = parameters.Name;

                //Add category record
                UnitOfWork.RepositoryCategories.Initiate(category);

                // Save changes.
                await UnitOfWork.CommitAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.Created, category);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Update category information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateCategory([FromUri] int index,
            [FromBody] InitiateCategoryViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCategoryViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, FindValidationMessage(ModelState, nameof(parameters)));

                #endregion

                #region Category search

                // Find the category
                var findCategoryViewModel = new FindCategoriesViewModel();
                findCategoryViewModel.Id = index;
                
                // Find the category.
                var category = await UnitOfWork.RepositoryCategories.FindCategoryAsync(findCategoryViewModel);
                if (category == null)
                {
                    _log.Error($"Category (Id: {index}) is not found in database.");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);
                }

                #endregion

                #region Information update

                // Begin transaction.
                using (var transaction = UnitOfWork.Context.Database.BeginTransaction())
                {
                    try
                    {
                        // Find unix system time.
                        var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);
                        
                        // Modify information.
                        category.Name = parameters.Name;
                        category.LastModified = unixSystemTime;
                        
                        // Save changes into database.
                        await UnitOfWork.CommitAsync();

                        // Save changes into database.
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback transaction.
                        transaction.Rollback();
                        throw;
                    }
                }

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Delete a specific category.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteCategories([FromBody] FindCategoriesViewModel conditions)
        {
            try
            {
                #region Parameter validate

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCategoriesViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, FindValidationMessage(ModelState, nameof(conditions)));

                #endregion

                #region Record delete

                // Delete categories by using specific conditions.
                UnitOfWork.RepositoryCategories.Delete(conditions);

                // Save changes into database.
                var totalRecords = await UnitOfWork.CommitAsync();

                // No record has been deleted.
                if (totalRecords < 1)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);
                
                #endregion

                // Tell the client , deletion is successful.
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Filter categories by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindCategories([FromBody] FindCategoriesViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCategoriesViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        FindValidationMessage(ModelState, nameof(conditions)));

                #endregion

                #region Records search

                // Find all categories.
                var findCategoriesResult = await UnitOfWork.RepositoryCategories.FindCategoriesAsync(conditions);

                // Find all accounts in the database.
                var accounts = UnitOfWork.RepositoryAccounts.Find();

                var apiCategories = from account in accounts
                    from category in findCategoriesResult.Categories
                    where account.Id == category.CreatorIndex
                    select new CategoryViewModel
                    {
                        Id = category.Id,
                        Creator = new AccountViewModel
                        {
                            Id = account.Id,
                            Email = account.Email,
                            Nickname = account.Nickname,
                            Status = account.Status,
                            Joined = account.Joined
                        },
                        Name = category.Name,
                        Created = category.Created,
                        LastModified = category.LastModified
                    };

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, new ResponseApiCategoriesViewModel
                {
                    Categories = apiCategories,
                    Total = findCategoriesResult.Total
                });
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}