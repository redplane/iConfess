﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SystemDatabase.Enumerations;
using SystemDatabase.Models.Entities;
using Administration.Attributes;
using Administration.ViewModels.ApiComment;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels;
using Shared.ViewModels.Comments;

namespace Administration.Controllers
{
    [RoutePrefix("api/comment")]
    [ApiAuthorize]
    [ApiRole(Roles.Admin)]
    public class ApiCommentController : ApiParentController
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
            ILog log) : base(unitOfWork)
        {
            _timeService = timeService;
            _identityService = identityService;
            _log = log;
        }

        #endregion

        #region Properties
        
        /// <summary>
        ///     Service which handles time calculation.
        /// </summary>
        private readonly ITimeService _timeService;

        /// <summary>
        ///     Service which handles identity in request.
        /// </summary>
        private readonly IIdentityService _identityService;

        /// <summary>
        ///     Service which is for handling log.
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

                // Search account which sends the current request.
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
                UnitOfWork.RepositoryComments.Insert(comment);

                // Save changes into database.
                await UnitOfWork.CommitAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, comment);
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

                #region Request identity search

                // Search account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Record find

                // Search the category
                var condition = new SearchCommentViewModel();
                condition.Id = index;

                // Account can only change its comment as it is not an administrator.
                if (account.Role != Roles.Admin)
                    condition.OwnerIndex = account.Id;

                // Search categories by using specific conditions.
                var comments = UnitOfWork.RepositoryComments.Search();
                comments = UnitOfWork.RepositoryComments.Search(comments, condition);

                #endregion

                #region Record update

                // Calculate the system time.
                var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                // Update all categories.
                foreach (var comment in comments)
                {
                    comment.Content = parameters.Content;
                    comment.LastModified = unixSystemTime;
                }

                // Save changes into database.
                await UnitOfWork.CommitAsync();

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
        public async Task<HttpResponseMessage> DeleteComments([FromBody] SearchCommentViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new SearchCommentViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Request identity search

                // Search account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Search & delete records

                // Account can only delete its own comment as it is not an administrator.
                if (account.Role != Roles.Admin)
                    conditions.OwnerIndex = account.Id;

                // Search all comments in database.
                var comments = UnitOfWork.RepositoryComments.Search();
                comments = UnitOfWork.RepositoryComments.Search(comments, conditions);

                // Search comment reports and delete them first.
                var commentReports = UnitOfWork.RepositoryCommentReports.Search();
                var markCommentReports = from comment in comments
                    from commentReport in commentReports
                    where comment.Id == commentReport.CommentIndex
                    select commentReport;


                // Delete all found comments.
                UnitOfWork.RepositoryCommentReports.Remove(markCommentReports);
                UnitOfWork.RepositoryComments.Remove(comments);

                // Save changes.
                var totalRecords = await UnitOfWork.CommitAsync();

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
        public async Task<HttpResponseMessage> FindComments([FromBody] SearchCommentViewModel conditions)
        {
            try
            {
                #region Parameter validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new SearchCommentViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                #endregion

                #region Request identity search

                // Search account which sends the current request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account information is attached into current request.");

                #endregion

                #region Search comments

                // Search comments by using specific conditions.
                var comments = UnitOfWork.RepositoryComments.Search();
                comments = UnitOfWork.RepositoryComments.Search(comments, conditions);

                var searchResult = new SearchResult<IQueryable<Comment>>();
                searchResult.Total = await comments.CountAsync();
                searchResult.Records = UnitOfWork.RepositoryComments.Paginate(comments, conditions.Pagination);

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
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
        [Route("details")]
        [HttpGet]
        public async Task<HttpResponseMessage> FindCommentDetails([FromUri] int index)
        {
            try
            {
                #region Search comments

                // Search all comment in database.
                var comments = UnitOfWork.RepositoryComments.Search();
                comments = comments.Where(x => x.Id == index);

                // Search all account in database.
                var accounts = UnitOfWork.RepositoryAccounts.Search();

                var commentDetails = await (from comment in comments
                    from owner in accounts
                    where owner.Id == comment.OwnerIndex
                    select new CommentDetailViewModel
                    {
                        Id = comment.Id,
                        Owner = owner,
                        Content = comment.Content,
                        Created = comment.Created,
                        LastModified = comment.LastModified
                    }).FirstOrDefaultAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, commentDetails);
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
        [Route("~/api/comments/details")]
        [HttpPost]
        public async Task<HttpResponseMessage> FindCommentsDetails(
            [FromBody] FindCommentsDetailsViewModel searchCommentsDetailsCondition)
        {
            try
            {
                #region Parameter validation

                // Conditions haven't been initialized.
                if (searchCommentsDetailsCondition == null)
                {
                    searchCommentsDetailsCondition = new FindCommentsDetailsViewModel();
                    Validate(searchCommentsDetailsCondition);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                #endregion

                #region Search comments

                // Search categories by using specific conditions.
                var conditions = new SearchCommentViewModel();
                conditions.Id = searchCommentsDetailsCondition.Id;
                conditions.OwnerIndex = searchCommentsDetailsCondition.OwnerIndex;
                conditions.PostIndex = searchCommentsDetailsCondition.PostIndex;
                conditions.Content = searchCommentsDetailsCondition.Content;
                conditions.Created = searchCommentsDetailsCondition.Created;
                conditions.Direction = searchCommentsDetailsCondition.Direction;
                conditions.LastModified = searchCommentsDetailsCondition.LastModified;
                conditions.Pagination = searchCommentsDetailsCondition.Pagination;

                var comments = UnitOfWork.RepositoryComments.Search();
                comments = UnitOfWork.RepositoryComments.Search(comments, conditions);

                // Search accounts from database.
                var accounts = UnitOfWork.RepositoryAccounts.Search();

                // Search comments details
                var commentsDetails = from comment in comments
                    from account in accounts
                    where comment.OwnerIndex == account.Id
                    select new CommentDetailViewModel
                    {
                        Id = comment.Id,
                        Owner = account,
                        Content = comment.Content,
                        Created = comment.Created,
                        LastModified = comment.LastModified
                    };

                #endregion

                #region Comments Details sort

                commentsDetails = UnitOfWork.RepositoryComments.Sort(commentsDetails,
                    searchCommentsDetailsCondition.Direction, searchCommentsDetailsCondition.Sort);
                
                #endregion

                #region Pagination
                
                var searchResult = new SearchResult<IQueryable<CommentDetailViewModel>>();

                searchResult.Total = await commentsDetails.CountAsync();
                searchResult.Records = UnitOfWork.RepositoryComments.Paginate(commentsDetails, conditions.Pagination);

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
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