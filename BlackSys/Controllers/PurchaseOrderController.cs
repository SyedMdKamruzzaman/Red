using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;
using System.Data.Entity.Infrastructure;


namespace BlackSys.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /PurchaseOrder/
        public ActionResult Index()
        {
           
               var  purchaseOrderlist = (from p in db.PurchaseOrders.AsEnumerable()
                                 join brn in db.Branchs.AsEnumerable() on p.BranchID equals brn.BranchId
                                 join itm in db.Products.AsEnumerable() on p.Item equals itm.productid
                                 join ven in db.Vendors.AsEnumerable() on p.VendorId equals ven.ID
                                 select new PurchaseOrder()
                                 {       
                                     ID = p.ID,                          
                                         ReqID = p.ReqID,
                                         RequisitionDate = p.RequisitionDate,
                                         product = itm.product,
                                         PurchaseDate = p.PurchaseDate,                                         
                                         branch = brn.BranchName,
                                         ApprovalQuantity = p.ApprovalQuantity,
                                         PurchasedQuantity = p.Quantity,
                                         VendorName = ven.CompanyName

                                 }).OrderByDescending(a => a.ReqID).ThenByDescending(a=>a.PurchaseDate).ToList();
               
            
            return View(purchaseOrderlist);
        }

        // GET: /PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorder);
        }

        // GET: /PurchaseOrder/Create
        public ActionResult Create()
        {
            ViewBag.VendorNameList = db.Vendors.Where(v => v.IsActive == true).Select(a => new { a.ID, a.CompanyName }).ToList();
            ViewBag.unitname = db.UnitModels.Select(a => new { a.Id, a.Name }).ToList();
            return View();
        }

        public ActionResult SearchRequisition(int? ReqID)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            try
            {
                bool redexist = db.Requisitions.Count(c => c.ID == ReqID) > 0;
                if (redexist)
                {
                    if (ReqID != null)
                    {
                        purchaseOrder = (from r in db.Requisitions
                                         join brn in db.Branchs on r.BranchID equals brn.BranchId
                                         join itm in db.Products.AsEnumerable() on r.Item equals itm.productid
                                        
                                         select new
                                         {
                                             r.ID,
                                             r.RequisitionDate,
                                             itm.productid,
                                             itm.product,
                                             r.Specification,
                                             brn.BranchName,
                                             r.ApprovalQuantity,
                                             r.ApprovalRemarks,
                                             brn.BranchId,                                            
                                             r.Quantity
                                         }).AsEnumerable().Where(r => r.ID == ReqID).
                                             Select(p => new PurchaseOrder
                                             {
                                                 ReqID = p.ID,
                                                 BranchID = p.BranchId,
                                                 RequisitionDate = p.RequisitionDate,
                                                 Item = p.productid,
                                                 product = p.product,
                                                 Specification = p.Specification,
                                                 branch = p.BranchName,
                                                 ApprovalQuantity = p.ApprovalQuantity,
                                                 ApprovalRemarks = p.ApprovalRemarks,
                                                PurchasedQuantity = p.ApprovalQuantity
                                                 

                                             }).FirstOrDefault();

                        //purchaseOrder.PurchasedQuantity = db.PurchaseOrders.Where(p => p.ReqID == ReqID).Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
                        ViewBag.unitname = db.UnitModels.Select(a => new { a.Id, a.Name }).ToList();
                    }
                }
                else
                {
                    TempData["ResultMessage"] = "The Requisition ID" + "-" + ReqID + "_" + "Not Found into the System";
                    TempData["ResultType"] = "E";
                }
                
                
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.VendorNameList = db.Vendors.Where(v => v.IsActive == true).Select(a => new { a.ID, a.CompanyName }).ToList();
            return View("Create", purchaseOrder);
        }

        // POST: /PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PurchaseOrder purchaseorder)
        {
            if (ModelState.IsValid)
            {
                
                db.PurchaseOrders.Add(purchaseorder);
                db.SaveChanges();
                if (purchaseorder.IsChecked)
                {
                    var quantity = db.Stocks.Where(s => s.productid == purchaseorder.Item).Where(x => x.branchid == purchaseorder.BranchID).Select(s => s.quantity).FirstOrDefault();



                    if (quantity > 0)
                    {
                        Stock stockModel = db.Stocks.Where(s => s.productid == purchaseorder.Item).FirstOrDefault();
                        DbEntityEntry<Stock> entry = db.Entry(stockModel);
                        entry.State = EntityState.Modified;
                        entry.Property(s => s.branchid).IsModified = false;
                        entry.Property(s => s.condition).IsModified = false;
                        entry.Property(s => s.EntryBy).IsModified = false;
                        entry.Property(s => s.EntryByIp).IsModified = false;
                        entry.Property(s => s.EntryDateTime).IsModified = false;
                        entry.Property(s => s.invoiceno).IsModified = false;
                        entry.Property(s => s.productid).IsModified = false;
                        entry.Property(s => s.categoryid).IsModified = false;

                        entry.Property(s => s.quantity).CurrentValue = entry.Property(s => s.quantity).OriginalValue + purchaseorder.Quantity;

                      
                            db.SaveChanges();
                           
                      


                    }
                    else
                    {
                        //TempData["ResultMessage"] = "Create Product Information First than Stock In Process";
                        //TempData["ResultType"] = "E";
                        Stock stockModel = new Stock();
                        //Mapper.map<StockInModel, Stock>(stockinmodel, stockModel);                                            
                        stockModel.productid =Convert.ToInt16(purchaseorder.Item);
                        stockModel.stocktakedate = purchaseorder.PurchaseDate;
                        stockModel.categoryid = (from pr in db.Products.AsEnumerable()
                                                 join ct in db.RequisitionItemCategoryModels.AsEnumerable()
                                                 on pr.categoryid equals ct.ID
                                                 select stockModel.categoryid = ct.ID).FirstOrDefault();

                        stockModel.quantity = purchaseorder.Quantity;
                        stockModel.branchid = purchaseorder.BranchID;
                        stockModel.invoiceno = 000000;
                        stockModel.condition = 1;
                        stockModel.remarks = purchaseorder.Remarks;

                        db.Stocks.Add(stockModel);
                    }

                    StockInModel stockinmodel = new StockInModel();
                    stockinmodel.ReqID = purchaseorder.ReqID;
                    stockinmodel.productid = Convert.ToInt16(purchaseorder.Item);
                    stockinmodel.stocktakedate = purchaseorder.PurchaseDate;
                    stockinmodel.quantity = purchaseorder.Quantity;
                    stockinmodel.branchid = purchaseorder.BranchID;
                    stockinmodel.invoiceno = 000000;
                    stockinmodel.condition = 1;
                    stockinmodel.remarks = purchaseorder.Remarks;
                    stockinmodel.UnitID = purchaseorder.UnitId;
                    db.Stockins.Add(stockinmodel);
                    db.SaveChanges();

                    TempData["ResultMessage"] = "Stock In Successfull.";
                    TempData["ResultType"] = "S";

                }
                    return RedirectToAction("Index");
            }
            ViewBag.unitname = db.UnitModels.Select(a => new { a.Id, a.Name }).ToList();
            return View(purchaseorder);
        }

        // GET: /PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorder);
        }

        // POST: /PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PurchaseID,RequisitionDate,PurchaseDate,Item,Specification,Quantity,BranchID,Remarks,ApprovalQuantity,ApprovalRemarks,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] PurchaseOrder purchaseorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseorder);
        }

        // GET: /PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorder);
        }

        // POST: /PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(purchaseorder);
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
