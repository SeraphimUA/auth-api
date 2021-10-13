using Auth2.Api.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var userIdentity = HttpContext.User.Identity;
            if (userIdentity.IsAuthenticated)
            {
                var username = userIdentity.Name;
                return Ok(username);
            }
            else return Unauthorized();
        }

        [HttpGet("admin")]
        [RequirePermission(PermissionType.AccessAdmin)]
        public IActionResult GetAccessAdmin()
        {
            return Ok();
        }

        [HttpGet("user")]
        [RequirePermission(PermissionType.AccessUser)]
        public IActionResult GetAccessUser()
        {
            return Ok();
        }
    }
}
