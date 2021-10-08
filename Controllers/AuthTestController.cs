using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
