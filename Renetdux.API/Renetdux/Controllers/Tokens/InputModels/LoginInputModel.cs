using Newtonsoft.Json;

namespace Renetdux.Controllers.Tokens.InputModels
{
    public class LoginInputModel
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("username")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
