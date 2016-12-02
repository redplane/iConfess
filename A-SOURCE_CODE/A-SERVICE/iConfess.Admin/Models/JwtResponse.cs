using Newtonsoft.Json;

namespace iConfess.Admin.Models
{
    public class JwtResponse
    {
        /// <summary>
        ///     Token which is used for accessing into system.
        /// </summary>
        [JsonProperty("access_token")]
        public string Token { get; set; }

        /// <summary>
        ///     Type of token.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     When should the token be expired.
        /// </summary>
        [JsonProperty("expires_at")]
        public double Expire { get; set; }
    }
}