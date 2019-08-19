using Microsoft.AspNetCore.Mvc;
using Renetdux.Infrastructure.Commands.Users.Models;

namespace Renetdux.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected UserClaim UserClaim => new UserClaim(User);

        protected int GetUserId()
        {
            return UserClaim.UserId;
        }
    }
}
