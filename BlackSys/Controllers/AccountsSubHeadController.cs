
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BlackSys.Models;
using System.Net;
using System.Data.Entity;


namespace BlackSys.Controllers
{
    public class AccountsSubHeadController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /AccountsSubHead/
        [Authorize]
        public ActionResult Index()
        {
            var myResult = (from data in db.AccountsSubHeads.AsEnumerable()
                            join lang in db.AccountsHeadCategorys.AsEnumerable() on data.CategoryID equals lang.ID
                            join sc in db.AccountsHeadSubCategories.AsEnumerable() on data.SubCategoryID equals sc.Id
                            select new AccountsSubHead()
                            {                         
                                CategoryName = lang.category,
                                SubCategoryName= sc.SubCategoryName,
                                AccountsName = data.AccountsName
                            }).ToList();
            return View(myResult);
          
        }

        // GET: /AccountsSubHead/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsSubHead accountssubhead = db.AccountsSubHeads.Find(id);
            if (accountssubhead == null)
            {
                return HttpNotFound();
            }
            return View(accountssubhead);
        }

        // GET: /AccountsSubHead/Create
        [Authorize]
        public ActionResult Create()
        {

            ViewBag.category = db.AccountsHeadCategorys.ToList();
            ViewBag.SubCategory = db.AccountsHeadSubCategories.ToList();
            return View();
        }

        // POST: /AccountsSubHead/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(AccountsSubHead accountssubhead)
        {
            if (ModelState.IsValid)
            {
                db.AccountsSubHeads.Add(accountssubhead);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category = db.AccountsHeadCategorys.ToList();                
            return View(accountssubhead);
        }

        // GET: /AccountsSubHead/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsSubHead accountssubhead = db.AccountsSubHeads.Find(id);
            if (accountssubhead == null)
            {
                return HttpNotFound();
            }
            //var ID = db.AccountsHeadCategorys.Where(u => u.ID == accountssubhead.CategoryID).Select(p => p.category).FirstOrDefault();
            //ViewBag.AccountsHeadCategorys = db.AccountsHeadCategorys.Where(x => x.ID == accountssubhead.CategoryID);         
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            return View(accountssubhead);
        }

        // POST: /AccountsSubHead/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(AccountsSubHead accountssubhead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountssubhead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //var ID = db.AccountsHeadCategorys.Where(u => u.ID == accountssubhead.CategoryID).Select(p => p.category).FirstOrDefault();
            //ViewBag.AccountsHeadCategorys = db.AccountsHeadCategorys.Where(x => x.ID == accountssubhead.CategoryID);  
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            return View(accountssubhead);
        }

        // GET: /AccountsSubHead/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountsSubHead accountssubhead = db.AccountsSubHeads.Find(id);
            if (accountssubhead == null)
            {
                return HttpNotFound();
            }
            return View(accountssubhead);
        }

        // POST: /AccountsSubHead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountsSubHead accountssubhead = db.AccountsSubHeads.Find(id);
            db.AccountsSubHeads.Remove(accountssubhead);
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