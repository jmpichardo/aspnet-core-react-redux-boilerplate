using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Renetdux.Controllers.Users.InputModels;
using Renetdux.Controllers.Users.ViewModels;
using Renetdux.Infrastructure.Commands.Users.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Renetdux.Controllers.Users
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IGetUsersCommand _getUsersCommand;
        private readonly IGetUserCommand _getUserCommand;
        private readonly IRegisterUserCommand _registerUserCommand;

        public UsersController(
            IGetUsersCommand getUsersCommand,
            IGetUserCommand getUserCommand,
            IRegisterUserCommand registerUserCommand)
        {
            _getUsersCommand = getUsersCommand;
            _getUserCommand = getUserCommand;
            _registerUserCommand = registerUserCommand;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<List<UserViewModel>>> GetUsers()
        {
            var userIdFromClaims = GetUserId();

            var result = await _getUsersCommand.ExecuteAsync();
            if (!result.IsSuccessful)
                return BadRequest(new ErrorViewModel(result.Error.Value.Code, result.Error.Value.Message));

            return new List<UserViewModel>(result.Result.Select(u => new UserViewModel(u)));
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserViewModel>> GetUser([FromRoute]int userId)
        {
            var result = await _getUserCommand.ExecuteAsync(userId);
            if (!result.IsSuccessful)
                return BadRequest(new ErrorViewModel(result.Error.Value.Code, result.Error.Value.Message));

            return new UserViewModel(result.Result);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserViewModel>> RegisterUser([FromBody]UserInputModel user)
        {
            var result = await _registerUserCommand.ExecuteAsync(user.ToDTO());
            if (!result.IsSuccessful)
                return BadRequest(new ErrorViewModel(result.Error.Value.Code, result.Error.Value.Message));

            return new UserViewModel(result.Result);
        }
    }
}
