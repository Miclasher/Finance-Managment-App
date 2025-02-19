using FinanceManagmentApp.Domain.Entities;

namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string username, CancellationToken cancellationToken = default);
        Task<bool> UsernameExists(string username, CancellationToken cancellationToken = default);
    }
}
