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
    public class AllowanceTypeController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /AllowanceType/
        public ActionResult Index()
        {
            return View(db.AllowanceTypes.ToList());
        }

        // GET: /AllowanceType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceType allowancetype = db.AllowanceTypes.Find(id);
            if (allowancetype == null)
            {
                return HttpNotFound();
            }
            return View(allowancetype);
        }

        // GET: /AllowanceType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AllowanceType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Type")] AllowanceType allowancetype)
        {
            if (ModelState.IsValid)
            {
                db.AllowanceTypes.Add(allowancetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allowancetype);
        }

        // GET: /AllowanceType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceType allowancetype = db.AllowanceTypes.Find(id);
            if (allowancetype == null)
            {
                return HttpNotFound();
            }
            return View(allowancetype);
        }

        // POST: /AllowanceType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Type")] AllowanceType allowancetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allowancetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allowancetype);
        }

        // GET: /AllowanceType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllowanceType allowancetype = db.AllowanceTypes.Find(id);
            if (allowancetype == null)
            {
                return HttpNotFound();
            }
            return View(allowancetype);
        }

        // POST: /AllowanceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllowanceType allowancetype = db.AllowanceTypes.Find(id);
            db.AllowanceTypes.Remove(allowancetype);
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
