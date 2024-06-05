using Microsoft.EntityFrameworkCore;

namespace FinancialTransaction.App.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
}