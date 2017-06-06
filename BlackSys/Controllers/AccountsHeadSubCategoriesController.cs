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
    public class AccountsHeadSubCategoriesController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /AccountsHeadSubCategories/
        public ActionResult Index()
        {
            var model = (from sc in db.AccountsHeadSubCategories.AsEnumerable()
                         join ct in db.AccountsHeadCategorys.AsEnumerable() on sc.CategoryID equals ct.ID
                         select new AccountsHeadSubCategory
                         {
                             Id = sc.Id,
                             CategoryName = ct.category,
                             SubCategoryName = sc.SubCategoryName
                         }).ToList();
            return View(model);
        }

        // GET: /AccountsHeadSubCategories/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsHeadSubCategory accountsHeadSubCategory = db.AccountsHeadSubCategories.Find(id);
            if (accountsHeadSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(accountsHeadSubCategory);
        }

        // GET: /AccountsHeadSubCategories/Create
        public ActionResult Create()
        {
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            return View();
        }

        // POST: /AccountsHeadSubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CategoryID,SubCategoryName,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] AccountsHeadSubCategory accountsHeadSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.AccountsHeadSubCategories.Add(accountsHeadSubCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountsHeadSubCategory);
        }

        // GET: /AccountsHeadSubCategories/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsHeadSubCategory accountsHeadSubCategory = db.AccountsHeadSubCategories.Find(id);
            if (accountsHeadSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(accountsHeadSubCategory);
        }

        // POST: /AccountsHeadSubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountsHeadSubCategory accountsHeadSubCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountsHeadSubCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountsHeadSubCategory);
        }

        // GET: /AccountsHeadSubCategories/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsHeadSubCategory accountsHeadSubCategory = db.AccountsHeadSubCategories.Find(id);
            if (accountsHeadSubCategory == null)
            {
                return HttpNotFound();
            }
            return View(accountsHeadSubCategory);
        }

        // POST: /AccountsHeadSubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AccountsHeadSubCategory accountsHeadSubCategory = db.AccountsHeadSubCategories.Find(id);
            db.AccountsHeadSubCategories.Remove(accountsHeadSubCategory);
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
