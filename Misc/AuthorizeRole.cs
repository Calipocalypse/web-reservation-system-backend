using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using Wsr.Models.Authentication.Enums;

namespace Wsr.Misc
{
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params UserRole[] userRoles)
        {
            var allowedRolesAsStrings = userRoles.Select(x => Enum.GetName(typeof(UserRole), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
