using System;
using System.Net.Http;
using System.Web.Http;

namespace iConfess.Admin.Controllers
{
    [RoutePrefix("api/report/comment")]
    public class ApiCommentReportController : ApiController
    {
        /// <summary>
        ///     Delete all reports from a comment due to its validation.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteCommentReport()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Find list of comment reports by using specific conditions.
        /// </summary>
        /// <returns></returns>
        [Route("find")]
        [HttpPost]
        public HttpResponseMessage FilterCommentReports()
        {
            throw new NotImplementedException();
        }
    }
}