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
    public class AccountHeadCategoryController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /AccountHeadCategory/
        public ActionResult Index()
        {
            return View(db.AccountsHeadCategorys.ToList());
        }

        // GET: /AccountHeadCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsHeadCategory accountsheadcategory = db.AccountsHeadCategorys.Find(id);
            if (accountsheadcategory == null)
            {
                return HttpNotFound();
            }
            return View(accountsheadcategory);
        }

        // GET: /AccountHeadCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AccountHeadCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,category")] AccountsHeadCategory accountsheadcategory)
        {
            if (ModelState.IsValid)
            {
                db.AccountsHeadCategorys.Add(accountsheadcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountsheadcategory);
        }

        // GET: /AccountHeadCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsHeadCategory accountsheadcategory = db.AccountsHeadCategorys.Find(id);
            if (accountsheadcategory == null)
            {
                return HttpNotFound();
            }
            return View(accountsheadcategory);
        }

        // POST: /AccountHeadCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,category")] AccountsHeadCategory accountsheadcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountsheadcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountsheadcategory);
        }

        // GET: /AccountHeadCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsHeadCategory accountsheadcategory = db.AccountsHeadCategorys.Find(id);
            if (accountsheadcategory == null)
            {
                return HttpNotFound();
            }
            return View(accountsheadcategory);
        }

        // POST: /AccountHeadCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountsHeadCategory accountsheadcategory = db.AccountsHeadCategorys.Find(id);
            db.AccountsHeadCategorys.Remove(accountsheadcategory);
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
