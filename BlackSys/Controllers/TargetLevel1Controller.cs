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
    public class TargetLevel1Controller : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /TargetLevel1/
        public ActionResult Index()
        {
            DateTime startTime = DateTime.Now;
            var endtime = db.TargetLevels1.AsEnumerable().Select(x => x.ExpireDate).FirstOrDefault();
            TimeSpan span = endtime.Subtract(startTime);
            var targetlist = (from tl1 in db.TargetLevels1.AsEnumerable() join emp in db.Employees.AsEnumerable()
                              on tl1.AssignTo equals emp.EmpID
                              join bnh in db.Branchs.AsEnumerable() on tl1.BranchId equals bnh.BranchId
                              select new TargetLevel1()
                              {
                                  BranchName = bnh.BranchName,
                                  TargetMonth = tl1.TargetDate.ToString("MMMM"),
                                  TargetAmount = tl1.TargetAmount,                                  
                                  DaysRemain = span.Days.ToString(),
                                  SupervisorName= db.Employees.Where(x=>x.EmpID==tl1.AssignBy).Select(x=>x.EmpName).FirstOrDefault(),
                                  EmployeeName = db.Employees.Where(x => x.EmpID == tl1.AssignTo).Select(x => x.EmpName).FirstOrDefault()

                              }).ToList();



            return View(targetlist);
        }

        // GET: /TargetLevel1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetLevel1 targetLevel1 = db.TargetLevels1.Find(id);
            if (targetLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(targetLevel1);
        }

        // GET: /TargetLevel1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TargetLevel1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TargetLevel1Id,BranchId,TargetDate,ExpireDate,TargetAmount,NumberOfDays,AssignBy,AssignTo,IsActive,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] TargetLevel1 targetLevel1)
        {
            if (ModelState.IsValid)
            {
                db.TargetLevels1.Add(targetLevel1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(targetLevel1);
        }

        // GET: /TargetLevel1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetLevel1 targetLevel1 = db.TargetLevels1.Find(id);
            if (targetLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(targetLevel1);
        }

        // POST: /TargetLevel1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TargetLevel1Id,BranchId,TargetDate,ExpireDate,TargetAmount,NumberOfDays,AssignBy,AssignTo,IsActive,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] TargetLevel1 targetLevel1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetLevel1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(targetLevel1);
        }

        // GET: /TargetLevel1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetLevel1 targetLevel1 = db.TargetLevels1.Find(id);
            if (targetLevel1 == null)
            {
                return HttpNotFound();
            }
            return View(targetLevel1);
        }

        // POST: /TargetLevel1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetLevel1 targetLevel1 = db.TargetLevels1.Find(id);
            db.TargetLevels1.Remove(targetLevel1);
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
