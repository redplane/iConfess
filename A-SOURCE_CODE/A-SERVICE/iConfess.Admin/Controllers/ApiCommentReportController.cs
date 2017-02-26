using System;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Database.Enumerations;
using iConfess.Database.Models.Tables;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.CommentReports;
using Shared.ViewModels.Comments;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/report/comment")]
    [ApiAuthorize]
    [ApiRole(AccountRole.Admin)]
    public class ApiCommentReportController : ApiController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with IoC
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiCommentReportController(IUnitOfWork unitOfWork,
            ITimeService timeService,
            IIdentityService identityService,
            ILog log)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
            _log = log;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Provides repositories to access database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Service which is used for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        /// Service which handles identity in request.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        ///     Service which handles logging operation.
        /// </summary>
        private readonly ILog _log;

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate a comment report.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("initiate")]
        [HttpPost]
        public async Task<HttpResponseMessage> InitiateCommentReport(
            [FromBody] InitiateCommentReportViewModel parameters)
        {
            throw new NotImplementedException();
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiateCommentReportViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Comment find

                // Comment find conditions build.
                var findCommentViewModel = new FindCommentsViewModel();
                findCommentViewModel.Id = parameters.CommentIndex;

                // Find all comments from database.
                var comments = _unitOfWork.RepositoryComments.FindComments();
                comments = _unitOfWork.RepositoryComments.FindComments(comments, findCommentViewModel);
                
                // Find the first matched comment.
                var comment = await comments.FirstOrDefaultAsync();
                if (comment == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);

                #endregion

                #region Request identity search

                // Find account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Record initialization

                var commentReport = new CommentReport();
                commentReport.CommentIndex = comment.Id;
                commentReport.CommentOwnerIndex = comment.OwnerIndex;
                commentReport.CommentReporterIndex = account.Id;
                commentReport.Body = comment.Content;
                commentReport.Reason = parameters.Reason;
                commentReport.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                // Save record into database.
                commentReport = await _unitOfWork.RepositoryCommentReports.InitiateCommentReportAsync(commentReport);

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, commentReport);
            }
            catch (Exception exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Delete all reports from a comment due to its validation.
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteCommentReport([FromBody] FindCommentReportsViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new FindCommentReportsViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion
                
                #region Request identity search

                // Find account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion
                
                #region Record delete

                // Account can only delete the reports whose reporter is it.
                if (account.Role != AccountRole.Admin)
                    parameters.CommentReporterIndex = account.Id;
                
                // Find comments and delete 'em all.
                _unitOfWork.RepositoryCommentReports.Delete(parameters);

                // Save changes.
                var totalRecords = await _unitOfWork.CommitAsync();
                
                // Nothing is changed.
                if (totalRecords < 1)
                {
                    _log.Error($"No comment (ID: {parameters.Id}) is found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        HttpMessages.CommentReportNotFound);
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
        ///     Find list of comment reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public HttpResponseMessage FilterCommentReports([FromBody] FindCommentReportsViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new FindCommentReportsViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion


                #region Request identity search

                // Find account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Comment report search

                // Account can only see the comments which it is their reporter.
                if (account.Role != AccountRole.Admin)
                    parameters.CommentReporterIndex = account.Id;
                
                // Find comment reports with specific conditions.
                var findResult = _unitOfWork.RepositoryCommentReports.FindCommentReportsAsync(parameters);

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, findResult);
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