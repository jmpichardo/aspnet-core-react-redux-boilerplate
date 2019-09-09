using Microsoft.IdentityModel.Tokens;
using Renetdux.Infrastructure.Commands.Users.Interfaces;
using Renetdux.Infrastructure.Commands.Users.Models;
using Renetdux.Infrastructure.Common;
using Renetdux.Infrastructure.Domain.Users;
using Renetdux.Infrastructure.Repositories;
using Renetdux.Infrastructure.Repositories.Users;
using Renetdux.Infrastructure.Services.Encryption;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Commands.Users.Commands
{
    public class GenerateJwtCommand : IGenerateJwtCommand
    {
        private const int C_EXPIRES_IN_SECONDS = 900;   // 15 minutes tokens

        private readonly Configuration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;

        public GenerateJwtCommand(
            Configuration configuration, 
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork,
            IEncryptionService encryptionService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
        }

        public async Task<ICommandResult<TokenDTO>> ExecuteAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new CommandResult<TokenDTO>(ErrorCodes.User_Login_Empty_Email.ToString(), "Empty Email");

            if (string.IsNullOrWhiteSpace(password))
                return new CommandResult<TokenDTO>(ErrorCodes.User_Login_Empty_Password.ToString(), "Empty Password");

            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            if (user == null)
                return new CommandResult<TokenDTO>(ErrorCodes.User_Login_Invalid_Email.ToString(), "Invalid Email");

            var validPassword = _encryptionService.ValidatePassword(user.Password, password);
            if (!validPassword)
                return new CommandResult<TokenDTO>(ErrorCodes.User_Login_Invalid_Password.ToString(), "Invalid Password");

            var token = GenerateToken(user);
            
            user.GenerateRefreshToken();
            await _unitOfWork.CommitAsync();

            return new CommandResult<TokenDTO>(new TokenDTO(token, user.RefreshToken, C_EXPIRES_IN_SECONDS));
        }

        public async Task<ICommandResult<TokenDTO>> ExecuteAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return new CommandResult<TokenDTO>(ErrorCodes.User_Login_Empty_RefreshToken.ToString(), "Empty refresh token");

            var user = await _userRepository.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null)
                return new CommandResult<TokenDTO>(ErrorCodes.User_Login_Invalid_RefreshToken.ToString(), "Invalid Refresh Token");

            var token = GenerateToken(user);

            user.GenerateRefreshToken();
            await _unitOfWork.CommitAsync();

            return new CommandResult<TokenDTO>(new TokenDTO(token, user.RefreshToken, C_EXPIRES_IN_SECONDS));
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.JwtSecret);

            var userClaims = new UserClaim(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims.GenerateClaims()),
                Expires = DateTime.UtcNow.AddSeconds(C_EXPIRES_IN_SECONDS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
