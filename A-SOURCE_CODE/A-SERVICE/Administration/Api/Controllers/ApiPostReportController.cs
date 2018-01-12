using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SystemDatabase.Models.Entities;
using Administration.Attributes;
using Administration.ViewModels.ApiPostReport;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels;
using Shared.ViewModels.PostReports;
using Shared.ViewModels.Posts;

namespace Administration.Controllers
{
    [RoutePrefix("api/report/post")]
    [ApiAuthorize]
    public class ApiPostReportController : ApiParentController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with IoC
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiPostReportController(
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

                #region Search post report

                // Search all posts.
                var posts = UnitOfWork.RepositoryPosts.Search();

                // Conditions construction.
                var findPostViewModel = new SearchPostViewModel();
                findPostViewModel.Id = parameters.PostIndex;

                // Search the post information.
                var post = await UnitOfWork.RepositoryPosts.Search(posts, findPostViewModel).FirstOrDefaultAsync();

                // Post is not found.
                if (post == null)
                {
                    _log.Error($"There is no post (ID: {parameters.PostIndex}) has been found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostNotFound);
                }

                #endregion

                #region Requester search.

                // Search the request sender.
                var requester = _identityService.FindAccount(Request.Properties);

                // Requester is not found.
                if (requester == null)
                {
                    _log.Error("No requester is found in the request.");
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        HttpMessages.RequestIsUnauthenticated);
                }

                #endregion

                #region Report initialization.

                var postReport = new PostReport();
                postReport.PostIndex = post.Id;
                postReport.PostOwnerIndex = post.OwnerIndex;
                postReport.PostReporterIndex = requester.Id;
                postReport.Body = post.Body;
                postReport.Reason = parameters.Reason;
                postReport.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                // Initiate post report into database.
                postReport = UnitOfWork.RepositoryPostReports.Insert(postReport);

                // Commit changes.
                #endregion

                // Tell the client about the post report.
                return Request.CreateResponse(HttpStatusCode.OK, postReport);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Delete reports about specific post (using conditions for search)
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeletePostReports([FromBody] SearchPostReportViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    // Initiate parameters.
                    parameters = new SearchPostReportViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Record delete

                // Search post reports.
                var postReports = UnitOfWork.RepositoryPostReports.Search();
                postReports = UnitOfWork.RepositoryPostReports.Search(postReports, parameters);

                // Find comment 
                // Delete post reports by using specific conditions.
                UnitOfWork.RepositoryPostReports.Remove(postReports);

                // Commit the change.
                var totalRecord = await UnitOfWork.CommitAsync();

                // No record has been affected.
                if (totalRecord < 1)
                {
                    _log.Error($"No post report (ID: {parameters.Id}) is found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostReportNotFound);
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
        ///     Search post reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindPostReports([FromBody] SearchPostReportViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    // Initiate parameters.
                    parameters = new SearchPostReportViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Search post reports

                // Search all post reports.
                var postReports = UnitOfWork.RepositoryPostReports.Search();
                postReports = UnitOfWork.RepositoryPostReports.Search(postReports, parameters);
                
                // Search posts from database.
                var posts = UnitOfWork.RepositoryPosts.Search();
                
                // Search all accounts.
                var accounts = UnitOfWork.RepositoryAccounts.Search();

                // Search post report by using specific conditions.
                var postReportDetails = from postReport in postReports
                    from post in posts
                    from owner in accounts
                    from reporter in accounts
                    where postReport.PostIndex == post.Id
                          && postReport.PostOwnerIndex == owner.Id
                          && postReport.PostReporterIndex == reporter.Id
                    select new PostReportViewModel
                    {
                        Post = post,
                        Owner = owner,
                        Reporter = reporter,
                        Body = postReport.Body,
                        Reason = postReport.Reason,
                        Created = postReport.Created
                    };

                // Order records.
                postReportDetails = UnitOfWork.RepositoryPostReports.Sort(postReportDetails, parameters.Direction, parameters.Sort);

                // Initiate search result
                var searchResult = new SearchResult<IQueryable<PostReportViewModel>>();
                searchResult.Total = await postReportDetails.CountAsync();
                searchResult.Records = UnitOfWork.RepositoryPostReports.Paginate(postReportDetails, parameters.Pagination);
                
                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #region Properties
        
        /// <summary>
        ///     Provides function for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        ///     Service which handles identity operations.
        /// </summary>
        private readonly IIdentityService _identityService;
        
        /// <summary>
        ///     Service which handles logging process.
        /// </summary>
        private readonly ILog _log;

        #endregion
    }
}