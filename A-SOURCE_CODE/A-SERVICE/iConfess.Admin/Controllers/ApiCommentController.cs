using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Database.Models.Tables;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.CommentReports;
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
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiCommentController(
            IUnitOfWork unitOfWork, 
            ITimeService timeService, 
            IIdentityService identityService,
            ILog log)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
            _identityService = identityService;
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
        /// Service which handles identity in request.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Service which is for handling log.
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Request identity search

                // Find account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Comment initialization

                var comment = new Comment();
                comment.OwnerIndex = account.Id;
                comment.PostIndex = parameters.PostIndex;
                comment.Content = parameters.Content;
                comment.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                //Add category record
                _unitOfWork.RepositoryComments.Initiate(comment);

                #endregion

                // Save changes into database.
                await _unitOfWork.CommitAsync();

                return Request.CreateResponse(HttpStatusCode.Created, comment);
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Record find

                // Find the category
                var findComment = new FindCommentsViewModel();
                findComment.Id = index;

                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryComments.FindCommentsAsync(findComment);

                // No record has been found
                if (response.Total < 1)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);

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
        public async Task<HttpResponseMessage> DeleteComments([FromBody] FindCommentsViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCommentsViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Find & delete records

                // Find all comments in database.
                var comments = _unitOfWork.RepositoryComments.FindComments();
                comments = _unitOfWork.RepositoryComments.FindComments(comments, conditions);

                // Loop through every found comments.
                foreach (var comment in comments)
                {
                    var findCommentReportConditions = new FindCommentReportsViewModel();
                    findCommentReportConditions.CommentIndex = comment.Id;

                    // Delete all comment report.
                    _unitOfWork.RepositoryCommentReports.Delete(findCommentReportConditions);
                }

                // Delete all found comments.
                _unitOfWork.RepositoryComments.Delete(conditions);

                // Save changes.
                var totalRecords = await _unitOfWork.CommitAsync();

                // No record has been deleted.
                if (totalRecords < 1)
                {
                    _log.Error($"There is no comment (Id: {conditions.Id}) from database.");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);
                }

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
        public async Task<HttpResponseMessage> FindComments([FromBody] FindCommentsViewModel conditions)
        {
            try
            {
                #region Parameter validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindCommentsViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                #endregion
                
                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryComments.FindCommentsAsync(conditions);

                return Request.CreateResponse(HttpStatusCode.OK, response);
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