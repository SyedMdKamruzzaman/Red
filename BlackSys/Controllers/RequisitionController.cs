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
using Blacksys.Controllers;

namespace BlackSys.Controllers
{
    public class RequisitionController : Controller
    {
        //
        // GET: /Requisition/
        private BlackSysEntities db = new BlackSysEntities();
        CommonRepository commonRepository = new CommonRepository();


        // GET: /Requisition/
        [Authorize]
        public ActionResult Index()
        {
            var requisitionlist = (from data in db.Requisitions.AsEnumerable()
                               join bnh in db.Products.AsEnumerable()
                               on data.Item equals bnh.productid
                               join brnch in db.Branchs.AsEnumerable()
                               on data.BranchID equals brnch.BranchId
                               select new Requisition()
                               {
                                   ID = data.ID,
                                   RequisitionDate = data.RequisitionDate,
                                   product = bnh.product,
                                   Specification = data.Specification,
                                   Quantity = data.Quantity,
                                   branch = brnch.BranchName,
                                   RequisitionApprovalDate = data.RequisitionApprovalDate,
                                   ApprovalStatus = data.ApprovalStatus,
                                   ApprovalQuantity = data.ApprovalQuantity,
                                   ApprovalRemarks = data.ApprovalRemarks

                               }).ToList();

            return View(requisitionlist);
        }

        public void InitializeField()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
        }

        // GET: /Requisition/Details/5
        [Authorize]
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            return View(requisition);
        }

        // GET: /Requisition/Create
        [Authorize]
        public ActionResult Create()
        {
            InitializeField();
            return View();
        }

        public ActionResult GetRequisitionItem(string q)
        {
            q = q.ToUpper();
            var users = db.Products
                .Where(a => a.product.ToUpper().Contains(q))                
                .Select(a => new { name = a.product, id= a.productid });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        // POST: /Requisition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ID,RequisitionDate,Item,Specification,Quantity,BranchID,Remarks,RequisitionApprovalDate,ApprovalStatus,ApprovalQuantity,ApprovalRemarks")] Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                db.Requisitions.Add(requisition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            InitializeField();
            return View(requisition);
        }

        // GET: /Requisition/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            InitializeField();
            return View(requisition);
        }

        


        // POST: /Requisition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,RequisitionDate,Item,Specification,Quantity,BranchID,Remarks,RequisitionApprovalDate,ApprovalStatus,ApprovalQuantity,ApprovalRemarks")] Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            InitializeField();
            return View(requisition);
        }

        // GET: /Requisition/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            return View(requisition);
        }

        // POST: /Requisition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisition requisition = db.Requisitions.Find(id);
            db.Requisitions.Remove(requisition);
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

        public ActionResult PurchaseOrderIndex()
        {
            var requisitionList = (from req in db.Requisitions.AsEnumerable().Where(r => r.ApprovalStatus.Equals(ApprovalStatus.Approved))
                                   join pur in db.PurchaseOrders.AsEnumerable() on req.ID equals pur.ReqID into reqpur
                                   from subpet in reqpur.DefaultIfEmpty()
                                   join bnh in db.Branchs.AsEnumerable()
                                   on req.BranchID equals bnh.BranchId
                                   join itm in db.RequisitionItems.AsEnumerable()
                                   on req.Item equals itm.ID
                                   select new PurchaseOrderViewModel()
                                   {
                                       ID = req.ID,
                                       RequisitionDate = req.RequisitionApprovalDate,
                                       product = itm.Item,
                                       branch = bnh.BranchName,
                                       Specification = req.Specification,
                                       ApprovalQuantity = req.ApprovalQuantity,
                                       ApprovalRemarks = req.ApprovalRemarks,
                                       PurchaseID = (subpet.ID == null ? 0 : subpet.ID)
                                   }).ToList();

            return View("PurchaseOrderIndex", requisitionList);
        }


        public ActionResult CreatePurchaseOrder(int? id)
        {
            PurchaseOrderViewModel purchaseOrderViewModel = (from req in db.Requisitions.AsEnumerable().Where(r => r.ApprovalStatus.Equals(ApprovalStatus.Approved))
                                                             join bnh in db.Branchs.AsEnumerable()
                                                             on req.BranchID equals bnh.BranchId
                                                             join prod in db.Products.AsEnumerable()
                                                             on req.Item equals prod.productid
                                                             select new PurchaseOrderViewModel()
                                                             {
                                                                 ID = req.ID,
                                                                 RequisitionDate = req.RequisitionApprovalDate,
                                                                 product = prod.product,
                                                                 branch = bnh.BranchName,
                                                                 Specification = req.Specification,
                                                                 ApprovalQuantity = req.ApprovalQuantity,
                                                                 ApprovalRemarks = req.ApprovalRemarks

                                                             }).Where(r => r.ID == id).FirstOrDefault();

            return View("PurchaseOrderCreate", purchaseOrderViewModel);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreatePurchaseOrder(PurchaseOrderViewModel purchaseOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();
                purchaseOrder.PurchaseDate = purchaseOrderViewModel.PurchaseDate;
                purchaseOrder.ReqID = purchaseOrderViewModel.ID;
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();
                return RedirectToAction("PurchaseOrderIndex");
            }
            //var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            //ViewBag.Braches = db.Branchs.Where(x => x.BranchId == branchId);
            return View("PurchaseOrderCreate", purchaseOrderViewModel);
        }

        public ActionResult EditPurchaseOrder(int? id)
        {
            PurchaseOrderViewModel purchaseOrderViewModel = (from req in db.Requisitions.AsEnumerable().Where(r => r.ApprovalStatus.Equals(ApprovalStatus.Approved))
                                                             join bnh in db.Branchs.AsEnumerable()
                                                             on req.BranchID equals bnh.BranchId
                                                             join prod in db.Products.AsEnumerable()
                                                             on req.Item equals prod.productid
                                                             select new PurchaseOrderViewModel()
                                                             {
                                                                 ID = req.ID,
                                                                 RequisitionDate = req.RequisitionApprovalDate,
                                                                 product = prod.product,
                                                                 branch = bnh.BranchName,
                                                                 Specification = req.Specification,
                                                                 ApprovalQuantity = req.ApprovalQuantity,
                                                                 ApprovalRemarks = req.ApprovalRemarks

                                                             }).Where(r => r.ID == id).FirstOrDefault();

            return View("PurchaseOrderCreate", purchaseOrderViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EditPurchaseOrder(PurchaseOrderViewModel purchaseOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();
                purchaseOrder.PurchaseDate = purchaseOrderViewModel.PurchaseDate;
                purchaseOrder.ReqID = purchaseOrderViewModel.ID;
                db.PurchaseOrders.Add(purchaseOrder);
                db.SaveChanges();
                return RedirectToAction("PurchaseOrderIndex");
            }
            //var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            //ViewBag.Braches = db.Branchs.Where(x => x.BranchId == branchId);
            return View("PurchaseOrderCreate", purchaseOrderViewModel);
        }
    }
}
