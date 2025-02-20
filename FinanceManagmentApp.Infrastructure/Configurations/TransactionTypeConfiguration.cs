using FinanceManagmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManagmentApp.Infrastructure.Configurations
{
    internal sealed class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.IsExpense)
                .IsRequired();

            builder.HasMany(e => e.FinancialOperations)
                .WithOne(e => e.TransactionType)
                .HasForeignKey(e => e.TransactionTypeId);
        }
    }
}
