﻿using FinanceManagmentApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceManagmentApp.Infrastructure.Configurations
{
    internal sealed class MccsConfiguration : IEntityTypeConfiguration<Mcc>
    {
        public void Configure(EntityTypeBuilder<Mcc> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Value)
                .IsRequired();

            builder.HasMany(e => e.TransactionTypes)
                .WithMany(e => e.Mccs);

            builder.HasMany(e => e.TransactionTypeTemplates)
                .WithMany(e => e.Mccs);
        }
    }
}
