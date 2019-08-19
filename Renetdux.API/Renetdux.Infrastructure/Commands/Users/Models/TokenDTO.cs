namespace Renetdux.Infrastructure.Commands.Users.Models
{
    public class TokenDTO
    {
        public string Token { get; private set; }
        public string RefreshToken { get; private set; }
        public int ExpiresIn { get; private set; }

        public TokenDTO(string token, string refreshToken, int expiresIn)
        {
            Token = token;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
        }
    }
}
