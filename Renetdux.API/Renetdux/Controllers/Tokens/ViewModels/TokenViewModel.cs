using Newtonsoft.Json;
using Renetdux.Infrastructure.Commands.Users.Models;

namespace Renetdux.Controllers.Tokens.ViewModels
{
    public class TokenViewModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public TokenViewModel(TokenDTO token)
        {
            AccessToken = token.Token;
            RefreshToken = token.RefreshToken;
            ExpiresIn = token.ExpiresIn;
        }
    }
}
