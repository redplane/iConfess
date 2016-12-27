using System;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.PostReports;
using Shared.ViewModels.Posts;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/report/post")]
    public class ApiPostReportController : ApiController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with IoC
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        public ApiPostReportController(IUnitOfWork unitOfWork, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
        }

        #endregion

        /// <summary>
        ///     Initiate a post report with given information.
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> InitiatePostReport([FromBody] InitiatePostReportViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized..
                if (parameters == null)
                {
                    // Initiate the parameters.
                    parameters = new InitiatePostReportViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Find post

                var findPostViewModel = new FindPostViewModel();
                findPostViewModel.Id = parameters.PostIndex;

                // Find the specific post in database.
                var findPostsResult = await _unitOfWork.RepositoryPosts.FindPostsAsync(findPostViewModel);

                // No record has been found.
                if ((findPostsResult == null) || (findPostsResult.Total < 0))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostNotFound);

                // Not only one record has been found.
                if (findPostsResult.Total != 1)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostIsNotUnique);

                // Find the post information.
                var post = await findPostsResult.Posts.FirstOrDefaultAsync();
                if (post == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostNotFound);

                #endregion

                #region Report initialization.

                // TODO: Find reporter index from request.
                var reporterIndex = 1;

                var postReport = new PostReport();
                postReport.PostIndex = post.Id;
                postReport.PostOwnerIndex = post.OwnerIndex;
                postReport.PostReporterIndex = reporterIndex;
                postReport.Body = post.Body;
                postReport.Reason = parameters.Reason;
                postReport.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                // Initiate post report into database.
                postReport = await _unitOfWork.RepositoryPostReports.InitiatePostReportAsync(postReport);

                #endregion

                // Tell the client about the post report.
                return Request.CreateResponse(HttpStatusCode.OK, postReport);
            }
            catch (Exception exception)
            {
                // TODO: Add log.
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Delete reports about specific post (using conditions for search)
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeletePostReports([FromBody] FindPostReportsViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    // Initiate parameters.
                    parameters = new FindPostReportsViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                // Delete post reports by using specific conditions.
                var totalRecords = await _unitOfWork.RepositoryPostReports.DeletePostsAsync(parameters);

                // No record has been affected.
                if (totalRecords < 1)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostReportNotFound);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                // TODO: Add log.
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Find post reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindPostReports([FromBody] FindPostReportsViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    // Initiate parameters.
                    parameters = new FindPostReportsViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                // Delete post reports by using specific conditions.
                var findResult = await _unitOfWork.RepositoryPostReports.FindPostReportsAsync(parameters);

                return Request.CreateResponse(HttpStatusCode.OK, findResult);
            }
            catch (Exception exception)
            {
                // TODO: Add log.
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #region Properties

        /// <summary>
        ///     Provides repositories to access database.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///     Provides function for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        #endregion
    }
}