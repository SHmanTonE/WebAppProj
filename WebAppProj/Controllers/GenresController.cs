using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MasterProj.Models;
using MasterProj.Models.DAL;

namespace WebAppProj.Controllers
{
    public class GenresController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Generes.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Generes.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GenreType")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Generes.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Generes.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GenreType")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Generes.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Generes.Find(id);
            db.Generes.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult TitlesPerGenreGraph()
        {

            List<Title> titles = db.Titles.ToList();
            Dictionary<int, int> counter = new Dictionary<int, int>();
            foreach (var genre in db.Generes)
            {
                counter.Add(genre.ID, 0);
            }

            foreach (var title in titles)
            {
                counter[title.GenreID] += 1;
            }
            var items = from pair in counter
                        orderby pair.Value descending
                        select pair;

            List<int> resVal = new List<int>();
            List<String> resName = new List<string>();
            List<KeyValuePair<string, int>> res = new List<KeyValuePair<string, int>>();
            foreach (var pair in items)
            {
                res.Add(new KeyValuePair<string, int>(db.Generes.Find(pair.Key).GenreType.ToString(), pair.Value));
                // resVal.Add(pair.Value*10);
                //  resName.Add(db.Paints.Find(pair.Key).PaintName.ToString());
            }

            return View(res);
        }
    }
}
