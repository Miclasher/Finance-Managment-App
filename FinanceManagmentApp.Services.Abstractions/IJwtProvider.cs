namespace FinanceManagmentApp.Services.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(Guid userId, IEnumerable<string> roles);
        string GenerateRefreshToken();
    }
}
