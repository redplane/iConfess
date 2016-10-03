using Newtonsoft.Json;

namespace Core.ViewModels
{
    public class HttpResponseViewModel
    {
        /// <summary>
        /// List of messages (this is used for constructing validation messages)
        /// </summary>
        [JsonProperty("messages", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string [] Messages { get; set; }

        /// <summary>
        /// Message from service.
        /// </summary>
        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }
    }
}