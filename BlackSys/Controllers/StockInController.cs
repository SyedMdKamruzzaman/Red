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
    public class StockInController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /StockIn/
        public ActionResult Index()
        {
            var stockinlist = (from data in db.Stockins.AsEnumerable()                            
                            join bnh in db.Products.AsEnumerable()
                            on data.productid equals bnh.productid
                            join brnch in db.Branchs.AsEnumerable()
                            on data.branchid equals brnch.BranchId
                            select new StockInModel()
                            {
                                stockinid = data.stockinid,
                                
                                product = bnh.product,
                                stocktakedate = data.stocktakedate,
                                branch = brnch.BranchName,
                                quantity = data.quantity,
                                invoiceno = data.invoiceno
                                                               
                            }).ToList();

            return View(stockinlist);
        }

        // GET: /StockIn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockInModel stockinmodel = db.Stockins.Find(id);
            if (stockinmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockinmodel);
        }


        //Search Product based on req and purchase done

        public ActionResult SearchRequisition(int? ReqID)
        {
            ViewBag.unitname = db.UnitModels.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.condition = db.ProductConditionModels.Select(a => new { a.ID, a.ConditionName }).ToList();
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            ViewBag.Products = db.Products.AsEnumerable().Select(a => new { a.productid, a.product }).ToList();
            ViewBag.Category = db.RequisitionItemCategoryModels.AsEnumerable().Select(a => new { a.ID, a.Category }).ToList();

            StockInModel stockin = new StockInModel();
            try
            {
                bool purcexist = db.PurchaseOrders.Count(c => c.ReqID == ReqID) > 0;
                if (purcexist)
                {
                    if (ReqID != null)
                    {
                        stockin = (from r in db.PurchaseOrders
                                   join brn in db.Branchs on r.BranchID equals brn.BranchId
                                         join itm in db.Products.AsEnumerable() on r.Item equals itm.productid                                                                     
                                         join unt in db.UnitModels.AsEnumerable() on r.UnitId equals unt.Id
                                        
                                         

                                         select new
                                         {
                                             r.ReqID,
                                             r.ID,                                             
                                             itm.product,                                            
                                             r.Item,
                                             r.BranchID,                                                                                          
                                             r.Quantity,
                                             brn.BranchName,                                                                                          
                                             r.UnitId,
                                             unt.Name
                                           
                                         }).AsEnumerable().Where(r => r.ReqID == ReqID).
                                             Select(p => new StockInModel
                                             {                                                                                         
                                                 productid = Convert.ToInt16(p.Item),
                                                 product = p.product,
                                                 quantity = p.Quantity,
                                                 branchid = p.BranchID,
                                                 branch = p.BranchName,                                                                                     
                                                 ReqID = p.ReqID,
                                                 UnitID = p.UnitId,
                                                 UnitName = p.Name

                                             }).FirstOrDefault();

                        //purchaseOrder.PurchasedQuantity = db.PurchaseOrders.Where(p => p.ReqID == ReqID).Select(s => s.Quantity).DefaultIfEmpty(0).Sum();
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
            //ViewBag.VendorNameList = db.Vendors.Where(v => v.IsActive == true).Select(a => new { a.ID, a.CompanyName }).ToList();
            return View("Create", stockin);
        }


        //







        // GET: /StockIn/Create
        public ActionResult Create()
        {
            ViewBag.unitname = db.UnitModels.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.condition = db.ProductConditionModels.Select(a => new { a.ID, a.ConditionName }).ToList();
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            ViewBag.Products = db.Products.AsEnumerable().Select(a => new { a.productid, a.product }).ToList();
           
            return View();
        }

        // POST: /StockIn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create(StockInModel stockinmodel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var quantity = db.Stocks.Where(s => s.productid == stockinmodel.productid).Where(x=>x.branchid == stockinmodel.branchid).Select(s => s.quantity).FirstOrDefault();



                    if (quantity > 0)
                    {
                        Stock stockModel = db.Stocks.Where(s => s.productid == stockinmodel.productid).FirstOrDefault();
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

                        entry.Property(s => s.quantity).CurrentValue = entry.Property(s => s.quantity).OriginalValue + stockinmodel.quantity;

                        try
                        {
                            db.SaveChanges();
                            TempData["ResultMessage"] = "Stock In Successfull.";
                            TempData["ResultType"] = "S";
                        }
                        catch (Exception ex)
                        {

                            TempData["ResultMessage"] = "Ops:Error saving data! " + ex.Message;
                            TempData["ResultType"] = "E";
                        };


                    }
                    else
                    {
                        //TempData["ResultMessage"] = "Create Product Information First than Stock In Process";
                        //TempData["ResultType"] = "E";
                        Stock stockModel = new Stock();
                        //Mapper.map<StockInModel, Stock>(stockinmodel, stockModel);                                            
                        stockModel.productid = stockinmodel.productid;
                        stockModel.stocktakedate = stockinmodel.stocktakedate;
                        stockModel.categoryid = (from pr in db.Products.AsEnumerable()
                                                 join ct in db.RequisitionItemCategoryModels.AsEnumerable()
                                                 on pr.categoryid equals ct.ID select stockModel.categoryid = ct.ID).FirstOrDefault();

                        stockModel.quantity = stockinmodel.quantity;
                        stockModel.branchid = stockinmodel.branchid;
                        stockModel.invoiceno = stockinmodel.invoiceno;
                        stockModel.condition = stockinmodel.condition;
                        stockModel.remarks = stockinmodel.remarks;                        

                        db.Stocks.Add(stockModel);
                    }

                    db.Stockins.Add(stockinmodel);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Stock");
                }
                ViewBag.unitname = db.UnitModels.Select(a => new { a.Id, a.Name }).ToList();
                ViewBag.condition = db.ProductConditionModels.Select(a => new { a.ID, a.ConditionName }).ToList();
                var userId = User.Identity.GetUserId();
                ViewBag.Braches = CommonRepository.GetBranchList(userId);
                ViewBag.Products = db.Products.AsEnumerable().Select(a => new { a.productid, a.product }).ToList();
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(stockinmodel);
        }

        public ActionResult GetRequisitionItemCategory(string q)
        {
            q = q.ToUpper();
            var cat = db.RequisitionItemCategoryModels
                .Where(a => a.Category.ToUpper().Contains(q))
                .Select(a => new { name = a.Category, id = a.ID });

            return Json(cat, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProduct(string q, string r)
        {
            q = q.ToUpper();
            var users = db.Products
                .Where(a => a.product.ToUpper().Contains(q))
                .Select(a => new { id = a.productid, name = a.product });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        // GET: /StockIn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockInModel stockinmodel = db.Stockins.Find(id);
            if (stockinmodel == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            return View(stockinmodel);
        }

        // POST: /StockIn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StockInModel stockinmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockinmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            return View(stockinmodel);
        }

        // GET: /StockIn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockInModel stockinmodel = db.Stockins.Find(id);
            if (stockinmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockinmodel);
        }

        // POST: /StockIn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockInModel stockinmodel = db.Stockins.Find(id);
            db.Stockins.Remove(stockinmodel);
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
