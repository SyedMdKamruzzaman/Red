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
    public class SpecialDiscountsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /SpecialDiscounts/
        public ActionResult Index()
        {
            return View(db.SpecialDiscounts.ToList());
        }

        // GET: /SpecialDiscounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialDiscount specialDiscount = db.SpecialDiscounts.Find(id);
            if (specialDiscount == null)
            {
                return HttpNotFound();
            }
            return View(specialDiscount);
        }

        // GET: /SpecialDiscounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SpecialDiscounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,DiscountName,AllowSpecialDiscount,DiscountValue,IsHappyHour")] SpecialDiscount specialDiscount)
        {
            if (ModelState.IsValid)
            {
                db.SpecialDiscounts.Add(specialDiscount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialDiscount);
        }

        // GET: /SpecialDiscounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialDiscount specialDiscount = db.SpecialDiscounts.Find(id);
            if (specialDiscount == null)
            {
                return HttpNotFound();
            }
            return View(specialDiscount);
        }

        // POST: /SpecialDiscounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,DiscountName,AllowSpecialDiscount,DiscountValue,IsHappyHour")] SpecialDiscount specialDiscount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialDiscount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialDiscount);
        }

        // GET: /SpecialDiscounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialDiscount specialDiscount = db.SpecialDiscounts.Find(id);
            if (specialDiscount == null)
            {
                return HttpNotFound();
            }
            return View(specialDiscount);
        }

        // POST: /SpecialDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecialDiscount specialDiscount = db.SpecialDiscounts.Find(id);
            db.SpecialDiscounts.Remove(specialDiscount);
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
