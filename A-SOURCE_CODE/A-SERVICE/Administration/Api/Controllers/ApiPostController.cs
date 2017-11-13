using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Administration.Attributes;
using Administration.ViewModels.ApiPost;
using Database.Enumerations;
using Database.Models.Entities;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels;
using Shared.ViewModels.Categories;
using Shared.ViewModels.Posts;

namespace Administration.Controllers
{
    [RoutePrefix("api/post")]
    [ApiAuthorize]
    [ApiRole(Roles.Admin)]
    public class ApiPostController : ApiParentController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiPostController(
            IUnitOfWork unitOfWork,
            ITimeService timeService,
            IIdentityService identityService,
            ILog log) : base(unitOfWork)
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
        ///     Identity service which handles analyze identity from request.
        /// </summary>
        private readonly IIdentityService _identityService;
        
        /// <summary>
        ///     Service which handles log writing.
        /// </summary>
        private readonly ILog _log;

        #endregion

        #region Methods

        /// <summary>
        ///     Initiate a post with specific information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> InitiatePost([FromBody] InitiatePostViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    // Initiate the parameter.
                    parameters = new InitiatePostViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Account identity search

                // Search account from request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account has attached to request");

                #endregion

                #region Category search

                // Search condition build.
                var findCategoryConditions = new SearchCategoryViewModel();
                findCategoryConditions.Id = parameters.CategoryIndex;

                // Search the first match category in the database.
                var categories = _unitOfWork.RepositoryCategories.Search();
                var category =
                    await _unitOfWork.RepositoryCategories.Search(categories, findCategoryConditions)
                        .FirstOrDefaultAsync();
                if (category == null)
                {
                    _log.Error($"No category (Id: {parameters.CategoryIndex}) is found in database.");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);
                }

                #endregion

                #region Post initialization

                // Initiate new post.
                var post = new Post();
                post.OwnerIndex = account.Id;
                post.CategoryIndex = parameters.CategoryIndex;
                post.Title = parameters.Title;
                post.Body = parameters.Body;
                post.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                //Add category record
                _unitOfWork.RepositoryPosts.Insert(post);

                // Save changes into database.
                await _unitOfWork.CommitAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.Created, post);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Update post information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePosts([FromUri] int index,
            [FromBody] InitiatePostViewModel parameters)
        {
            try
            {
                #region Parameters validation

                // Parameters haven't been initialized.
                if (parameters == null)
                {
                    parameters = new InitiatePostViewModel();
                    Validate(parameters);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Account identity search

                // Search account from request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account has attached to request");

                #endregion

                #region Information available check

                // Search the post by index.
                var condition = new SearchPostViewModel();
                condition.Id = index;

                // If account is not admin. It can only change its own posts.
                if (account.Role != Roles.Admin)
                    condition.OwnerIndex = account.Id;

                // Search all posts in database.
                var posts = _unitOfWork.RepositoryPosts.Search();
                posts = _unitOfWork.RepositoryPosts.Search(posts, condition);

                #endregion

                #region Update post

                // Search the current time on the system.
                var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                foreach (var post in posts)
                {
                    post.CategoryIndex = parameters.CategoryIndex;
                    post.Title = parameters.Title;
                    post.Body = parameters.Body;
                    post.LastModified = unixSystemTime;
                }

                // Save changes into database.
                await _unitOfWork.CommitAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Error occured while executing post update");
            }
        }

        /// <summary>
        ///     Delete a specific post.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        [ApiRole(Roles.Admin)]
        public async Task<HttpResponseMessage> DeletePosts([FromBody] SearchPostViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new SearchPostViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Records delete

                // Find posts by using specific conditions.
                var posts = _unitOfWork.RepositoryPosts.Search();
                posts = _unitOfWork.RepositoryPosts.Search(posts, conditions);

                // Find related post reports.
                var postReports = _unitOfWork.RepositoryPostReports.Search();
                var childrenPostReports = from post in posts
                    from postReport in postReports
                    where post.Id == postReport.PostIndex
                    select postReport;

                // Remove posts & post reports from database.
                _unitOfWork.RepositoryPosts.Remove(posts);
                _unitOfWork.RepositoryPostReports.Remove(childrenPostReports);

                // Save changes and count the number of deleted records.
                var totalRecords = await _unitOfWork.CommitAsync();

                // No record has been deleted.
                if (totalRecords < 1)
                {
                    _log.Error("No post with specific conditions is found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostNotFound);
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
        ///     Filter posts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        [ApiRole(Roles.Admin)]
        public async Task<HttpResponseMessage> FindPosts([FromBody] SearchPostViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new SearchPostViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                #endregion

                // Initiate search result.
                var searchResult = new SearchResult<IQueryable<Post>>();

                // Search posts by using specific conditions.
                var posts = _unitOfWork.RepositoryPosts.Search();
                posts = _unitOfWork.RepositoryPosts.Search(posts, conditions);

                // Count the number of condition matched records.
                searchResult.Total = await posts.CountAsync();

                // Order and paginate record.
                searchResult.Records = UnitOfWork.RepositoryPosts.Paginate(posts, conditions.Pagination);

                return Request.CreateResponse(HttpStatusCode.OK, searchResult);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        ///     Filter posts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("details")]
        [HttpGet]
        [ApiRole(Roles.Admin)]
        public async Task<HttpResponseMessage> FindPostDetails([FromUri] int index)
        {
            try
            {
                // Search all posts from database.
                var posts = _unitOfWork.RepositoryPosts.Search();
                posts = posts.Where(x => x.Id == index);

                // Search all accounts from database.
                var accounts = _unitOfWork.RepositoryAccounts.Search();

                // Search all category from database.
                var categories = _unitOfWork.RepositoryCategories.Search();

                // Search and select the first result.
                var detail = await (from post in posts
                    from account in accounts
                    from category in categories
                    select new PostDetailViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Body = post.Body,
                        Owner = account,
                        Category = category,
                        Created = post.Created,
                        LastModified = post.LastModified
                    }).FirstOrDefaultAsync();

                return Request.CreateResponse(HttpStatusCode.OK, detail);
            }
            catch (Exception exception)
            {
                _log.Error(exception.Message, exception);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        //[Route("summary/daily")]
        //[HttpPost]
        //public async Task<HttpResponseMessage> SummarizePostsDaily([FromBody] DailyPostSummaryViewModel parameters)
        //{
        //    #region Parameters validation

        //    if (parameters == null)
        //    {
        //        parameters = new DailyPostSummaryViewModel();
        //        Validate(parameters);
        //    }

        //    if (!ModelState.IsValid)
        //        return Request.CreateResponse(HttpStatusCode.BadRequest,
        //            FindValidationMessage(ModelState, nameof(parameters)));

        //    #endregion

        //    #region Result filter

        //    // Search all posts.
        //    var posts = _unitOfWork.RepositoryPosts.Search();

        //    // Convert unix start date to datetime instance.
        //    var startDate = _timeService.UnixToDateTimeUtc(parameters.StartDate);
        //    var endDate = startDate.AddDays(parameters.Days);

        //    // Convert end date back to unix date time.
        //    var unixEndDate = _timeService.DateTimeUtcToUnix(endDate);

        //    // Filter post by specific conditions.
        //    posts = posts.Where(x => x.Created >= parameters.StartDate);
        //    posts = posts.Where(x => x.Created <= unixEndDate);

        //    // Category index is specified.
        //    if (parameters.CategoryIndex != null)
        //        posts = posts.Where(x => x.CategoryIndex != parameters.CategoryIndex.Value);

        //    // Owner index is specified.
        //    if (parameters.OwnerIndex != null)
        //        posts = posts.Where(x => x.OwnerIndex == parameters.OwnerIndex.Value);

        //    #endregion

        //    var grouppedPosts = posts.GroupBy(x => x.CategoryIndex);

        //    var postSummary = grouppedPosts.Select(x => new
        //    {
        //        Id = x.Key,
        //        Count = x.Count()
        //    });

        //    var result = await postSummary.ToListAsync();
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        #endregion
    }
}