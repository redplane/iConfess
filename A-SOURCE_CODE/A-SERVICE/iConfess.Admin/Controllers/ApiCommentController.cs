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
using Shared.ViewModels.Comments;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/comment")]
    public class ApiCommentController : ApiController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        public ApiCommentController(IUnitOfWork unitOfWork, ITimeService timeService)
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
        public async Task<HttpResponseMessage> InitiateComment([FromBody] InitiateCommentViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCommentViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                #endregion

                // TODO : Find owner index of request.
                var ownerIndex = 1;
                var comment = new Comment();
                comment.OwnerIndex = ownerIndex;
                comment.PostIndex = parameters.PostIndex;
                comment.Content = parameters.Content;
                comment.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                //Add category record
                comment = await _unitOfWork.RepositoryComments.InitiateCommentAsync(comment);

                return Request.CreateResponse(HttpStatusCode.Created, comment);
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
        public async Task<HttpResponseMessage> UpdateComment([FromUri] int index,
            [FromBody] InitiateCommentViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCommentViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                #endregion

                #region Record find

                // Find the category
                var findComment = new FindCommentViewModel();
                findComment.Id = index;

                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryComments.FindCommentsAsync(findComment);

                // No record has been found
                if (response.Total < 1)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);
                }

                #endregion

                #region Record update

                // Begin transaction.
                using (var transaction = _unitOfWork.Context.Database.BeginTransaction())
                {
                    try
                    {
                        // Calculate the system time.
                        var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                        // Update all categories.
                        foreach (var comment in response.Comments)
                        {
                            comment.Content = parameters.Content;
                            comment.LastModified = unixSystemTime;
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

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                // TODO: Add log
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Delete a specific category.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteComments([FromBody] FindCommentViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCommentViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                #endregion

                #region Find & delete records

                // Delete categories by using specific conditions.
                var totalRecords = await _unitOfWork.RepositoryComments.DeleteCommentsAsync(conditions);

                // No record has been deleted.
                if (totalRecords < 1)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);
                }

                #endregion

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
        public async Task<HttpResponseMessage> FindComments([FromBody] FindCommentViewModel conditions)
        {
            try
            {
                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCommentViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryComments.FindCommentsAsync(conditions);

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