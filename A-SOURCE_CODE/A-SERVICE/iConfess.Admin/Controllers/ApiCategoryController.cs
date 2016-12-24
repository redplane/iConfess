﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.ViewModels.ApiCategory;
using iConfess.Database.Models.Tables;
using log4net;
using Shared.Enumerations;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.Accounts;
using Shared.ViewModels.Categories;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/category")]
    public class ApiCategoryController : ApiParentController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="log"></param>
        public ApiCategoryController(IUnitOfWork unitOfWork, ITimeService timeService, ILog log): base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
            _log = log;
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

        /// <summary>
        /// Service which is for logging.
        /// </summary>
        private readonly ILog _log;

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
                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCategoryViewModel();
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
                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCategoryViewModel();
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
                        await UnitOfWork.Context.SaveChangesAsync();

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
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, FindValidationMessage(ModelState, nameof(conditions)));
                }

                #endregion

                // Find all categories.
                var findCategoriesResult = await _unitOfWork.RepositoryCategories.FindCategoriesAsync(conditions);
                
                // Find all accounts in the database.
                var accounts = _unitOfWork.Context.Accounts.AsQueryable();
                
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