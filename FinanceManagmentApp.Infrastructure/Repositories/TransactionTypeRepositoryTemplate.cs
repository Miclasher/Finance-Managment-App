using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Domain.Repositories;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    internal class TransactionTypeRepositoryTemplate: Repository<TransactionTypeTemplate>, ITransactionTypeTemplateRepository
    {
        public TransactionTypeRepositoryTemplate(FinanceManagmentAppContext context) : base(context)
        {
        }
    }
}