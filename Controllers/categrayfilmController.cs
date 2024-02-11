using books.Models;
using books.Models.intervace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace books.Controllers
{
    [Authorize(Roles = "Admin")]
    public class categrayfilmController : Controller
    {
        private readonly moviesDbcontext db;
        [Obsolete]
        private IHostingEnvironment Hosting;
        private object inherate;

        [Obsolete]
        public categrayfilmController(moviesDbcontext context , IHostingEnvironment Hosting )
        {
            db = context;
            this.Hosting = Hosting;
        }
        // GET: categrayfilmController
        public ActionResult Index()
        {
            return View(db.categrayfilm.ToList());

        }

        // GET: categrayfilmController/Details/5
        public ActionResult Details(int id)
        {

            var categrayfilm = db.categrayfilm.Find(id);
            return View(categrayfilm);
        }

        // GET: categrayfilmController/Create
        public ActionResult Create()
        {

            ViewData["catagresid"] = new SelectList(db.categrayfilm, "id", "name");
            return View();
        }

        // POST: categrayfilmController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(Categrayfilm categrayfilm)
        {
            string filename = string.Empty;

            if (categrayfilm.File != null)
            {
                string uploads = Path.Combine(Hosting.WebRootPath, "upload");
                filename = categrayfilm.File.FileName;
                string fullpath = Path.Combine(uploads, filename);
                categrayfilm.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                categrayfilm.ImageUrl = filename;

            }
            db.Add(categrayfilm);
             db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            }

        
        // GET: categrayfilmController/Edit/5
        public ActionResult Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var categrayfilm = db.categrayfilm.Find(id);
            if (categrayfilm == null)
            {
                return NotFound();
            }
            ViewData["catagresid"] = new SelectList(db.categrayfilm, "id", "name", categrayfilm.Id);
            return View(categrayfilm);
        }

        // POST: categrayfilmController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Edit(Categrayfilm categrayfilm)
        {
            string filename = string.Empty;
            string oldfilename = string.Empty;
            if (categrayfilm.File != null)
            {
                string uploads = Path.Combine(Hosting.WebRootPath, "upload");

                filename = categrayfilm.File.FileName;
                string fullPath = Path.Combine(uploads, filename);
                // delete file old
                oldfilename = categrayfilm.File.FileName;
                string fileoldpath = Path.Combine(uploads, oldfilename);
                if (filename != fileoldpath)
                {

                    System.IO.File.Delete(fileoldpath);

                    categrayfilm.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }


            }

            categrayfilm.ImageUrl = filename;
            db.Update(categrayfilm);
            db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: categrayfilmController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news =  db.categrayfilm
                .Include(n => n.Film)
                .FirstOrDefault(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: categrayfilmController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {

            var categrayfilm = db.categrayfilm.Find(id);
            db.categrayfilm.Remove(categrayfilm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Raptors
        //public async Task<IActionResult> Index() // hidden to add search
        
    }
}
