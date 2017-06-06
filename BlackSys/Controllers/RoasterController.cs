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
    public class RoasterController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Roaster/
        public ActionResult Index()
        {
            return View(db.Roasters.ToList());
        }

        // GET: /Roaster/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roaster roaster = db.Roasters.Find(id);
            if (roaster == null)
            {
                return HttpNotFound();
            }
            return View(roaster);
        }

        // GET: /Roaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Roaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoasterId,BranchID,EmployeeID,ShiftCode,RoasterDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Roaster roaster)
        {
            if (ModelState.IsValid)
            {
                db.Roasters.Add(roaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roaster);
        }

        // GET: /Roaster/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roaster roaster = db.Roasters.Find(id);
            if (roaster == null)
            {
                return HttpNotFound();
            }
            return View(roaster);
        }

        // POST: /Roaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoasterId,BranchID,EmployeeID,ShiftCode,RoasterDate,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Roaster roaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roaster);
        }

        // GET: /Roaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roaster roaster = db.Roasters.Find(id);
            if (roaster == null)
            {
                return HttpNotFound();
            }
            return View(roaster);
        }

        // POST: /Roaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Roaster roaster = db.Roasters.Find(id);
            db.Roasters.Remove(roaster);
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
