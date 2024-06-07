using FinancialTransaction.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransaction.App.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.OriginBank).IsRequired();
        builder.Property(t => t.OriginAgency).IsRequired();
        builder.Property(t => t.OriginAccount).IsRequired();
        builder.Property(t => t.DestinationBank).IsRequired();
        builder.Property(t => t.DestinationAgency).IsRequired();
        builder.Property(t => t.DestinationAccount).IsRequired();
        builder.Property(t => t.TransactionValue).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TransactionDate).IsRequired();
        builder.Property(t => t.ImportDateTime).IsRequired();
    }
}