using Newtonsoft.Json;

namespace Administration.ViewModels
{
    public class TokenGeneralViewModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expire")]
        public int Expire { get; set; }
    }
}