using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using System.Text;
using BlackSys.Filter;
using System.Data.Entity.Infrastructure;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace BlackSys.Controllers
{
    public class CategoryController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Category/
        public ActionResult Index()
        {
            return View(db.Categorys.ToList());
        }

        // GET: /Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categorymodel = db.Categorys.Find(id);
            if (categorymodel == null)
            {
                return HttpNotFound();
            }
            return View(categorymodel);
        }

        public ActionResult GetCategory(string q)
        {
            q = q.ToUpper();
            var users = db.Categorys
                .Where(a => a.category.ToUpper().Contains(q))
                .Select(a => new { id = a.categoryid, name = a.category });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="categoryid,category,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] CategoryModel categorymodel)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(categorymodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorymodel);
        }

        // GET: /Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categorymodel = db.Categorys.Find(id);
            if (categorymodel == null)
            {
                return HttpNotFound();
            }
            return View(categorymodel);
        }

        // POST: /Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="categoryid,category,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] CategoryModel categorymodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorymodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorymodel);
        }

        // GET: /Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel categorymodel = db.Categorys.Find(id);
            if (categorymodel == null)
            {
                return HttpNotFound();
            }
            return View(categorymodel);
        }

        // POST: /Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryModel categorymodel = db.Categorys.Find(id);
            db.Categorys.Remove(categorymodel);
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
