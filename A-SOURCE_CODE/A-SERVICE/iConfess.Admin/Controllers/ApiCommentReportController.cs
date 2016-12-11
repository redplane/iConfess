using System;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.CommentReports;
using Shared.ViewModels.Comments;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/report/comment")]
    public class ApiCommentReportController : ApiController
    {
        #region Properties

        /// <summary>
        /// Provides repositories to access database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Service which is used for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate controller with IoC
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        public ApiCommentReportController(IUnitOfWork unitOfWork, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initiate a comment report.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> InitiateCommentReport(
            [FromBody] InitiateCommentReportViewModel parameters)
        {
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
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                #endregion

                #region Comment find

                // Comment find conditions build.
                var findCommentViewModel = new FindCommentsViewModel();
                findCommentViewModel.Id = parameters.CommentIndex;

                // Find the comment with specific conditions.
                var findCommentResult = await _unitOfWork.RepositoryComments.FindCommentsAsync(findCommentViewModel);
                if (findCommentResult == null || findCommentResult.Total < 1)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);
                }

                // Result is not unique.
                if (findCommentResult.Total != 1)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CommentReportNotUnique);
                }

                // Result cannot be retrieved.
                var comment = await findCommentResult.Comments.FirstOrDefaultAsync();
                if (comment == null)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);
                }

                #endregion

                #region Record initialization

                // TODO: Obtain the reporter index.
                var reporter = 1;

                var commentReport = new CommentReport();
                commentReport.CommentIndex = comment.Id;
                commentReport.CommentOwnerIndex = comment.OwnerIndex;
                commentReport.CommentReporterIndex = reporter;
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
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                #endregion

                // Delete found comment reports and count the total affected number.
                var totalRecords = await _unitOfWork.RepositoryCommentReports.DeleteCommentReportsAsync(parameters);

                if (totalRecords < 1)
                {
                    // TODO: Add log.
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentReportNotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
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
                {
                    // TODO: Add log.
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                #endregion

                // Find comment reports with specific conditions.
                var findResult = _unitOfWork.RepositoryCommentReports.FindCommentReportsAsync(parameters);

                return Request.CreateResponse(HttpStatusCode.OK, findResult);
            }
            catch (Exception exception)
            {
                // TODO: Add log.
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}