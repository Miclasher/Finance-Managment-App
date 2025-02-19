using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task InvalidateUserToken(Guid userId, CancellationToken cancellationToken = default);
        Task ReplaceUserToken(Guid userId, string newToken, CancellationToken cancellationToken = default);
        Task<string> GetUserToken(Guid userId, CancellationToken cancellationToken = default);
    }
}
