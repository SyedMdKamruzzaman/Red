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
    public class AssignShiftController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /AssignShift/
        public ActionResult Index()
        {
            return View(db.AssignShifts.ToList());
        }

        // GET: /AssignShift/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignShift assignshift = db.AssignShifts.Find(id);
            if (assignshift == null)
            {
                return HttpNotFound();
            }
            return View(assignshift);
        }

        // GET: /AssignShift/Create
        public ActionResult Create()
        {
            ViewBag.ShiftCode = db.Shifts.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View();
        }

        // POST: /AssignShift/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AssignShiftId,BranchID,EmployeeID,ShiftCode,EffectiveDate,ExpireDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] AssignShift assignshift)
        {
            if (ModelState.IsValid)
            {
                db.AssignShifts.Add(assignshift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShiftCode = db.Shifts.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(assignshift);
        }

        // GET: /AssignShift/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignShift assignshift = db.AssignShifts.Find(id);
            if (assignshift == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShiftCode = db.Shifts.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(assignshift);
        }

        // POST: /AssignShift/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AssignShiftId,BranchID,EmployeeID,ShiftCode,EffectiveDate,ExpireDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] AssignShift assignshift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignshift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShiftCode = db.Shifts.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(assignshift);
        }

        // GET: /AssignShift/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignShift assignshift = db.AssignShifts.Find(id);
            if (assignshift == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShiftCode = db.Shifts.ToList();
            ViewBag.Employee = db.Employees.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View(assignshift);
        }

        // POST: /AssignShift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignShift assignshift = db.AssignShifts.Find(id);
            db.AssignShifts.Remove(assignshift);
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
