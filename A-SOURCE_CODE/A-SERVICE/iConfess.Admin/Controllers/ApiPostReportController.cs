using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Admin.ViewModels.ApiPostReport;
using iConfess.Database.Models.Tables;
using log4net;
using Shared.Enumerations.Order;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.PostReports;
using Shared.ViewModels.Posts;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/report/post")]
    [ApiAuthorize]
    public class ApiPostReportController : ApiController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with IoC
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiPostReportController(IUnitOfWork unitOfWork,
            ITimeService timeService, IIdentityService identityService,
            ILog log)
        {
            _unitOfWork = unitOfWork;
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

                #region Find post report

                // Find all posts.
                var posts = _unitOfWork.RepositoryPosts.FindPosts();

                // Conditions construction.
                var findPostViewModel = new FindPostViewModel();
                findPostViewModel.Id = parameters.PostIndex;

                // Find the post information.
                var post = await _unitOfWork.RepositoryPosts.FindPosts(posts, findPostViewModel).FirstOrDefaultAsync();

                // Post is not found.
                if (post == null)
                {
                    _log.Error($"There is no post (ID: {parameters.PostIndex}) has been found in database");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostNotFound);
                }

                #endregion

                #region Requester search.

                // Find the request sender.
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
                postReport = await _unitOfWork.RepositoryPostReports.InitiatePostReportAsync(postReport);

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

                #region Record delete

                // Delete post reports by using specific conditions.
                _unitOfWork.RepositoryPostReports.Delete(parameters);

                // Commit the change.
                var totalRecord = await _unitOfWork.CommitAsync();

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

                #region Find post reports

                // Find all post reports.
                var postReports = _unitOfWork.RepositoryPostReports.FindPostReports();
                postReports = _unitOfWork.RepositoryPostReports.FindPostReports(postReports, parameters);
                
                // Find posts from database.
                var posts = _unitOfWork.RepositoryPosts.FindPosts();
                
                // Find all accounts.
                var accounts = _unitOfWork.RepositoryAccounts.FindAccounts();

                // Find post report by using specific conditions.
                var result = from postReport in postReports
                    from post in posts
                    from owner in accounts
                    from reporter in accounts
                    where postReport.PostIndex == post.Id
                          && postReport.PostOwnerIndex == owner.Id
                          && postReport.PostReporterIndex == reporter.Id
                    select new PostReportViewModel
                    {
                        Id = postReport.Id,
                        Post = post,
                        Owner = owner,
                        Reporter = reporter,
                        Body = postReport.Body,
                        Reason = postReport.Reason,
                        Created = postReport.Created
                    };

                #endregion

                #region Result order 

                switch (parameters.Direction)
                {
                    case SortDirection.Decending:
                        switch (parameters.Sort)
                        {
                            case PostReportSort.PostIndex:
                                result = result.OrderByDescending(x => x.Post.Id);
                                break;
                            case PostReportSort.PostOwnerIndex:
                                result = result.OrderByDescending(x => x.Owner.Id);
                                break;
                            case PostReportSort.PostReporterIndex:
                                result = result.OrderByDescending(x => x.Reporter.Id);
                                break;
                            case PostReportSort.Created:
                                result = result.OrderByDescending(x => x.Created);
                                break;
                            default:
                                result = result.OrderByDescending(x => x.Id);
                                break;
                        }
                        break;
                    default:
                        switch (parameters.Sort)
                        {
                            case PostReportSort.PostIndex:
                                result = result.OrderBy(x => x.Post.Id);
                                break;
                            case PostReportSort.PostOwnerIndex:
                                result = result.OrderBy(x => x.Owner.Id);
                                break;
                            case PostReportSort.PostReporterIndex:
                                result = result.OrderBy(x => x.Reporter.Id);
                                break;
                            case PostReportSort.Created:
                                result = result.OrderBy(x => x.Created);
                                break;
                            default:
                                result = result.OrderBy(x => x.Id);
                                break;
                        }
                        break;
                }

                #endregion

                #region Pagination

                // Count the total records.
                var total = await result.CountAsync();

                // Find pagination information.
                var pagination = parameters.Pagination;
                
                // Pagination specified.
                if (pagination != null)
                {
                    // Do pagination.
                    result = result
                        .Skip(pagination.Index*pagination.Records)
                        .Take(pagination.Records);
                }
                
                // Initiate response.
                var responseFindPostReport = new ResponseFindPostReportViewModel();
                responseFindPostReport.PostReports = result;
                responseFindPostReport.Total = total;

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, responseFindPostReport);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
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