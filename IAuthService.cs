using Auth2.Api.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace Auth2.Api
{
    public interface IAuthService
    {
        UserModel Authenticate(LoginModel model);
        ClaimsPrincipal CreatePrincipal(UserModel model, string authScheme);
        List<Claim> CreateClaims(UserModel user);
        string CreateJwt(List<Claim> claims, string signingKey);
    }
}