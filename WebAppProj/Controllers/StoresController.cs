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
    public class StoresController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Stores
        [HttpGet]
        public ViewResult Index()
        {
            return View(db.Stores.ToList());
        }
        [HttpPost]
        public ViewResult Index(string StoreName, string StoreCity, string StoreState, Store s)
        {
            //the search is case sensetive
            var store = db.Stores.ToList().Where(a => a.StoreName.StartsWith(StoreName)
            && a.StoreCity.StartsWith(StoreCity) && a.StoreState.StartsWith(StoreState));
            return View(store);
        }

        // GET: Stores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StoreName,StoreAddress,StoreCity,StoreState")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StoreName,StoreAddress,StoreCity,StoreState")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
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
        public ActionResult GroupByCityQuery()
        {//Group by Query
            List<Store> stores = db.Stores.ToList();
            var result =
                from store in stores
                group store by store.StoreCity into newGroup
                select newGroup;
            return View("groupbycityresult", result.ToList());
        }

        public ActionResult GroupByStateQuery()
        {//Group by Query
            List<Store> stores = db.Stores.ToList();
            var result =
                 from store in stores
                 group store by store.StoreState into newGroup
                 select newGroup;
            return View("groupbystateresult", result.ToList());
        }
    }
}


