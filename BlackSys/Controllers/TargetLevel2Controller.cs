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
    public class TargetLevel2Controller : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /TargetLevel2/
        public ActionResult Index()
        {
            DateTime startTime = DateTime.Now;
            var endtime = db.TargetLevels2.AsEnumerable().Select(x => x.ExpireDate).FirstOrDefault();
            TimeSpan span = endtime.Subtract(startTime);
            var targetlist = (from tl2 in db.TargetLevels2.AsEnumerable()
                              join emp in db.Employees.AsEnumerable() on tl2.AssignTo equals emp.EmpID
                              join bnh in db.Branchs.AsEnumerable() on tl2.BranchId equals bnh.BranchId
                              select new TargetLevel2()
                              {
                                  BranchName = bnh.BranchName,
                                  TargetMonth = tl2.TargetDate.ToString("MMMM"),
                                  TargetAmount = tl2.TargetAmount,
                                  DaysRemain = span.Days.ToString(),
                                  SupervisorName = db.Employees.Where(x => x.EmpID == tl2.AssignBy).Select(x => x.EmpName).FirstOrDefault(),
                                  EmployeeName = db.Employees.Where(x => x.EmpID == tl2.AssignTo).Select(x => x.EmpName).FirstOrDefault()

                              }).ToList();



            return View(targetlist);
        }

        // GET: /TargetLevel2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetLevel2 targetLevel2 = db.TargetLevels2.Find(id);
            if (targetLevel2 == null)
            {
                return HttpNotFound();
            }
            return View(targetLevel2);
        }

        // GET: /TargetLevel2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TargetLevel2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TargetLevel2 targetLevel2, [Bind(Include = "ID,TargetDate,ExpireDate,,BranchId,TargetAmount,AchAmount,GapAmount,IsDone,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp,AssignTo")] TargetVsAch targetvsach)
        {
            if (ModelState.IsValid)
            {
                db.TargetLevels2.Add(targetLevel2);
        db.TargetVsAchs.Add(targetvsach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(targetLevel2);
        }

        // GET: /TargetLevel2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetLevel2 targetLevel2 = db.TargetLevels2.Find(id);
            if (targetLevel2 == null)
            {
                return HttpNotFound();
            }
            return View(targetLevel2);
        }

        // POST: /TargetLevel2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TargetLevel2 targetLevel2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetLevel2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(targetLevel2);
        }

        // GET: /TargetLevel2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetLevel2 targetLevel2 = db.TargetLevels2.Find(id);
            if (targetLevel2 == null)
            {
                return HttpNotFound();
            }
            return View(targetLevel2);
        }

        // POST: /TargetLevel2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetLevel2 targetLevel2 = db.TargetLevels2.Find(id);
            db.TargetLevels2.Remove(targetLevel2);
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
