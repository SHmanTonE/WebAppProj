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
    public class TitlesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Titles
        [HttpGet]
        public ViewResult Index()
        {
            var titles = db.Titles.Include(t => t.Author).Include(t => t.Genre);
            return View(titles.ToList());
        }
        [HttpPost]
        public ViewResult Index(string TitleName, string Genre, string Author)
        {
            //the search is case sensetive
            var title = db.Titles.Include(t => t.Author).Include(t => t.Genre).ToList().Where(t => t.TitleName.StartsWith(TitleName)
            && t.Genre.GenreType.StartsWith(Genre) &&( t.Author.FirstName.StartsWith(Author)|| t.Author.LastName.StartsWith(Author))); //&& a.Price.Equals(Price));
            return View(title);
        }
        // GET: Titles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = db.Titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName");
            ViewBag.GenreID = new SelectList(db.Generes, "ID", "GenreType");
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TitleName,Price,PublishedDate,AuthorID,GenreID")] Title title)
        {
            if (ModelState.IsValid)
            {
                db.Titles.Add(title);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", title.AuthorID);
            ViewBag.GenreID = new SelectList(db.Generes, "ID", "GenreType", title.GenreID);
            return View(title);
        }


        // GET: Titles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = db.Titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", title.AuthorID);
            ViewBag.GenreID = new SelectList(db.Generes, "ID", "GenreType", title.GenreID);
            return View(title);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TitleName,Price,PublishedDate,AuthorID,GenreID")] Title title)
        {
            if (ModelState.IsValid)
            {
                db.Entry(title).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "ID", "FullName", title.AuthorID);
            ViewBag.GenreID = new SelectList(db.Generes, "ID", "GenreType", title.GenreID);
            return View(title);
        }

        // GET: Titles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = db.Titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Title title = db.Titles.Find(id);
            db.Titles.Remove(title);
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

        public ActionResult RecomandadTitles()
        {//Recommand Algo
            List<Sale> sales = db.Sales.ToList();
            Dictionary<int, int> counter = new Dictionary<int, int>();
            foreach (var sale in db.Sales)
            {
                counter.Add(sale.ID, 0);
            }

            foreach (var sale in sales)
            {
                counter[sale.TitleID] += 1;
            }
            var items = from pair in counter
                        orderby pair.Value descending
                        select pair;
            List<Title> result = new List<Title>();

            foreach (KeyValuePair<int, int> pair in items)
            {
                result.Add(db.Titles.Find(pair.Key));
            }

            return View("RecomandadTitles",result.GetRange(0, 3));
        }


        public ActionResult HowManyTitlesWasOrderedGraph()
        {

            List<Sale> sales = db.Sales.ToList();
            Dictionary<int, int> counter = new Dictionary<int, int>();
            foreach (var title in db.Titles)
            {
                counter.Add(title.ID, 0);
            }

            foreach (var sale in sales)
            {
               counter[sale.TitleID] += 1;
            }
            var items = from pair in counter
                        orderby pair.Value descending
                        select pair;

            List<int> resVal = new List<int>();
            List<String> resName = new List<string>();
            List<KeyValuePair<string, int>> res = new List<KeyValuePair<string, int>>();
            foreach (var pair in items)
            {
                res.Add(new KeyValuePair<string, int>(db.Titles.Find(pair.Key).TitleName.ToString(), pair.Value));
                // resVal.Add(pair.Value*10);
                //  resName.Add(db.Paints.Find(pair.Key).PaintName.ToString());
            }

            return View(res);
        }

        public ActionResult PieChart()
        {
            return View();
        }
        public JsonResult MakeJsonResult()
        {
            int globalCounter = 0;
            List<Sale> sales = db.Sales.ToList();
            Dictionary<int, int> counter = new Dictionary<int, int>();
            foreach (var title in db.Titles)
            {
                counter.Add(title.ID, 0);
            }

            foreach (var sale in sales)
            {
                counter[sale.TitleID] += 1;
                globalCounter++;
                
            }

            var items = from pair in counter
                        select new
                        {
                            label = pair.Key,
                            count = pair.Value
                        };

            return Json(items, JsonRequestBehavior.AllowGet);
        }




    }
}
