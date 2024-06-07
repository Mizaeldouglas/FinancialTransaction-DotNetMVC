using System.Text.Json.Serialization;

namespace FinancialTransaction.App.Models;

public class Transaction
{
    public int Id { get; set; }
    public string OriginBank { get; set; } = string.Empty;
    public int OriginAgency { get; set; }
    public string OriginAccount { get; set; } = string.Empty;
    public string DestinationBank { get; set; } = string.Empty;
    public int DestinationAgency { get; set; }
    public string DestinationAccount { get; set; } = string.Empty;
    public decimal TransactionValue { get; set; }
    public DateTime TransactionDate{ get; set; }
    
    [JsonIgnore]
    public DateTime ImportDateTime { get; set; }
}