using Newtonsoft.Json;

namespace Core.ViewModels
{
    public class TokenDetailViewModel
    {
        /// <summary>
        /// Email which is used for logging into system.
        /// </summary>
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Encrypted password which is used for logging into system.
        /// </summary>
        [JsonProperty(PropertyName = "expire_in")]
        public int ExpireIn { get; set; }
    }
}
