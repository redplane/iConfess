using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SystemDatabase.Enumerations;
using SystemDatabase.Models.Entities;
using Administration.Attributes;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels;
using Shared.ViewModels.CommentReports;
using Shared.ViewModels.Comments;

namespace Administration.Controllers
{
    [RoutePrefix("api/report/comment")]
    [ApiAuthorize]
    [ApiRole(Roles.Admin)]
    public class ApiCommentReportController : ApiParentController
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
            ILog log) : base(unitOfWork)
        {
            _timeService = timeService;
            _identityService = identityService;
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
                var findCommentViewModel = new SearchCommentViewModel();
                findCommentViewModel.Id = parameters.CommentIndex;

                // Search all comments from database.
                var comments = UnitOfWork.RepositoryComments.Search();
                comments = UnitOfWork.RepositoryComments.Search(comments, findCommentViewModel);

                // Search the first matched comment.
                var comment = await comments.FirstOrDefaultAsync();
                if (comment == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CommentNotFound);

                #endregion

                #region Request identity search

                // Search account which sends the current request.
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
                commentReport = UnitOfWork.RepositoryCommentReports.Insert(commentReport);

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
        public async Task<HttpResponseMessage> DeleteCommentReport([FromBody] SearchCommentReportViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new SearchCommentReportViewModel();
                    Validate(parameters);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Request identity search

                // Search account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Record delete

                // Account can only delete the reports whose reporter is the requester.
                if (account.Role != Roles.Admin)
                    parameters.CommentReporterIndex = account.Id;

                // Search comment reports by using specific conditions.
                var commentReports = UnitOfWork.RepositoryCommentReports.Search();
                commentReports = UnitOfWork.RepositoryCommentReports.Search(commentReports, parameters);

                // Search comments and delete 'em all.
                UnitOfWork.RepositoryCommentReports.Remove(commentReports);

                // Save changes.
                var totalRecords = await UnitOfWork.CommitAsync();

                // Nothing is changed.
                if (totalRecords < 1)
                {
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
        ///     Search list of comment reports by using specific conditions.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindCommentReports([FromBody] SearchCommentReportViewModel condition)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (condition == null)
                {
                    condition = new SearchCommentReportViewModel();
                    Validate(condition);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, FindValidationMessage(ModelState, nameof(condition)));

                #endregion

                #region Request identity search

                // Search account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Comment report search

                // Account can only see the comments which it is their reporter.
                if (account.Role != Roles.Admin)
                    condition.CommentReporterIndex = account.Id;

                // Search comment reports with specific conditions.
                var commentReports = UnitOfWork.RepositoryCommentReports.Search();
                commentReports = UnitOfWork.RepositoryCommentReports.Search(commentReports, condition);
                commentReports = UnitOfWork.RepositoryCommentReports.Sort(commentReports, condition.Direction, condition.Sort);
                
                // Search all accounts.
                var accounts = UnitOfWork.RepositoryAccounts.Search();

                // Search all comments.
                var comments = UnitOfWork.RepositoryComments.Search();

                // Search all posts.
                var posts = UnitOfWork.RepositoryPosts.Search();

                // Search comment report details
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
                                                      Comment = comment,
                                                      Post = post,
                                                      Body = commentReport.Body,
                                                      Reason = commentReport.Reason,
                                                      Created = commentReport.Created
                                                  };

                #endregion

                #region Comment report search
                
                // Initiate result.
                var result = new SearchResult<IQueryable<CommentReportDetailViewModel>>();

                // Count the total records first.
                result.Total = await commentReports.CountAsync();

                // Do pagination.
                result.Records = UnitOfWork.RepositoryCommentReports.Paginate(commentReportDetails, condition.Pagination);
                
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
        ///     Search details of a comment report.
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

                // Search account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Comment report search

                // Search comment reports with specific conditions.
                var commentReports = UnitOfWork.RepositoryCommentReports.Search();

                // Account can only see the comments which it is their reporter.
                if (account.Role != Roles.Admin)
                    commentReports = commentReports.Where(x => x.CommentReporterIndex == account.Id);
                
                // Search all accounts.
                var accounts = UnitOfWork.RepositoryAccounts.Search();

                // Search all comments.
                var comments = UnitOfWork.RepositoryComments.Search();

                // Search all posts.
                var posts = UnitOfWork.RepositoryPosts.Search();

                // Search comment report details
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