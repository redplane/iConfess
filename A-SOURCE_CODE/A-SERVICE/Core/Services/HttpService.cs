using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Core.Services
{
    public class HttpService : IHttpService
    {
        /// <summary>
        /// Create a message respond with specific information.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="source"></param>
        public void RespondHttpMessage(HttpResponse httpResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK, object source = null)
        {
            // Attach status code to response body.
            httpResponse.StatusCode = (int) httpStatusCode;
            httpResponse.ContentType = "application/json";

            if (source == null) return;

            // Serialize object to string.
            var serializedInformation = JsonConvert.SerializeObject(source);
            using (var streamWriter = new StreamWriter(httpResponse.Body))
            {
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(serializedInformation);
            }
        }

        /// <summary>
        /// Create a response into HttpContext.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="httpStatusCode"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public async Task RespondHttpMessageAsync(HttpResponse httpResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK,
            object source = null)
        {
            // Attach status code to response body.
            httpResponse.StatusCode = (int)httpStatusCode;
            httpResponse.ContentType = "application/json";

            // Response body is null, no need to write anything here.
            if (source == null) return;

            // Serialize object to string.
            var serializedInformation = JsonConvert.SerializeObject(source);
            using (var streamWriter = new StreamWriter(httpResponse.Body))
            {
                streamWriter.AutoFlush = true;
                await streamWriter.WriteLineAsync(serializedInformation);
            }
        }
        
    }
}