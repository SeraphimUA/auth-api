using Auth2.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthCookieController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthCookieController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            var user = authService.Authenticate(model);
            if (user is null)
            {
                return Unauthorized();
            }

            var principal = authService.CreatePrincipal(user, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                null);

            return Ok();
        }
    }
}
