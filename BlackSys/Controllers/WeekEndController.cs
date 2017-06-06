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
    public class WeekEndController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /WeekEnd/
        public ActionResult Index()
        {
            return View(db.WeekEnds.ToList());
        }

        // GET: /WeekEnd/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeekEnd weekend = db.WeekEnds.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            return View(weekend);
        }

        // GET: /WeekEnd/Create
        public ActionResult Create()
        {
            ViewBag.Branch = db.Branchs.ToList();
            return View();
        }

        // POST: /WeekEnd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="WeekEndId,BranchID,WeekDay,EffectiveDate,ExpireDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] WeekEnd weekend)
        {
            if (ModelState.IsValid)
            {
                db.WeekEnds.Add(weekend);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branch = db.Branchs.ToList();
            return View(weekend);
        }

        // GET: /WeekEnd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeekEnd weekend = db.WeekEnds.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            ViewBag.Branch = db.Branchs.ToList();
            return View(weekend);
        }

        // POST: /WeekEnd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="WeekEndId,BranchID,WeekDay,EffectiveDate,ExpireDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] WeekEnd weekend)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weekend).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Branch = db.Branchs.ToList();
            return View(weekend);
        }

        // GET: /WeekEnd/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeekEnd weekend = db.WeekEnds.Find(id);
            if (weekend == null)
            {
                return HttpNotFound();
            }
            return View(weekend);
        }

        // POST: /WeekEnd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeekEnd weekend = db.WeekEnds.Find(id);
            db.WeekEnds.Remove(weekend);
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
