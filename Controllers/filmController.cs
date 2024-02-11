using books.Models;
using books.Models.intervace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace books.Controllers
{
    [Authorize(Roles = "Admin")]

    public class filmController : Controller
    {
        private readonly moviesDbcontext db;
        [Obsolete]
        private IHostingEnvironment Hosting;

        [Obsolete]
        public filmController(moviesDbcontext context, IHostingEnvironment Hosting )
        {
            db = context;
            this.Hosting = Hosting;
        }
        // GET: filmController
        public ActionResult Index()
        {

            var moviesDbcontext = db.film.Include(f => f.categrayfilm);
            return View( moviesDbcontext.ToList());
        }

        // GET: filmController/Details/5
        public ActionResult Details(int id)
        {

            var films = db.film.Find(id);
            return View(films);
        }

        // GET: filmController/Create
        public ActionResult Create()
        {

            ViewData["catagresid"] = new SelectList(db.categrayfilm, "Id", "tittle");
            return View();
        }

        // POST: filmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(Film film)
        {
            string filename = string.Empty;

            if (film.File != null)
            {
                string uploads = Path.Combine(Hosting.WebRootPath, "upload");
                filename = film.File.FileName;
                string fullpath = Path.Combine(uploads, filename);
                film.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                film.image = filename;

            }
            db.Add(film);
            db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: filmController/Edit/5
        public ActionResult Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var film = db.film.Find(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["catagresid"] = new SelectList(db.categrayfilm, "Id", "tittle", film.categrayfilmid);
            return View(film);
        }

        // POST: filmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(int id, Film film)
        {

            string filename = string.Empty;
            string oldfilename = string.Empty;
            if (film.File != null)
            {
                string uploads = Path.Combine(Hosting.WebRootPath, "upload");

                filename = film.File.FileName;
                string fullPath = Path.Combine(uploads, filename);
                // delete file old
                oldfilename = film.File.FileName;
                string fileoldpath = Path.Combine(uploads, oldfilename);
                if (filename != fileoldpath)
                {

                    System.IO.File.Delete(fileoldpath);

                    film.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
            }

            film.image = filename;
            db.Update(film);

            db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: filmController/Delete/5
        public ActionResult Delete(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var films = db.film
                .Include(n => n.categrayfilm)
                .FirstOrDefault(m => m.Id == id);
            if (films == null)
            {
                return NotFound();
            }

            return View(films);
        }

        // POST: filmController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            var films = db.film.Find(id);
            db.film.Remove(films);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}
