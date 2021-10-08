using Auth2.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTokenController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IConfiguration configuration;
        private const string authScheme = JwtBearerDefaults.AuthenticationScheme;

        public AuthTokenController(IAuthService authService, IConfiguration configuration)
        {
            this.authService = authService;
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var user = authService.Authenticate(model);
            if (user is null)
            {
                return Unauthorized();
            }

            var claims = authService.CreateClaims(user);
            string encodedJwt = authService.CreateJwt(claims, configuration["SigningKey"]);

            return Ok(new { Token = encodedJwt });
        }
    }
}

