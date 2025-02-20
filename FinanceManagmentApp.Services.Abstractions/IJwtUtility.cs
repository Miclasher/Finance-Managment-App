using System.Security.Claims;

namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IJwtUtility
    {
        string GenerateAccessToken(Guid userId, IEnumerable<string> roles);
        string GenerateRefreshToken();
        Guid GetUserIdFromJwt(ClaimsPrincipal claimsPrincipal);
    }
}
