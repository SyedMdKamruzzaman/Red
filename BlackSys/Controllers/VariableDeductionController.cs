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
    public class VariableDeductionController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /VariableDeduction/
        public ActionResult Index()
        {
            var fixedDeduction = (from fxdDeduct in db.FixedDeductions.AsEnumerable()
                                  join deduction in db.Deductions.AsEnumerable() on fxdDeduct.DeductionId equals deduction.Id
                                  select new VariableDeduction
                                  {
                                      Id = fxdDeduct.Id,
                                      EmployeeId = fxdDeduct.EmployeeId,
                                      DeductionName = deduction.DeductionName,
                                      DeductionAmount = fxdDeduct.DeductionAmount,
                                      ActiveStatus = fxdDeduct.IsActive == true ? "Active" : "Not Active"
                                  }).ToList();

            return View(fixedDeduction);
        }

        // GET: /VariableDeduction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableDeduction variablededuction = db.VariableDeductions.Find(id);
            if (variablededuction == null)
            {
                return HttpNotFound();
            }
            return View(variablededuction);
        }

        // GET: /VariableDeduction/Create
        public ActionResult Create()
        {
            ViewBag.DeductionName = db.Deductions.Where(a => a.IsManageBySystem != true && a.DeductionTypeId == 2).ToList();
            return View();
        }

        // POST: /VariableDeduction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VariableDeduction variablededuction)
        {
            if (ModelState.IsValid)
            {
                db.VariableDeductions.Add(variablededuction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(variablededuction);
        }

        // GET: /VariableDeduction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableDeduction variablededuction = db.VariableDeductions.Find(id);
            if (variablededuction == null)
            {
                return HttpNotFound();
            }
            return View(variablededuction);
        }

        // POST: /VariableDeduction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EmployeeID,DeductID,DeductAmount,IsActive,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] VariableDeduction variablededuction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variablededuction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(variablededuction);
        }

        // GET: /VariableDeduction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableDeduction variablededuction = db.VariableDeductions.Find(id);
            if (variablededuction == null)
            {
                return HttpNotFound();
            }
            return View(variablededuction);
        }

        // POST: /VariableDeduction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VariableDeduction variablededuction = db.VariableDeductions.Find(id);
            db.VariableDeductions.Remove(variablededuction);
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
