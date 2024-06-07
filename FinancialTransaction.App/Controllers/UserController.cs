using FinancialTransaction.App.Data;
using FinancialTransaction.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransaction.App.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserController
        public ActionResult Index()
        {
           var users = _context.Users
               .OrderByDescending(u => u.Id)
               .ToList();
           
           ViewBag.Users = users;
           
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = new string(Enumerable.Range(0, 8).Select(x => (char)new Random().Next(65, 90)).ToArray());
                    
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao criar o usuário: " + ex.Message);
                }
            }

            return View(user);
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
