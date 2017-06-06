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
    public class AllowancesController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: Allowances
        public ActionResult Index()
        {
            var allowanceList = (from allowance in db.Allowances.AsEnumerable()
                                 join allowCategory in db.AllowanceCategories.AsEnumerable() on allowance.AllowanceCategoryId equals allowCategory.Id
                                 join allowType in db.AllowanceTypes.AsEnumerable() on allowance.AllowanceTypeId equals allowType.Id
                                 select new Allowance
                                 {
                                     AllowanceName = allowance.AllowanceName,
                                     AllowanceCategory = allowCategory.Name,
                                     AllowanceType = allowType.Type,
                                     PartofGrossSalary = allowance.IsPartOfGrossSalary == true ? "Yes" : "No",
                                     ManagebySystem = allowance.IsManageBySystem == true ? "Yes" : "No"

                                 }).ToList();

            return View(allowanceList);
        }

        // GET: Allowances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allowance allowance = db.Allowances.Find(id);
            if (allowance == null)
            {
                return HttpNotFound();
            }
            return View(allowance);
        }

        // GET: Allowances/Create
        public ActionResult Create()
        {
            ViewBag.AllowanceCategory = db.AllowanceCategories.ToList();
            ViewBag.AllowanceType = db.AllowanceTypes.ToList();
            return View();
        }

        // POST: Allowances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Allowance allowance)
        {
            if (ModelState.IsValid)
            {
                db.Allowances.Add(allowance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allowance);
        }

        // GET: Allowances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allowance allowance = db.Allowances.Find(id);
            if (allowance == null)
            {
                return HttpNotFound();
            }
            return View(allowance);
        }

        // POST: Allowances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AllowanceID,AllowanceName,AllowanceCategoryId,AllowanceType,IsManageBySystem,IsPartOfGrossSalary,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Allowance allowance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allowance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allowance);
        }

        // GET: Allowances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Allowance allowance = db.Allowances.Find(id);
            if (allowance == null)
            {
                return HttpNotFound();
            }
            return View(allowance);
        }

        // POST: Allowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Allowance allowance = db.Allowances.Find(id);
            db.Allowances.Remove(allowance);
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
