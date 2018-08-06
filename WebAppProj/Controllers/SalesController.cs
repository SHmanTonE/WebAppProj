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
    public class SalesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Sales
        [HttpGet]
        public ViewResult Index()
        {
            var sales = db.Sales.Include(s => s.Store).Include(s => s.Title);
            return View(sales.ToList());
        }
        [HttpPost]
        public ViewResult Index(int? Quantity, DateTime? SaleDate, decimal? TotalSalePrice)

        // public ViewResult Index(int? Quantity, DateTime? SaleDate, decimal? TotalSalePrice)
        {
            object sales;
            if(Quantity==null&&SaleDate==null&&TotalSalePrice==null)
            {
                sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList();
                return View(sales);
            }
            else if (Quantity == null && SaleDate == null && TotalSalePrice != null)
            {
                sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.TotalSalePrice.Equals(TotalSalePrice));
                return View(sales);
            }
            else if (Quantity == null && SaleDate != null && TotalSalePrice == null)
            {
                sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.SaleDate.Equals(SaleDate));
                return View(sales);
            }
            else if (Quantity == null && SaleDate != null && TotalSalePrice != null)
            {
                sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.TotalSalePrice.Equals(TotalSalePrice)
                && s.SaleDate.Equals(SaleDate));
                return View(sales);
            }
            else if (Quantity != null && SaleDate == null && TotalSalePrice == null)
            {
                sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.Quantity.Equals(Quantity));
                return View(sales);
            }
           else if (Quantity != null && SaleDate != null && TotalSalePrice == null)
            {
                sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.Quantity.Equals(Quantity)
                && s.SaleDate.Equals(SaleDate));
                return View(sales);
            }
         else  if (Quantity != null && SaleDate == null && TotalSalePrice != null)
            {
              sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.Quantity.Equals(Quantity)
              && s.TotalSalePrice.Equals(TotalSalePrice));
                return View(sales);
            }
            //(Quantity != null && SaleDate != null && TotalSalePrice != null)

            sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.Quantity.Equals(Quantity)
          && s.TotalSalePrice.Equals(TotalSalePrice)&&s.SaleDate.Equals(SaleDate));


            //the search is case sensetive
            // var sales = db.Sales.ToList().Where(s => (s.Quantity.Equals(Quantity) && s.Equals(SaleDate) && s.TotalSalePrice.Equals(TotalSalePrice)));

            /* if (SaleDate == null)
             {
                 sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.Quantity.Equals(Quantity));
             }
             else
             {
                 sales = db.Sales.Include(s => s.Store).Include(s => s.Title).ToList().Where(s => s.Quantity.Equals(Quantity) && s.SaleDate.Equals(SaleDate));
             }
             */

            return View(sales);
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "StoreName");
            ViewBag.TitleID = new SelectList(db.Titles, "ID", "TitleName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Quantity,SaleDate,TitleID,StoreID")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreID = new SelectList(db.Stores, "ID", "StoreName", sale.StoreID);
            ViewBag.TitleID = new SelectList(db.Titles, "ID", "TitleName", sale.TitleID);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "StoreName", sale.StoreID);
            ViewBag.TitleID = new SelectList(db.Titles, "ID", "TitleName", sale.TitleID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Quantity,SaleDate,TitleID,StoreID")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "StoreName", sale.StoreID);
            ViewBag.TitleID = new SelectList(db.Titles, "ID", "TitleName", sale.TitleID);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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
    }
}
