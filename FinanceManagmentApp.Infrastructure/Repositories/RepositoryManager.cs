using FinanceManagmentApp.Domain.Repositories;

namespace FinanceManagmentApp.Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly FinanceManagmentAppContext _context;
        private readonly Lazy<ITransactionTypeRepository> _lazyTransactionTypeRepository;
        private readonly Lazy<IFinancialOperationRepository> _lazyFinancialOperationRepository;
        private readonly Lazy<IUserRepository> _lazyUserRepository;
        private readonly Lazy<IJwtRefreshTokenRepository> _lazyRefreshTokenRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoryManager(FinanceManagmentAppContext context)
        {
            _context = context;
            _lazyTransactionTypeRepository = new Lazy<ITransactionTypeRepository>(() => new TransactionTypeRepository(context));
            _lazyFinancialOperationRepository = new Lazy<IFinancialOperationRepository>(() => new FinancialOperationRepository(context));
            _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(context));
            _lazyRefreshTokenRepository = new Lazy<IJwtRefreshTokenRepository>(() => new JwtRefreshTokenRepository(context));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(context));
        }

        public ITransactionTypeRepository TransactionType => _lazyTransactionTypeRepository.Value;

        public IFinancialOperationRepository FinancialOperation => _lazyFinancialOperationRepository.Value;

        public IUserRepository User => _lazyUserRepository.Value;

        public IJwtRefreshTokenRepository RefreshToken => _lazyRefreshTokenRepository.Value;

        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
    }
}
