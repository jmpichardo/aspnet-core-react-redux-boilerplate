using Microsoft.AspNetCore.Mvc;
using Renetdux.Controllers.Tokens.InputModels;
using Renetdux.Controllers.Tokens.Models;
using Renetdux.Controllers.Tokens.ViewModels;
using Renetdux.Infrastructure.Commands;
using Renetdux.Infrastructure.Commands.Users.Interfaces;
using Renetdux.Infrastructure.Commands.Users.Models;
using System.Threading.Tasks;

namespace Renetdux.Controllers.Tokens
{
    [Route("token")]
    public class TokenController : BaseController
    {
        private readonly IGenerateJwtCommand _generateJwtCommand;

        public TokenController(IGenerateJwtCommand generateJwtCommand)
        {
            _generateJwtCommand = generateJwtCommand;
        }

        /// <summary>
        /// Endpoint to generate a jwt token for authentication
        /// Grant Types are:
        /// refresh_token
        /// password
        /// </summary>
        /// <param name="loginInputModel"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult<TokenViewModel>> GenerateToken([FromBody] LoginInputModel loginInputModel)
        {
            ICommandResult<TokenDTO> jwtResult;
            if (loginInputModel.GrantType == GrantTypes.Password)
            {
                jwtResult = await _generateJwtCommand.ExecuteAsync(loginInputModel.Email, loginInputModel.Password);
            }
            else if (loginInputModel.GrantType == GrantTypes.RefreshToken)
            {
                jwtResult = await _generateJwtCommand.ExecuteAsync(loginInputModel.RefreshToken);
            }
            else
            {
                return BadRequest("Invalid Grant Type");
            }

            if (!jwtResult.IsSuccessful)
            {
                if (jwtResult.Error != null)
                {
                    return Unauthorized(new ErrorViewModel(jwtResult.Error.Value.Code, jwtResult.Error.Value.Message));
                }
            }

            return new TokenViewModel(jwtResult.Result);
        }
    }
}
