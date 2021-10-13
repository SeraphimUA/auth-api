using Microsoft.AspNetCore.Mvc;
using System;

namespace Auth2.Api.Auth
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RequirePermissionAttribute : TypeFilterAttribute
    {
        public RequirePermissionAttribute(params PermissionType[] permissions) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permissions };
        }
    }
}
