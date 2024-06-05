using Microsoft.AspNetCore.Mvc;

namespace FinancialTransaction.App.Controllers;

public class ImportTransactionsController : Controller
{
    // GET: ImportTransactionsController
    public ActionResult Index()
    {
        
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
        if (file == null) return RedirectToAction("Error");
        
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                Console.WriteLine($"Banco origem: {values[0]}");
                Console.WriteLine($"Agência origem: {values[1]}");
                Console.WriteLine($"Conta origem: {values[2]}");
                Console.WriteLine($"Banco destino: {values[3]}");
                Console.WriteLine($"Agência destino: {values[4]}");
                Console.WriteLine($"Conta destino: {values[5]}");
                Console.WriteLine($"Valor da transação: {values[6]}");
                Console.WriteLine($"Data e hora da transação: {values[7]}");
            }
        }

        return RedirectToAction("Index");
    }

}