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
    public class LeaveController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Leave/
        public ActionResult Index()
        {
            return View(db.Leaves.ToList());
        }

        // GET: /Leave/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // GET: /Leave/Create
        public ActionResult Create()
        {
            ViewBag.LeaveType = db.LeaveTypes.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View();
        }

        // POST: /Leave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LeaveId,BranchID,EmployeeID,LeaveType,TotalDays,FromDate,ToDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Leaves.Add(leave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeaveType = db.LeaveTypes.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(leave);
        }

        // GET: /Leave/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveType = db.LeaveTypes.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(leave);
        }

        // POST: /Leave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LeaveId,BranchID,EmployeeID,LeaveType,TotalDays,FromDate,ToDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeaveType = db.LeaveTypes.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(leave);
        }

        // GET: /Leave/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            
            return View(leave);
        }

        // POST: /Leave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leave leave = db.Leaves.Find(id);
            db.Leaves.Remove(leave);
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
