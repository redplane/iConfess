using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SystemDatabase.Models.Entities;
using Administration.Attributes;
using Administration.ViewModels.ApiCategory;
using log4net;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.Resources;
using Shared.ViewModels;
using Shared.ViewModels.Accounts;
using Shared.ViewModels.Categories;

namespace Administration.Controllers
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
        public ApiCategoryController(
            IUnitOfWork unitOfWork,
            ITimeService timeService, 
            IIdentityService identityService,
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

                // Search account information attached in the current request.
                var account = _identityService.FindAccount(Request.Properties);
                
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Record duplicate check

                var findCategoryConditions = new SearchCategoryViewModel();
                findCategoryConditions.Name = new TextSearch();
                findCategoryConditions.Name.Value = parameters.Name;
                findCategoryConditions.Name.Mode = TextComparision.EqualIgnoreCase;

                // Category has been created before.
                var categories = UnitOfWork.RepositoryCategories.Search();
                var category = await UnitOfWork.RepositoryCategories.Search(categories, findCategoryConditions).FirstOrDefaultAsync();
                if (category != null)
                {
                    _log.Error($"Category with name : {parameters.Name} has been created before.");
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CategoryDuplicated);
                }

                #endregion

                #region Record initialization

                // Search current time on system.
                var systemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                // Search the id of requester.
                //Initiate new category
                category = new Category();
                category.CreatorIndex = account.Id;
                category.Created = systemTime;
                category.Name = parameters.Name;

                //Add category record
                UnitOfWork.RepositoryCategories.Insert(category);

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
                
                // Search the category.
                var categories = UnitOfWork.RepositoryCategories.Search();
                categories = categories.Where(x => x.Id == index);

                var category = await categories.FirstOrDefaultAsync();
                if (category == null)
                {
                    _log.Error($"Category (Id: {index}) is not found in database.");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);
                }

                #endregion

                #region Information update
                
                // Search unix system time.
                var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);
                        
                // Modify information.
                category.Name = parameters.Name;
                category.LastModified = unixSystemTime;
                        
                // Save changes into database.
                await UnitOfWork.CommitAsync();

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
        public async Task<HttpResponseMessage> DeleteCategories([FromBody] SearchCategoryViewModel conditions)
        {
            try
            {
                #region Parameter validate

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new SearchCategoryViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, FindValidationMessage(ModelState, nameof(conditions)));

                #endregion

                #region Record delete

                // Delete categories by using specific conditions.
                var categories = UnitOfWork.RepositoryCategories.Search();
                categories = UnitOfWork.RepositoryCategories.Search(categories, conditions);

                // Delete the list of categories.
                UnitOfWork.RepositoryCategories.Remove(categories);

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
        public async Task<HttpResponseMessage> FindCategories([FromBody] SearchCategoryViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new SearchCategoryViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        FindValidationMessage(ModelState, nameof(conditions)));

                #endregion

                #region Records search

                // Initiate search result.
                var searchResult = new SearchResult<IList<CategoryViewModel>>();

                // Search all categories.
                var categories = UnitOfWork.RepositoryCategories.Search();
                categories = UnitOfWork.RepositoryCategories.Search(categories, conditions);

                // Search all accounts in the database.
                var accounts = UnitOfWork.RepositoryAccounts.Search();

                var apiCategories = from account in accounts
                    from category in categories
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

                // Sort the results.
                var sorting = conditions.Sorting;
                apiCategories = UnitOfWork.RepositoryCategories.Sort(apiCategories, sorting.Direction, sorting.Property);

                // Update result.
                searchResult.Total = await apiCategories.CountAsync();
                searchResult.Records = await UnitOfWork.RepositoryCategories.Paginate(apiCategories, conditions.Pagination).ToListAsync();

                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
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