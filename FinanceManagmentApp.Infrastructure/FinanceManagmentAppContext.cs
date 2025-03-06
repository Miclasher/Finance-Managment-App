using FinanceManagmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagmentApp.Infrastructure
{
    public sealed class FinanceManagmentAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<JwtRefreshToken> RefreshTokens { get; set; }
        public DbSet<FinancialOperation> FinancialOperations { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public FinanceManagmentAppContext(DbContextOptions<FinanceManagmentAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceManagmentAppContext).Assembly);
        }
    }
}
