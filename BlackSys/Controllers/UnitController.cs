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
    public class UnitController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Unit/
        public ActionResult Index()
        {
            return View(db.UnitModels.ToList());
        }

        // GET: /Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitModel unitmodel = db.UnitModels.Find(id);
            if (unitmodel == null)
            {
                return HttpNotFound();
            }
            return View(unitmodel);
        }

        // GET: /Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] UnitModel unitmodel)
        {
            if (ModelState.IsValid)
            {
                db.UnitModels.Add(unitmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unitmodel);
        }

        // GET: /Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitModel unitmodel = db.UnitModels.Find(id);
            if (unitmodel == null)
            {
                return HttpNotFound();
            }
            return View(unitmodel);
        }

        // POST: /Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] UnitModel unitmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitmodel);
        }

        // GET: /Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitModel unitmodel = db.UnitModels.Find(id);
            if (unitmodel == null)
            {
                return HttpNotFound();
            }
            return View(unitmodel);
        }

        // POST: /Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitModel unitmodel = db.UnitModels.Find(id);
            db.UnitModels.Remove(unitmodel);
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
