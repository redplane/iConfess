using System;
using System.Net.Http;
using System.Web.Http;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/category")]
    public class ApiCategoryController : ApiController
    {
        /// <summary>
        /// Initiate a category with specific information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public HttpResponseMessage InitiateCategory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update category information.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public HttpResponseMessage UpdateCategory()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a specific category.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public HttpResponseMessage DeleteCategories()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Filter categories by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public HttpResponseMessage FindCategories()
        {
            throw new NotImplementedException();
        }
    }
}