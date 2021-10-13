using Auth2.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Auth2.Api
{
    public class AuthService : IAuthService
    {
        public UserModel Authenticate(LoginModel model)
        {
            if (model.Username == "admin" && model.Password == "admin")
                return new UserModel
                {
                    Id = 15,
                    FirstName = "Kostia",
                    LastName = "Mick",
                    Role = "Admin"
                };

            if (model.Username == "user1" && model.Password == "kkeerr22")
                return new UserModel
                {
                    Id = 1,
                    FirstName = "Vlad",
                    LastName = "Dub",
                    Role = "User"
                };

            if (model.Username == "user2" && model.Password == "mmyyll55")
                return new UserModel
                {
                    Id = 2,
                    FirstName = "Roman",
                    LastName = "Sokol",
                    Role = "User"
                };

            return null;
        }

        public List<Claim> CreateClaims(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.Name, user.FullName),
            };

            if (user.Role == "Admin")
            {
                claims.Add(new Claim("permissions", nameof(PermissionType.AccessAdmin)));
                claims.Add(new Claim("permissions", nameof(PermissionType.AccessUser)));
            }
            else
            {
                claims.Add(new Claim("permissions", nameof(PermissionType.AccessUser)));
            }

            return claims;
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
