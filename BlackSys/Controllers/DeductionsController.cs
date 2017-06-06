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
    public class DeductionsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: Deductions
        public ActionResult Index()
        {
            var deductionList = (from deduction in db.Deductions.AsEnumerable()
                                 join allowCategory in db.AllowanceCategories.AsEnumerable() on deduction.DeductionCategoryId equals allowCategory.Id
                                 join allowType in db.AllowanceTypes.AsEnumerable() on deduction.DeductionTypeId equals allowType.Id
                                 select new Deduction
                                 {
                                     DeductionName = deduction.DeductionName,
                                     DeductionCategory = allowCategory.Name,
                                     DeductionType = allowType.Type,
                                     PartofGrossSalary = deduction.IsPartOfGrossSalary == true ? "Yes" : "No",
                                     ManagebySystem = deduction.IsManageBySystem == true ? "Yes" : "No"

                                 }).ToList();

            return View(deductionList);
        }

        // GET: Deductions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deduction deduction = db.Deductions.Find(id);
            if (deduction == null)
            {
                return HttpNotFound();
            }
            return View(deduction);
        }

        // GET: Deductions/Create
        public ActionResult Create()
        {
            ViewBag.AllowanceCategory = db.AllowanceCategories.ToList();
            ViewBag.AllowanceType = db.AllowanceTypes.ToList();
            return View();
        }

        // POST: Deductions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deduction deduction)
        {
            if (ModelState.IsValid)
            {
                db.Deductions.Add(deduction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deduction);
        }

        // GET: Deductions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deduction deduction = db.Deductions.Find(id);
            if (deduction == null)
            {
                return HttpNotFound();
            }
            return View(deduction);
        }

        // POST: Deductions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DeductionID,DeductionName,DeductionCategoryId,DeductionType,IsManageBySystem,IsPartOfGrossSalary,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Deduction deduction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deduction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deduction);
        }

        // GET: Deductions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deduction deduction = db.Deductions.Find(id);
            if (deduction == null)
            {
                return HttpNotFound();
            }
            return View(deduction);
        }

        // POST: Deductions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deduction deduction = db.Deductions.Find(id);
            db.Deductions.Remove(deduction);
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
