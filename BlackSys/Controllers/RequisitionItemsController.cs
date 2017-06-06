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

namespace BlackSys.Controllers
{
    public class RequisitionItemsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /RequisitionItems/
        public ActionResult Index()
        {
            var requisitionitemlist = (from data in db.RequisitionItems.AsEnumerable()
                                       join cat in db.RequisitionItemCategoryModels.AsEnumerable()
                                        on data.Category equals cat.ID
                                       select new RequisitionItems()
                                       {
                                           Category = Convert.ToInt16( cat.ID),
                                           Item = data.Item
                                       }).ToList();

            return View(requisitionitemlist);
        }

        // GET: /RequisitionItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItems RequisitionItems = db.RequisitionItems.Find(id);
            if (RequisitionItems == null)
            {
                return HttpNotFound();
            }
            return View(RequisitionItems);
        }

        // GET: /RequisitionItems/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetRequisitionItemCategory(string q)
        {
            q = q.ToUpper();
            var users = db.RequisitionItemCategoryModels
                .Where(a => a.Category.ToUpper().Contains(q))
                .Select(a => new { id = a.ID, name = a.Category });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        // POST: /RequisitionItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Category,Item,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] RequisitionItems RequisitionItems)
        {
            if (ModelState.IsValid)
            {
                db.RequisitionItems.Add(RequisitionItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(RequisitionItems);
        }

        // GET: /RequisitionItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItems RequisitionItems = db.RequisitionItems.Find(id);
            if (RequisitionItems == null)
            {
                return HttpNotFound();
            }
            return View(RequisitionItems);
        }

        // POST: /RequisitionItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Category,Item,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] RequisitionItems RequisitionItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(RequisitionItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(RequisitionItems);
        }

        // GET: /RequisitionItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionItems RequisitionItems = db.RequisitionItems.Find(id);
            if (RequisitionItems == null)
            {
                return HttpNotFound();
            }
            return View(RequisitionItems);
        }

        // POST: /RequisitionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitionItems RequisitionItems = db.RequisitionItems.Find(id);
            db.RequisitionItems.Remove(RequisitionItems);
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
