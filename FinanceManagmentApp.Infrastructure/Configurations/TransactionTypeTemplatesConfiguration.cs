using FinanceManagmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagmentApp.Infrastructure.Configurations
{
    internal sealed class TransactionTypeTemplatesConfiguration : IEntityTypeConfiguration<TransactionTypeTemplate>
    {
        public void Configure(EntityTypeBuilder<TransactionTypeTemplate> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.IsExpense)
                .IsRequired();

            builder.HasMany(e => e.Mccs)
                .WithMany(e => e.TransactionTypeTemplates);
        }
    }
}
