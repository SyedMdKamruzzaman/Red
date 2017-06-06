using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;

namespace BlackSys.Controllers
{
    public class AllowanceCategoryController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /AllowanceCategory/
        public ActionResult Index()
        {
            return View(db.AllowanceCategories.ToList());
        }

        // GET: /AllowanceCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceCategory allowancecategory = db.AllowanceCategories.Find(id);
            if (allowancecategory == null)
            {
                return HttpNotFound();
            }
            return View(allowancecategory);
        }

        // GET: /AllowanceCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AllowanceCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] AllowanceCategory allowancecategory)
        {
            if (ModelState.IsValid)
            {
                db.AllowanceCategories.Add(allowancecategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allowancecategory);
        }

        // GET: /AllowanceCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceCategory allowancecategory = db.AllowanceCategories.Find(id);
            if (allowancecategory == null)
            {
                return HttpNotFound();
            }
            return View(allowancecategory);
        }

        // POST: /AllowanceCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] AllowanceCategory allowancecategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allowancecategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allowancecategory);
        }

        // GET: /AllowanceCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceCategory allowancecategory = db.AllowanceCategories.Find(id);
            if (allowancecategory == null)
            {
                return HttpNotFound();
            }
            return View(allowancecategory);
        }

        // POST: /AllowanceCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllowanceCategory allowancecategory = db.AllowanceCategories.Find(id);
            db.AllowanceCategories.Remove(allowancecategory);
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
