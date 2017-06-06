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
    public class HappyHourModelController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /HappyHourModel/
        public ActionResult Index()
        {
            return View(db.HappyHourModels.ToList());
        }

        // GET: /HappyHourModel/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HappyHourModel happyHourModel = db.HappyHourModels.Find(id);
            if (happyHourModel == null)
            {
                return HttpNotFound();
            }
            return View(happyHourModel);
        }

        // GET: /HappyHourModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /HappyHourModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StartTime,EndTime,IsActive")] HappyHourModel happyHourModel)
        {
            if (ModelState.IsValid)
            {
                db.HappyHourModels.Add(happyHourModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(happyHourModel);
        }

        // GET: /HappyHourModel/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HappyHourModel happyHourModel = db.HappyHourModels.Find(id);
            if (happyHourModel == null)
            {
                return HttpNotFound();
            }
            return View(happyHourModel);
        }

        // POST: /HappyHourModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StartTime,EndTime,IsActive")] HappyHourModel happyHourModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(happyHourModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(happyHourModel);
        }

        // GET: /HappyHourModel/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HappyHourModel happyHourModel = db.HappyHourModels.Find(id);
            if (happyHourModel == null)
            {
                return HttpNotFound();
            }
            return View(happyHourModel);
        }

        // POST: /HappyHourModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HappyHourModel happyHourModel = db.HappyHourModels.Find(id);
            db.HappyHourModels.Remove(happyHourModel);
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
