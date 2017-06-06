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
    public class FixedAllowanceController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /FixedAllowance/
        public ActionResult Index()
        {
            var fixedAllowance = (from fxdAllow in db.FixedAllowances.AsEnumerable()
                                  join allowance in db.Allowances.AsEnumerable() on fxdAllow.AllowanceId equals allowance.Id
                                  select new FixedAllowance
                                  {
                                      Id = fxdAllow.Id,
                                      EmployeeId = fxdAllow.EmployeeId,
                                      AllowanceName = allowance.AllowanceName,
                                      AllowanceAmount = fxdAllow.AllowanceAmount,
                                      EmployeeBankAccount = fxdAllow.EmployeeBankAccount,
                                      SalaryBankAccount = fxdAllow.SalaryBankAccount,
                                      ActiveStatus =fxdAllow.IsActive==true?"Active":"Not Active"
                                  }).ToList();

            return View(fixedAllowance);
        }

        // GET: /FixedAllowance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedAllowance fixedallowance = db.FixedAllowances.Find(id);
            if (fixedallowance == null)
            {
                return HttpNotFound();
            }
            return View(fixedallowance);
        }

        // GET: /FixedAllowance/Create
        public ActionResult Create()
        {
            ViewBag.AllowanceName = db.Allowances.Where(a => a.IsManageBySystem != true && a.AllowanceTypeId==1).ToList();
            return View();
        }

        // POST: /FixedAllowance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FixedAllowance fixedallowance)
        {
            if (ModelState.IsValid)
            {
                db.FixedAllowances.Add(fixedallowance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fixedallowance);
        }

        // GET: /FixedAllowance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedAllowance fixedallowance = db.FixedAllowances.Find(id);
            if (fixedallowance == null)
            {
                return HttpNotFound();
            }
            return View(fixedallowance);
        }

        // POST: /FixedAllowance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FixedAllowance fixedallowance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixedallowance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fixedallowance);
        }

        // GET: /FixedAllowance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedAllowance fixedallowance = db.FixedAllowances.Find(id);
            if (fixedallowance == null)
            {
                return HttpNotFound();
            }
            return View(fixedallowance);
        }

        // POST: /FixedAllowance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixedAllowance fixedallowance = db.FixedAllowances.Find(id);
            db.FixedAllowances.Remove(fixedallowance);
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
