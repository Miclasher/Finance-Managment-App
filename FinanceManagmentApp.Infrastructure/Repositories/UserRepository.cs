using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(FinanceManagmentAppContext context) : base(context)
        {
        }

        public override async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.Include(e => e.Roles).Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken))!;
        }

        public async Task<User> GetByUsername(string username, CancellationToken cancellationToken = default)
        {
            return (await _dbSet.Where(e => e.Username == username).FirstOrDefaultAsync(cancellationToken))!;
        }

        public async Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(e => e.Username == username).AnyAsync(cancellationToken);
        }
    }
}
