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
    public class MemberShipCardController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /MemberShipCard/
        public ActionResult Index()
        {
            return View(db.MemberInfoes.ToList());
        }

        // GET: /MemberShipCard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipInfoes MembershipInfoesmodel = db.MemberInfoes.Find(id);
            if (MembershipInfoesmodel == null)
            {
                return HttpNotFound();
            }
            return View(MembershipInfoesmodel);
        }

        // GET: /MemberShipCard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MemberShipCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MembershipInfoes MembershipInfoesmodel)
        {
            if (ModelState.IsValid)
            {
                db.MemberInfoes.Add(MembershipInfoesmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(MembershipInfoesmodel);
        }

        // GET: /MemberShipCard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipInfoes MembershipInfoesmodel = db.MemberInfoes.Find(id);
            if (MembershipInfoesmodel == null)
            {
                return HttpNotFound();
            }
            return View(MembershipInfoesmodel);

        }

        // POST: /MemberShipCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MembershipInfoes MembershipInfoesmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(MembershipInfoesmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(MembershipInfoesmodel);
        }

        // GET: /MemberShipCard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipInfoes MembershipInfoesmodel = db.MemberInfoes.Find(id);
            if (MembershipInfoesmodel == null)
            {
                return HttpNotFound();
            }
            return View(MembershipInfoesmodel);
        }

        // POST: /MemberShipCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipInfoes MembershipInfoesmodel = db.MemberInfoes.Find(id);
            db.MemberInfoes.Remove(MembershipInfoesmodel);
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
