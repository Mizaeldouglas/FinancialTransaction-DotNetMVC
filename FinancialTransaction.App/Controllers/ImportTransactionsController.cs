using System.Globalization;
using FinancialTransaction.App.Data;
using FinancialTransaction.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransaction.App.Controllers;

public class ImportTransactionsController : Controller
{
    
    private readonly AppDbContext _context;

    public ImportTransactionsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: ImportTransactionsController
    public ActionResult Index()
    {
        var importHistories = _context.ImportHistories
            .OrderByDescending(h => h.ImportDateTime)
            .ToList(); 
        
        var transactions = _context.Transactions
            .OrderByDescending(t => t.ImportDateTime) 
            .ThenByDescending(t => t.TransactionDate) 
            .ToList();

        ViewBag.ImportHistories = importHistories;
        ViewBag.Transactions = transactions; 
        
        return View();
    }
    public ActionResult Error()
    {
        Console.WriteLine("ERROR");
        return View();
    }

        
    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return RedirectToAction("Index", new { message = "Erro: Arquivo CSV vazio!" });
        }

        bool isFirstTransaction = true;
        DateTime transactionDate = DateTime.MinValue;
        

        try
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Validação de informações obrigatórias
                    if (string.IsNullOrEmpty(values[0]) ||
                        string.IsNullOrEmpty(values[1]) ||
                        string.IsNullOrEmpty(values[2]) ||
                        string.IsNullOrEmpty(values[3]) ||
                        string.IsNullOrEmpty(values[4]) ||
                        string.IsNullOrEmpty(values[5]) ||
                        string.IsNullOrEmpty(values[6]) ||
                        string.IsNullOrEmpty(values[7]))
                    {
                        Console.WriteLine($"Transação ignorada: Informação faltando ({line})");
                        continue;
                    }

                    // Verificação da data da transação
                    transactionDate = DateTime.ParseExact(values[7], "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture);

                    if (isFirstTransaction)
                    {
                        isFirstTransaction = false;
                    }
                    else if (transactionDate != DateTime.ParseExact(values[7], "yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture))
                    {
                        // Transação com data diferente, ignorar
                        Console.WriteLine($"Transação ignorada: Data inválida ({values[7]})");
                        continue;
                    }

                    // Verificação de transações duplicadas
                    var existingTransaction = _context.Transactions
                        .FirstOrDefault(t => t.OriginBank == values[0] &&
                                             t.OriginAgency == int.Parse(values[1]) &&
                                             t.OriginAccount == values[2] &&
                                             t.DestinationBank == values[3] &&
                                             t.DestinationAgency == int.Parse(values[4]) &&
                                             t.DestinationAccount == values[5] &&
                                             t.TransactionDate == transactionDate);

                    if (existingTransaction != null)
                    {
                        // Transação duplicada, ignorar
                        Console.WriteLine($"Transação ignorada: Duplicada ({values[0]}, {values[1]}, {values[2]}, {values[3]}, {values[4]}, {values[5]}, {transactionDate})");
                        continue;
                    }

                    // Criar nova transação e salvar no banco de dados
                    var transaction = new Transaction
                    {
                        OriginBank = values[0],
                        OriginAgency = int.Parse(values[1]),
                        OriginAccount = values[2],
                        DestinationBank = values[3],
                        DestinationAgency = int.Parse(values[4]),
                        DestinationAccount = values[5],
                        TransactionValue = decimal.Parse(values[6], CultureInfo.InvariantCulture),
                        TransactionDate = transactionDate,
                        ImportDateTime = DateTime.Now
                    };

                    _context.Transactions.Add(transaction);
                }

                _context.SaveChanges(); 
            }
            var importHistory = new ImportHistory
            {
                ImportDateTime = DateTime.Now,
                FileName = file.FileName 
            };
            _context.ImportHistories.Add(importHistory);
            _context.SaveChanges(); 

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Handle any exceptions during processing
            Console.WriteLine($"Error importing CSV: {ex.Message}");
            return RedirectToAction("Error");
        }
    }

}