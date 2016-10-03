using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Write response information to HttpContext.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="source"></param>
        void RespondHttpMessage(HttpResponse httpResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK, object source = null);

        /// <summary>
        /// Write response information to HttpContext.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        Task RespondHttpMessageAsync(HttpResponse httpResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK,
            object source = null);
    }
}
