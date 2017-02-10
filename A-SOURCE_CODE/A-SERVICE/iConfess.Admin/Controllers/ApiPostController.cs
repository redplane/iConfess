using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Admin.Attributes;
using iConfess.Database.Models.Tables;
using log4net;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.Categories;
using Shared.ViewModels.Posts;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/post")]
    [ApiAuthorize]
    public class ApiPostController : ApiController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        /// <param name="identityService"></param>
        /// <param name="log"></param>
        public ApiPostController(IUnitOfWork unitOfWork,
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

                // Find account from request.
                var account = _identityService.FindAccount(Request.Properties);
                if (account == null)
                    throw new Exception("No account has attached to request");

                #endregion

                #region Category search

                // Search condition build.
                var findCategoryConditions = new FindCategoriesViewModel();
                findCategoryConditions.Id = parameters.CategoryIndex;

                // Find the first match category in the database.
                var category = await _unitOfWork.RepositoryCategories.FindCategoryAsync(findCategoryConditions);
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
                _unitOfWork.RepositoryPosts.Initiate(post);

                #endregion

                // Save changes into database.
                await _unitOfWork.CommitAsync();
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

                #region Information available check

                // Find the post by index.
                var findPost = new FindPostViewModel();
                findPost.Id = index;
                var findPostResult = await _unitOfWork.RepositoryPosts.FindPostsAsync(findPost);

                // Result availability validate
                if ((findPostResult == null) || (findPostResult.Total != 1))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.PostNotFound);

                #endregion

                #region Update post

                // Find the current time on the system.
                var unixSystemTime = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                foreach (var post in findPostResult.Posts)
                {
                    post.CategoryIndex = parameters.CategoryIndex;
                    post.Title = parameters.Title;
                    post.Body = parameters.Body;
                    post.LastModified = unixSystemTime;
                }

                // Save changes into database.
                await _unitOfWork.Context.SaveChangesAsync();

                #endregion

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Error occured while executing category");
            }
        }

        /// <summary>
        ///     Delete a specific post.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeletePosts([FromBody] FindPostViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindPostViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                #endregion

                #region Records delete

                // Delete categories by using specific conditions.
                _unitOfWork.RepositoryPosts.Delete(conditions);

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
        public async Task<HttpResponseMessage> FindPosts([FromBody] FindPostViewModel conditions)
        {
            try
            {
                #region Parameters validation

                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindPostViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                #endregion

                // Find posts by using specific conditions.
                var response = await _unitOfWork.RepositoryPosts.FindPostsAsync(conditions);

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