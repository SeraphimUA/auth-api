using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Auth2.Api.Auth
{
    public class PermissionFilter : IAuthorizationFilter
    {
        private readonly PermissionType[] _permissions;

        public PermissionFilter(PermissionType[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var hasAccess =
                _permissions.All(p =>
                    user.HasClaim("permissions", p.ToString()));

            if (!hasAccess)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
