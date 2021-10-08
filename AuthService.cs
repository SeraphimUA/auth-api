using Auth2.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth2.Api
{
    public class AuthService : IAuthService
    {
        public UserModel Authenticate(LoginModel model)
        {
            return model.Username != "admin" || model.Password != "admin"
                ? null
                : new UserModel
                {
                    Id = 15,
                    FirstName = "Kostia",
                    LastName = "Mick"
                };
        }

        public List<Claim> CreateClaims(UserModel user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
            };
        }

        public ClaimsPrincipal CreatePrincipal(UserModel user, string authScheme)
        {
            var claims = CreateClaims(user);

            var identity = new ClaimsIdentity(claims, authScheme);

            return new ClaimsPrincipal(identity);
        }

        public string CreateJwt(List<Claim> claims, string signingKey)
        {
            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(
                notBefore: now,
                expires: now.AddDays(365),
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(signingKey)),
                            SecurityAlgorithms.HmacSha256)
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}
