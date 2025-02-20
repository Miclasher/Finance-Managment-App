using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class JwtRefreshTokenRepository : IJwtRefreshTokenRepository
    {
        private readonly FinanceManagmentAppContext _context;

        public JwtRefreshTokenRepository(FinanceManagmentAppContext context)
        {
            _context = context;
        }

        public async Task<JwtRefreshToken> GetUserToken(Guid userId, CancellationToken cancellationToken = default)
        {
            return (await _context.RefreshTokens.Where(e => e.UserId == userId).FirstOrDefaultAsync(cancellationToken))!;
        }

        public async Task InvalidateUserToken(Guid userId, CancellationToken cancellationToken = default)
        {
            var tokenToInvalidate = await _context.RefreshTokens
                .Where(e => e.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (tokenToInvalidate is null)
            {
                return;
            }

            _context.RefreshTokens.Remove(tokenToInvalidate);
        }

        public async Task ReplaceUserToken(Guid userId, JwtRefreshToken newToken, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newToken, nameof(newToken));

            await InvalidateUserToken(userId, cancellationToken);

            await _context.RefreshTokens.AddAsync(newToken, cancellationToken);
        }
    }
}
