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
    public class VariableAllowanceController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /VariableAllowance/
        public ActionResult Index()
        {
            var variableAllowance = (from varAllow in db.VariableAllowances.AsEnumerable()
                                     join allowance in db.Allowances.AsEnumerable() on varAllow.AllowanceId equals allowance.Id
                                  select new VariableAllowance
                                  {
                                      Id = varAllow.Id,
                                      EmployeeId = varAllow.EmployeeId,
                                      AllowanceName = allowance.AllowanceName,
                                      AllowanceAmount = varAllow.AllowanceAmount,
                                      EmployeeBankAccount = varAllow.EmployeeBankAccount,
                                      SalaryBankAccount = varAllow.SalaryBankAccount,
                                      ActiveStatus = varAllow.IsActive == true ? "Active" : "Not Active"
                                  }).ToList();

            return View(variableAllowance);
        }

        // GET: /VariableAllowance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableAllowance variableallowance = db.VariableAllowances.Find(id);
            if (variableallowance == null)
            {
                return HttpNotFound();
            }
            return View(variableallowance);
        }

        // GET: /VariableAllowance/Create
        public ActionResult Create()
        {
            ViewBag.AllowanceName = db.Allowances.Where(a => a.IsManageBySystem != true && a.AllowanceTypeId == 2).ToList();
            return View();
        }

        // POST: /VariableAllowance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VariableAllowance variableallowance)
        {
            if (ModelState.IsValid)
            {
                db.VariableAllowances.Add(variableallowance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(variableallowance);
        }

        // GET: /VariableAllowance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableAllowance variableallowance = db.VariableAllowances.Find(id);
            if (variableallowance == null)
            {
                return HttpNotFound();
            }
            return View(variableallowance);
        }

        // POST: /VariableAllowance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EmployeeID,AllownceID,AllowanceAmount,EmployeeBankAccount,SalaryBankAccount,IsActive,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] VariableAllowance variableallowance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(variableallowance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(variableallowance);
        }

        // GET: /VariableAllowance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VariableAllowance variableallowance = db.VariableAllowances.Find(id);
            if (variableallowance == null)
            {
                return HttpNotFound();
            }
            return View(variableallowance);
        }

        // POST: /VariableAllowance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VariableAllowance variableallowance = db.VariableAllowances.Find(id);
            db.VariableAllowances.Remove(variableallowance);
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
