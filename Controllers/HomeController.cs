using System.Diagnostics;
using books.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books.Controllers
{
    public class HomeController : Controller
    {

        moviesDbcontext db;
        public HomeController(moviesDbcontext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var result = db.categrayfilm.ToList();
            return View(result);
        }

        public IActionResult about()
        {
            return View();
        }
        public IActionResult contact()
        {

            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
        [HttpPost]
        public IActionResult savecontact(Contact model)
        {
            db.Contact.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult deletefilm(int id)
        {
            var films = db.film.Find(id);
            db.film.Remove(films);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult savefilm(Film model2)
        {
            db.film.Add(model2);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult film(int id)
        {
            var result = db.film.Where(x => x.categrayfilmid == id).ToList();

            return View(result);
        }
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
