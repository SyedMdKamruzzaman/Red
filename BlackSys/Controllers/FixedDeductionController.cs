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
    public class FixedDeductionController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /FixedDeduction/
        public ActionResult Index()
        {
            var fixedDeduction = (from fxdDeduct in db.FixedDeductions.AsEnumerable()
                                  join deduction in db.Deductions.AsEnumerable() on fxdDeduct.DeductionId equals deduction.Id
                                  select new FixedDeduction
                                  {
                                      Id = fxdDeduct.Id,
                                      EmployeeId = fxdDeduct.EmployeeId,
                                      DeductionName = deduction.DeductionName,
                                      DeductionAmount = fxdDeduct.DeductionAmount,
                                      ActiveStatus = fxdDeduct.IsActive == true ? "Active" : "Not Active"
                                  }).ToList();

            return View(fixedDeduction);
        }

        // GET: /FixedDeduction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeduction fixeddeduction = db.FixedDeductions.Find(id);
            if (fixeddeduction == null)
            {
                return HttpNotFound();
            }
            return View(fixeddeduction);
        }

        // GET: /FixedDeduction/Create
        public ActionResult Create()
        {
            ViewBag.DeductionName = db.Deductions.Where(a => a.IsManageBySystem != true && a.DeductionTypeId == 1).ToList();
            return View();
        }

        // POST: /FixedDeduction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FixedDeduction fixeddeduction)
        {
            if (ModelState.IsValid)
            {
                db.FixedDeductions.Add(fixeddeduction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fixeddeduction);
        }

        // GET: /FixedDeduction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeduction fixeddeduction = db.FixedDeductions.Find(id);
            if (fixeddeduction == null)
            {
                return HttpNotFound();
            }
            return View(fixeddeduction);
        }

        // POST: /FixedDeduction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EmployeeID,DeductID,DeductAmount,IsActive,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] FixedDeduction fixeddeduction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixeddeduction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fixeddeduction);
        }

        // GET: /FixedDeduction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedDeduction fixeddeduction = db.FixedDeductions.Find(id);
            if (fixeddeduction == null)
            {
                return HttpNotFound();
            }
            return View(fixeddeduction);
        }

        // POST: /FixedDeduction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixedDeduction fixeddeduction = db.FixedDeductions.Find(id);
            db.FixedDeductions.Remove(fixeddeduction);
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
