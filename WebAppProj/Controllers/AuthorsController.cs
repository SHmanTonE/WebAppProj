﻿using System;
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
    public class AuthorsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Authors
        [HttpGet]
        public ViewResult Index()
        {

            return View(db.Authors.ToList());
        }

        [HttpPost]
        public ViewResult Index(string AuthorName, string AuthorCity, string AuthorState, Author aut)
        {
            //the search is case sensetive
            var author = db.Authors.ToList().Where(a => (a.FirstName.StartsWith(AuthorName) || a.LastName.StartsWith(AuthorName))
            && a.AuthorCity.StartsWith(AuthorCity) && a.AuthorState.StartsWith(AuthorState));
            return View(author);
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstName,AuthorAddress,AuthorCity,AuthorState,Phone,Email")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,AuthorAddress,AuthorCity,AuthorState,Phone,Email")] Author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            db.Authors.Remove(author);
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

        public JsonResult GroupByName()
        {
            var list = from author in db.Authors
                       select new
                       {
                           name = author.FirstName
                       };

            var returnList = from a in list
                             group a by a.name into g
                             select new
                             {
                                 id = g.Key,
                                 sum = g.Count()
                             };

            return Json(returnList , JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult GroupByQuery()
        {//Group by Query
            List<Author> authors = db.Authors.ToList();
            var result =
                from author in authors
                group author by author.FirstName into newGroup
                select newGroup;
            return View("groupbyresult", result.ToList());
        }

        public ActionResult GroupByNameQuery()
        {//Group by Query
            List<Author> authors = db.Authors.ToList();
            var result =
                from author in authors
                group author by author.FirstName into newGroup
                select newGroup;
            return View("groupbynameresult", result.ToList());
        }

        public ActionResult GroupByCityQuery()
        {//Group by Query
            List<Author> authors = db.Authors.ToList();
            var result =
                from author in authors
                group author by author.AuthorCity into newGroup
                select newGroup;
            return View("groupbycityresult", result.ToList());
        }
    }
}
