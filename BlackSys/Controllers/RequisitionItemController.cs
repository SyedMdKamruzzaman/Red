using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using System.Text;
using BlackSys.Filter;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    public class RequisitionItemController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /RequisitionItem/
        public ActionResult Index()
        {
            var requisitionitemlist = (from data in db.RequisitionItems.AsEnumerable()
                                       join cat in db.RequisitionItemCategoryModels.AsEnumerable()
                                        on data.Category equals cat.ID                                   
                                   select new RequisitionItem()
                                   {
                                       category = cat.Category,
                                       Item = data.Item
                                   }).ToList();

            return View(requisitionitemlist);
        }

        // GET: /RequisitionItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItem requisitionitem = db.RequisitionItems.Find(id);
            if (requisitionitem == null)
            {
                return HttpNotFound();
            }
            return View(requisitionitem);
        }

        // GET: /RequisitionItem/Create
        public ActionResult Create()
        {
            //var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            //ViewBag.Braches = db.Branchs.Where(x => x.BranchId == branchId);
            return View();
        }

        public ActionResult GetRequisitionItemCategory(string q)
        {
            q = q.ToUpper();
            var users = db.RequisitionItemCategoryModels
                .Where(a => a.Category.ToUpper().StartsWith(q))
                .Select(a => new { id = a.ID, name = a.Category });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        // POST: /RequisitionItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Category,Item,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] RequisitionItem requisitionitem)
        {
            if (ModelState.IsValid)
            {
                db.RequisitionItems.Add(requisitionitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requisitionitem);
        }

        // GET: /RequisitionItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItem requisitionitem = db.RequisitionItems.Find(id);
            if (requisitionitem == null)
            {
                return HttpNotFound();
            }
            return View(requisitionitem);
        }

        // POST: /RequisitionItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Category,Item,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] RequisitionItem requisitionitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisitionitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requisitionitem);
        }

        // GET: /RequisitionItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItem requisitionitem = db.RequisitionItems.Find(id);
            if (requisitionitem == null)
            {
                return HttpNotFound();
            }
            return View(requisitionitem);
        }

        // POST: /RequisitionItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitionItem requisitionitem = db.RequisitionItems.Find(id);
            db.RequisitionItems.Remove(requisitionitem);
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
