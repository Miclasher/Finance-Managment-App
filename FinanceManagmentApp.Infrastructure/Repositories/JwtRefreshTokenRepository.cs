﻿using FinanceManagmentApp.Domain.Entities;
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

        public async Task<JwtRefreshToken> GetTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            var tokenObject = await _context.RefreshTokens
                .Include(e => e.User)
                .ThenInclude(e => e.Roles)
                .Where(e => e.Token == token)
                .FirstOrDefaultAsync(cancellationToken);

            return tokenObject!;
        }

        public async Task InvalidateUserTokenAsync(Guid userId, CancellationToken cancellationToken = default)
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

        public async Task ReplaceUserTokenAsync(Guid userId, JwtRefreshToken newToken, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(newToken, nameof(newToken));

            await InvalidateUserTokenAsync(userId, cancellationToken);

            await _context.RefreshTokens.AddAsync(newToken, cancellationToken);
        }
    }
}
