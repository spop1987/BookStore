using System.Security.Claims;

namespace BookStore.Service.TokenService
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> calims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}