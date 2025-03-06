using FinanceManagmentApp.Domain.Repositories;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceManagmentAppContext _context;

        public UnitOfWork(FinanceManagmentAppContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
