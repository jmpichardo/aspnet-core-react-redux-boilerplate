using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Renetdux.Infrastructure.Domain.Users;
using System.Linq;
using System.Security.Claims;

namespace Renetdux.Authorization
{
    public class CompanyAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var roleAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(RolesAttribute), true).FirstOrDefault() as RolesAttribute;

                var adminUserRoleTypes = roleAttribute?.Roles;

                if (adminUserRoleTypes != null && adminUserRoleTypes.Any())
                {
                    var claims = context.HttpContext.User.Claims;

                    var roleJsonFromClaims = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                    if (roleJsonFromClaims == null)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    var roles = JsonConvert.DeserializeObject<UserRoles[]>(roleJsonFromClaims);
                    if (!roles.Any())
                    {
                        context.Result = new ForbidResult();
                        return;
                    }

                    if (roles.All(x => !adminUserRoleTypes.Contains(x)))
                    {
                        context.Result = new ForbidResult();
                    }
                }
            }
        }
    }
}
