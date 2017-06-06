using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using BlackSys.Models.ViewModels;
using System.Text;
using BlackSys.Filter;
using System.Data.Entity.Infrastructure;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    public class RequisitionItemCategoryController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /RequisitionItemCategory/
        public ActionResult Index()
        {
            return View(db.RequisitionItemCategoryModels.ToList());
        }

        // GET: /RequisitionItemCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItemCategoryModel requisitionitemcategorymodel = db.RequisitionItemCategoryModels.Find(id);
            if (requisitionitemcategorymodel == null)
            {
                return HttpNotFound();
            }
            return View(requisitionitemcategorymodel);
        }

        // GET: /RequisitionItemCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RequisitionItemCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Category,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] RequisitionItemCategoryModel requisitionitemcategorymodel)
        {
            if (ModelState.IsValid)
            {
                db.RequisitionItemCategoryModels.Add(requisitionitemcategorymodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requisitionitemcategorymodel);
        }

        // GET: /RequisitionItemCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItemCategoryModel requisitionitemcategorymodel = db.RequisitionItemCategoryModels.Find(id);
            if (requisitionitemcategorymodel == null)
            {
                return HttpNotFound();
            }
            return View(requisitionitemcategorymodel);
        }

        // POST: /RequisitionItemCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Category,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] RequisitionItemCategoryModel requisitionitemcategorymodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisitionitemcategorymodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requisitionitemcategorymodel);
        }

        // GET: /RequisitionItemCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItemCategoryModel requisitionitemcategorymodel = db.RequisitionItemCategoryModels.Find(id);
            if (requisitionitemcategorymodel == null)
            {
                return HttpNotFound();
            }
            return View(requisitionitemcategorymodel);
        }

        // POST: /RequisitionItemCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitionItemCategoryModel requisitionitemcategorymodel = db.RequisitionItemCategoryModels.Find(id);
            db.RequisitionItemCategoryModels.Remove(requisitionitemcategorymodel);
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
