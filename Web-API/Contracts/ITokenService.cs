using System.Security.Claims;
using Web_API.Others;

namespace Web_API.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        ClaimVM ExtractClaimsFromJwt(string token);
    }
}
