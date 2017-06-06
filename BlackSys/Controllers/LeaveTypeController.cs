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
    public class LeaveTypeController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /LeaveType/
        public ActionResult Index()
        {
            return View(db.LeaveTypes.ToList());
        }

        // GET: /LeaveType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leavetype = db.LeaveTypes.Find(id);
            if (leavetype == null)
            {
                return HttpNotFound();
            }
            return View(leavetype);
        }

        // GET: /LeaveType/Create
        public ActionResult Create()
        {
            ViewBag.Branch = db.Branchs.ToList();
            return View();
        }

        // POST: /LeaveType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveType leavetype)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Branch = db.Branchs.ToList();
                db.LeaveTypes.Add(leavetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leavetype);
        }

        // GET: /LeaveType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leavetype = db.LeaveTypes.Find(id);
            if (leavetype == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branch = db.Branchs.ToList();
            return View(leavetype);
        }

        // POST: /LeaveType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveType leavetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leavetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branch = db.Branchs.ToList();
            return View(leavetype);
        }

        // GET: /LeaveType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveType leavetype = db.LeaveTypes.Find(id);
            if (leavetype == null)
            {
                return HttpNotFound();
            }
            return View(leavetype);
        }

        // POST: /LeaveType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveType leavetype = db.LeaveTypes.Find(id);
            db.LeaveTypes.Remove(leavetype);
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
