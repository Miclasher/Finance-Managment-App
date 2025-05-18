using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal sealed class MccRepository : Repository<Mcc>, IMccRepository
    {
        public MccRepository(FinanceManagmentAppContext context) : base(context)
        {
        }
    }
}