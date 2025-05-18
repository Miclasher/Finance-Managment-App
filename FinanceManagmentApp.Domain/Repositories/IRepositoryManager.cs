namespace FinanceManagmentApp.Domain.Repositories
{
    public interface IRepositoryManager
    {
        public ITransactionTypeRepository TransactionType { get; }
        public IFinancialOperationRepository FinancialOperation { get; }
        public IUserRepository User { get; }
        public IJwtRefreshTokenRepository RefreshToken { get; }
        public IMccRepository Mcc { get; }
        public ITransactionTypeTemplateRepository TransactionTypeTemplate { get; }
        public IUnitOfWork UnitOfWork { get; }
    }
}
