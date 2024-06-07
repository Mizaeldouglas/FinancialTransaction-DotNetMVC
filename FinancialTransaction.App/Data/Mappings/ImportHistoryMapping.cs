using FinancialTransaction.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransaction.App.Data.Mappings;

public class ImportHistoryMapping : IEntityTypeConfiguration<ImportHistory>
{
    public void Configure(EntityTypeBuilder<ImportHistory> builder)
    {
        builder.ToTable("ImportHistory");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ImportDateTime).IsRequired();
        builder.Property(i => i.FileName);
    }
}