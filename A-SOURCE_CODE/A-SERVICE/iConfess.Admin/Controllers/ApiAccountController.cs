using System;
using System.Net.Http;
using System.Web.Http;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/account")]
    public class ApiAccountController : ApiController
    {
        /// <summary>
        /// Check account information and sign user into system as the information is valid.
        /// </summary>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Submit request to service to receive an instruction email about finding account password.
        /// </summary>
        /// <returns></returns>
        [Route("lost_password")]
        [HttpGet]
        public HttpResponseMessage RequestFindLostPassword()
        {
            throw new NotImplementedException();    
        }

        /// <summary>
        /// Submit a new password which will replace the old password
        /// </summary>
        /// <returns></returns>
        [Route("lost_password")]
        [HttpPost]
        public HttpResponseMessage SubmitAlternativePassword()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find list of accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public HttpResponseMessage FindAccounts()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permanantly or temporarily ban accounts by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("forbid")]
        [HttpPut]
        public HttpResponseMessage ForbidAccountAccess()
        {
            throw new NotImplementedException();
        }
        
    }
}