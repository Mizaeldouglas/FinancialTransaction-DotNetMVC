namespace FinancialTransaction.App.Models;

public class ImportHistory
{
    public int Id { get; set; }
    public DateTime ImportDateTime { get; set; }
    public string FileName { get; set; }
}