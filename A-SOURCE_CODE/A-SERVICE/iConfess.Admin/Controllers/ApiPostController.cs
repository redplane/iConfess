using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iConfess.Database.Models.Tables;
using Shared.Interfaces.Services;
using Shared.Resources;
using Shared.ViewModels.Categories;
using Shared.ViewModels.Posts;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/post")]
    public class ApiPostController : ApiController
    {
        #region Controllers

        /// <summary>
        ///     Initiate controller with
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="timeService"></param>
        public ApiPostController(IUnitOfWork unitOfWork, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
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

                #region Information availability check

                // TODO: Find owner index from request.
                var ownerIndex = 1;

                // Find category from database.
                var findCategory = new FindCategoriesViewModel();
                findCategory.Id = parameters.CategoryIndex;

                // Find the category which matches with the index.
                var findCategoriesResult = await _unitOfWork.RepositoryCategories.FindCategoriesAsync(findCategory);
                if ((findCategoriesResult == null) || (findCategoriesResult.Total != 1))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);

                #endregion

                // Initiate new post.
                var post = new Post();
                post.OwnerIndex = ownerIndex;
                post.CategoryIndex = parameters.CategoryIndex;
                post.Title = parameters.Title;
                post.Body = parameters.Body;
                post.Created = _timeService.DateTimeUtcToUnix(DateTime.UtcNow);

                //Add category record
                post = await _unitOfWork.RepositoryPosts.InitiatePostAsync(post);

                return Request.CreateResponse(HttpStatusCode.Created, post);
            }
            catch (Exception exception)
            {
                // TODO: Write log.
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
                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindPostViewModel();
                    Validate(conditions);
                }

                //Request parameters are invalid
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                // Delete categories by using specific conditions.
                var totalRecords = await _unitOfWork.RepositoryPosts.DeletePostsAsync(conditions);

                // No record has been deleted.
                if (totalRecords < 1)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.CategoryNotFound);

                // Tell the client , deletion is successful.
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                // TODO: Add log
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
                // Conditions haven't been initialized.
                if (conditions == null)
                {
                    conditions = new FindPostViewModel();
                    Validate(conditions);
                }

                // Parameters are invalid.
                if (!ModelState.IsValid)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                // Find categories by using specific conditions.
                var response = await _unitOfWork.RepositoryPosts.DeletePostsAsync(conditions);

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception exception)
            {
                // TODO: Add log
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        #endregion
    }
}