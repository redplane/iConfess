using System;
using System.Data.Entity;
using System.Linq;
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
    public class ApiCommentReportController : ApiParentController
    {
        #region Constructors

        /// <summary>
        ///     Initiate controller with IoC
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="commonRepositoryService"></param>
        /// <param name="log"></param>
        public ApiCommentReportController(IUnitOfWork unitOfWork,
            ITimeService timeService,
            IIdentityService identityService,
            ICommonRepositoryService commonRepositoryService,
            ILog log) : base(unitOfWork)
        {
            _timeService = timeService;
            _identityService = identityService;
            _commonRepositoryService = commonRepositoryService;
            _log = log;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Service which is used for time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        ///     Service which handles identity in request.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        ///     Service which handles common repository business.
        /// </summary>
        private readonly ICommonRepositoryService _commonRepositoryService;

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
        [Route("")]
        [HttpPost]
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Comment find

                // Comment find conditions build.
                var findCommentViewModel = new FindCommentsViewModel();
                findCommentViewModel.Id = parameters.CommentIndex;

                // Find all comments from database.
                var comments = UnitOfWork.RepositoryComments.Find();
                comments = UnitOfWork.RepositoryComments.Find(comments, findCommentViewModel);

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
                commentReport = UnitOfWork.RepositoryCommentReports.Initiate(commentReport);

                // Commit changes to database.
                await UnitOfWork.CommitAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, commentReport);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Delete all reports from a comment due to its validation.
        /// </summary>
        /// <returns></returns>
        [Route("")]
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

                // Account can only delete the reports whose reporter is the requester.
                if (account.Role != AccountRole.Admin)
                    parameters.CommentReporterIndex = account.Id;

                // Find comments and delete 'em all.
                UnitOfWork.RepositoryCommentReports.Delete(parameters);

                // Save changes.
                var totalRecords = await UnitOfWork.CommitAsync();

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
        /// <param name="condition"></param>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindCommentReports([FromBody] FindCommentReportsViewModel condition)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (condition == null)
                {
                    condition = new FindCommentReportsViewModel();
                    Validate(condition);
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
                    condition.CommentReporterIndex = account.Id;

                // Find comment reports with specific conditions.
                var commentReports = UnitOfWork.RepositoryCommentReports.Find();
                commentReports = UnitOfWork.RepositoryCommentReports.Find(commentReports, condition);
                commentReports = _commonRepositoryService.Sort(commentReports, condition.Direction, condition.Sort);
                
                // Find all accounts.
                var accounts = UnitOfWork.RepositoryAccounts.Find();

                // Find all comments.
                var comments = UnitOfWork.RepositoryComments.Find();

                // Find all posts.
                var posts = UnitOfWork.RepositoryPosts.Find();

                // Find comment report details
                var commentReportDetails = from commentReport in commentReports
                                                  from commentOwner in accounts
                                                  from commentReporter in accounts
                                                  from comment in comments
                                                  from post in posts
                                                  where (commentReport.CommentOwnerIndex == commentOwner.Id)
                                                        && (commentReport.CommentReporterIndex == commentReporter.Id)
                                                        && (commentReport.PostIndex == post.Id)
                                                        && (commentReport.CommentIndex == comment.Id)
                                                  select new CommentReportDetailViewModel
                                                  {
                                                      Id = commentReport.Id,
                                                      Comment = comment,
                                                      Post = post,
                                                      Body = commentReport.Body,
                                                      Reason = commentReport.Reason,
                                                      Created = commentReport.Created
                                                  };

                #endregion

                #region Comment report search
                
                // Initiate result.
                var result = new ResponseCommentReportsViewModel();

                // Count the total records first.
                result.Total = await commentReports.CountAsync();

                // Do pagination.
                commentReportDetails = _commonRepositoryService.Paginate(commentReportDetails, condition.Pagination);
                result.CommentReports = await commentReportDetails.ToListAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Find details of a comment report.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [Route("details")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindCommentReportDetails([FromUri] int index)
        {
            try
            {
                #region Request identity search

                // Find account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Comment report search

                // Find comment reports with specific conditions.
                var commentReports = UnitOfWork.RepositoryCommentReports.Find();

                // Account can only see the comments which it is their reporter.
                if (account.Role != AccountRole.Admin)
                    commentReports = commentReports.Where(x => x.CommentReporterIndex == account.Id);

                // Find the comment by using id.
                commentReports = commentReports.Where(x => x.Id == index);

                // Find all accounts.
                var accounts = UnitOfWork.RepositoryAccounts.Find();

                // Find all comments.
                var comments = UnitOfWork.RepositoryComments.Find();

                // Find all posts.
                var posts = UnitOfWork.RepositoryPosts.Find();

                // Find comment report details
                var commentReportDetails = await (from commentReport in commentReports
                    from commentOwner in accounts
                    from commentReporter in accounts
                    from comment in comments
                    from post in posts
                    where (commentReport.CommentOwnerIndex == commentOwner.Id)
                          && (commentReport.CommentReporterIndex == commentReporter.Id)
                          && (commentReport.PostIndex == post.Id)
                          && (commentReport.CommentIndex == comment.Id)
                    select new CommentReportDetailViewModel
                    {
                        Id = commentReport.Id,
                        Comment = comment,
                        Post = post,
                        Body = commentReport.Body,
                        Reason = commentReport.Reason,
                        Created = commentReport.Created
                    }).FirstOrDefaultAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, commentReportDetails);
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