using Microsoft.AspNetCore.Authorization;
using Renetdux.Infrastructure.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Renetdux.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RolesAttribute : AuthorizeAttribute
    {
        public new readonly List<UserRoles> Roles;

        public RolesAttribute(params UserRoles[] roles)
        {
            Roles = roles.ToList();
        }
    }
}
