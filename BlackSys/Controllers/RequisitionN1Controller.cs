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
using BlackSys.Models.ViewModels;
using System.Text;
using BlackSys.Filter;
using System.Data.Entity.Infrastructure;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    public class RequisitionN1Controller : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        

        public void InitializeField()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
        }

        // GET: /RequisitionN1/
        public ActionResult Index()
        {
            var requisitionlist = (from data in db.Requisitions.AsEnumerable()
                                   join bnh in db.Products.AsEnumerable()
                                   on data.Item equals bnh.productid
                                   join brnch in db.Branchs.AsEnumerable()
                                   on data.BranchID equals brnch.BranchId
                                   select new RequisitionN1ViewModel()
                                   {
                                       ID= data.ID,
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

        // GET: /RequisitionN1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionN1ViewModel requisitionn1viewmodel = db.RequisitionN1ViewModel.Find(id);
            if (requisitionn1viewmodel == null)
            {
                return HttpNotFound();
            }
            return View(requisitionn1viewmodel);
        }

        // GET: /RequisitionN1/Create
        public ActionResult Create()
        {
            InitializeField();
            return View();
        }

        // POST: /RequisitionN1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RequisitionId,RequisitionDate,Item,Specification,Quantity,BranchID,Remarks,RequisitionApprovalDate,ApprovalStatus,ApprovalQuantity,ApprovalRemarks")] RequisitionN1ViewModel requisitionn1viewmodel)
        {
            if (ModelState.IsValid)
            {
                db.RequisitionN1ViewModel.Add(requisitionn1viewmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            InitializeField();
            return View(requisitionn1viewmodel);
        }

        // GET: /RequisitionN1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            RequisitionN1ViewModel requisitionn1viewmodel = new RequisitionN1ViewModel();

            Requisition requisition = db.Requisitions.Find(id);

            requisitionn1viewmodel.ID = requisition.ID;
            requisitionn1viewmodel.RequisitionDate = requisition.RequisitionDate;
            requisitionn1viewmodel.product = db.Products.Where(w=>w.productid== requisition.Item).Select(s=>s.product).SingleOrDefault();
            requisitionn1viewmodel.Specification = requisition.Specification;
            requisitionn1viewmodel.Quantity = requisition.Quantity;
            requisitionn1viewmodel.BranchID = requisition.BranchID;
            requisitionn1viewmodel.Remarks = requisition.Remarks;
            requisitionn1viewmodel.RequisitionApprovalDate = requisition.RequisitionApprovalDate;
            requisitionn1viewmodel.ApprovalStatus = requisition.ApprovalStatus;
            requisitionn1viewmodel.ApprovalQuantity = requisition.ApprovalQuantity;
            requisitionn1viewmodel.ApprovalRemarks = requisition.ApprovalRemarks;

            if (requisitionn1viewmodel == null)
            {
                return HttpNotFound();
            }
            InitializeField();
            return View(requisitionn1viewmodel);
        }

        // POST: /RequisitionN1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                DbEntityEntry<Requisition> dbEntity = db.Entry(requisition);
                dbEntity.State = EntityState.Modified;
                dbEntity.Property(d => d.Item).IsModified = false;              
                db.SaveChanges();
                return RedirectToAction("Index");

                //var requisitionlist = (from data in db.Requisitions.AsEnumerable()
                //                       join bnh in db.Products.AsEnumerable() on data.Item equals bnh.productid
                //                       join brnch in db.Branchs.AsEnumerable() on data.BranchID equals brnch.BranchId
                //                       select new RequisitionN1ViewModel()
                //                       {
                //                           ID = data.ID,
                //                           RequisitionDate = data.RequisitionDate,
                //                           product = bnh.product,
                //                           Specification = data.Specification,
                //                           Quantity = data.Quantity,
                //                           branch = brnch.BranchName,
                //                           RequisitionApprovalDate = data.RequisitionApprovalDate,
                //                           ApprovalStatus = data.ApprovalStatus,
                //                           ApprovalQuantity = data.ApprovalQuantity,
                //                           ApprovalRemarks = data.ApprovalRemarks
                //                       }).ToList();

                //return View("Index",requisitionlist);
            }
            InitializeField();


            return View(requisition);
        }

        // GET: /RequisitionN1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequisitionN1ViewModel requisitionn1viewmodel = db.RequisitionN1ViewModel.Find(id);
            if (requisitionn1viewmodel == null)
            {
                return HttpNotFound();
            }            
            return View(requisitionn1viewmodel);
        }

        // POST: /RequisitionN1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequisitionN1ViewModel requisitionn1viewmodel = db.RequisitionN1ViewModel.Find(id);
            db.RequisitionN1ViewModel.Remove(requisitionn1viewmodel);
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
