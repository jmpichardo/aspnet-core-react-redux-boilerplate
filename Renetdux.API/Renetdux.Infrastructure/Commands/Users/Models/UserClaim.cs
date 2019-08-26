using Newtonsoft.Json;
using Renetdux.Infrastructure.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Renetdux.Infrastructure.Commands.Users.Models
{
    public class UserClaim
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public List<UserRoles> Roles { get; set; }

        public UserClaim(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            Email = user.Email;
            Roles = new List<UserRoles>() { user.UserRole };
        }

        public UserClaim(ClaimsPrincipal user)
        {
            UserId = int.Parse(user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
            FirstName = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            Email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            Roles = user.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => (UserRoles)Enum.Parse(typeof(UserRoles), x.Value, true)).ToList();
        }

        public Claim[] GenerateClaims()
        {
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
                new Claim(ClaimTypes.GivenName, FirstName),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(Roles)),
            };
        }
    }
}
