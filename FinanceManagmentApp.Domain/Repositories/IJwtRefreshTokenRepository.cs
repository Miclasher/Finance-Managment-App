using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IJwtRefreshTokenRepository
    {
        Task InvalidateUserToken(Guid userId, CancellationToken cancellationToken = default);
        Task ReplaceUserToken(Guid userId, JwtRefreshToken newToken, CancellationToken cancellationToken = default);
        Task<JwtRefreshToken> GetUserToken(Guid userId, CancellationToken cancellationToken = default);
    }
}
