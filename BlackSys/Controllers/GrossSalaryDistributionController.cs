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
    public class GrossSalaryDistributionController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /GrossSalaryDistribution/
        public ActionResult Index()
        {
            return View(db.GrossSalaryDistributions.ToList());
        }

        // GET: /GrossSalaryDistribution/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrossSalaryDistribution grosssalarydistribution = db.GrossSalaryDistributions.Find(id);
            if (grosssalarydistribution == null)
            {
                return HttpNotFound();
            }
            return View(grosssalarydistribution);
        }

        // GET: /GrossSalaryDistribution/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /GrossSalaryDistribution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,AllowanceId,PercentageOfGross,IsActive")] GrossSalaryDistribution grosssalarydistribution)
        {
            if (ModelState.IsValid)
            {
                db.GrossSalaryDistributions.Add(grosssalarydistribution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grosssalarydistribution);
        }

        // GET: /GrossSalaryDistribution/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrossSalaryDistribution grosssalarydistribution = db.GrossSalaryDistributions.Find(id);
            if (grosssalarydistribution == null)
            {
                return HttpNotFound();
            }
            return View(grosssalarydistribution);
        }

        // POST: /GrossSalaryDistribution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,AllowanceId,PercentageOfGross,IsActive")] GrossSalaryDistribution grosssalarydistribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grosssalarydistribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grosssalarydistribution);
        }

        // GET: /GrossSalaryDistribution/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrossSalaryDistribution grosssalarydistribution = db.GrossSalaryDistributions.Find(id);
            if (grosssalarydistribution == null)
            {
                return HttpNotFound();
            }
            return View(grosssalarydistribution);
        }

        // POST: /GrossSalaryDistribution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrossSalaryDistribution grosssalarydistribution = db.GrossSalaryDistributions.Find(id);
            db.GrossSalaryDistributions.Remove(grosssalarydistribution);
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
