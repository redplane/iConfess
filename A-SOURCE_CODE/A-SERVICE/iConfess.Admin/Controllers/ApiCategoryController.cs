using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.ViewModels.ApiCategory;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.Categories;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/category")]
    public class ApiCategoryController : ApiController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        public ApiCategoryController(IUnitOfWork unitOfWork, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Unit of work which provides database context and repositories to handle business of application.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Service which handles time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate a category with specific information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> InitiateCategory([FromBody] CategoryViewModel parameters)
        {
            try
            {
                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new CategoryViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                //Initiate new category
                var category = new Category();
                category.CreatorIndex = parameters.CreatorIndex;
                category.Created = parameters.Created;
                category.Name = parameters.Name;

                //Add category record
                await _unitOfWork.RepositoryCategories.InitiateCategoryAsync(category);

                return Request.CreateResponse(HttpStatusCode.Created, category);
            }
            catch (Exception exception)
            {
                // TODO: Write log.
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
            [FromBody] CategoryViewModel parameters)
        {
            try
            {
                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new CategoryViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                // Find the category
                var findCategoryViewModel = new FindCategoriesViewModel();
                findCategoryViewModel.Id = index;

                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryCategories.FindCategoriesAsync(findCategoryViewModel);

                // No record has been found
                if (response.Total < 1)
                {
                    // TODO: Add log
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);
                }

                // Begin transaction.
                using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
                {
                    try
                    {
                        // Find unix system time.
                        var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                        // Update all categories.
                        foreach (var category in response.Categories)
                        {
                            category.Name = parameters.Name;
                            category.LastModified = unixSystemTime;
                        }

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

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
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
                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCategoriesViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                // Delete categories by using specific conditions.
                var totalRecords = await _unitOfWork.RepositoryCategories.DeleteCategoriesAsync(conditions);

                // No record has been deleted.
                if (totalRecords < 1)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);
                }
                // Tell the client , deletion is successful.
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                // TODO: Add log
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
                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCategoriesViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryCategories.FindCategoriesAsync(conditions);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception exception)
            {
                // TODO: Add log
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}