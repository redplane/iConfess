using System;
using System.Net.Http;
using System.Web.Http;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/report/post")]
    public class ApiPostReportController : ApiController
    {
        /// <summary>
        ///     Delete reports about specific post (using conditions for search)
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpDelete]
        public HttpResponseMessage DeletePostReports()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Find
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public HttpResponseMessage FindPostReports()
        {
            throw new NotImplementedException();
        }
    }
}