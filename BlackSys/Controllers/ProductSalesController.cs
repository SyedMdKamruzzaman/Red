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
using System.Web.Configuration;

namespace BlackSys.Controllers
{
    public class ProductSalesController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /ProductSales/
        public ActionResult Index()
        {
            return View(db.ProductSales.ToList());
        }

        // GET: /ProductSales/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSales productSales = db.ProductSales.Find(id);
            if (productSales == null)
            {
                return HttpNotFound();
            }
            return View(productSales);
        }

        // GET: /ProductSales/Create
        public ActionResult Create()
        {
            ViewBag.condition = db.ProductConditionModels.Select(a => new { a.ID, a.ConditionName }).ToList();
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            ViewBag.Products = db.Products.AsEnumerable().Select(a => new { a.productid, a.product }).ToList();
            ViewBag.AccountsCategory = db.AccountsHeadCategorys.Where(w => w.category.ToUpper().Equals("INCOME")).ToList();
            ViewBag.Unit = db.UnitModels.ToList();
            return View();
        }

        public ActionResult GetProduct(string q)
        {
            q = q.ToUpper();
            var prod = db.Products
                .Where(a => a.product.ToUpper().Contains(q))
                .Select(a => new { name = a.product, id = a.productid });

            return Json(prod, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        // POST: /ProductSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ProductSales productSales)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ProductSales.Add(productSales);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(productSales);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductSales productsales)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var quantity = db.Stocks.Where(s => s.productid == productsales.ItemId).Where(x => x.branchid == productsales.BranchId).Select(s => s.quantity).FirstOrDefault();



                    if (quantity > 0)
                    {
                        Stock stockModel = db.Stocks.Where(s => s.productid == productsales.ItemId).FirstOrDefault();
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

                        entry.Property(s => s.quantity).CurrentValue = entry.Property(s => s.quantity).OriginalValue - productsales.Quantity;

                        Transaction trn = new Transaction();
                        var accheadcatid = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceCategoryId"]);
                        var accheadSubcatid = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceSubCategoryId"]);
                        var peoductSalesAccid = Convert.ToInt16(WebConfigurationManager.AppSettings["ProductSalesAccountId"]);
                        trn.CategoryID = accheadcatid;
                        trn.SubCategoryID = accheadSubcatid;
                        trn.AccountsName = peoductSalesAccid.ToString();
                        trn.CreditDebitFlag = "C";
                        trn.Amount = productsales.SubTotal;
                        trn.Date = productsales.SalesDate;
                        trn.Remarks = "Product Sales";
                        trn.AppointmentID = Convert.ToInt32(productsales.Id) * 0;
                        trn.BranchId = db.ProductSales.Where(x => x.BranchId == trn.BranchId).Select(x => x.BranchId).SingleOrDefault();
                        db.Transactions.Add(trn);                       

                        try
                        {
                            db.SaveChanges();
                            TempData["ResultMessage"] = "Stock out Successfull.";
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
                        TempData["ResultMessage"] = "You dont have stock currently in this Branch";
                        TempData["ResultType"] = "E";
                        
                    }

                    db.ProductSales.Add(productsales);
                    db.SaveChanges();

                    return RedirectToAction("Create", "ProductSales");
                }
               
                ViewBag.condition = db.ProductConditionModels.Select(a => new { a.ID, a.ConditionName }).ToList();
                var userId = User.Identity.GetUserId();
                ViewBag.Braches = CommonRepository.GetBranchList(userId);
                ViewBag.Products = db.Products.AsEnumerable().Select(a => new { a.productid, a.product }).ToList();

                ViewBag.AccountsCategory = db.AccountsHeadCategorys.Where(w => w.category.ToUpper().Equals("INCOME")).ToList();
                ViewBag.Unit = db.UnitModels.ToList();             
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(productsales);
        }

        // GET: /ProductSales/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSales productSales = db.ProductSales.Find(id);
            if (productSales == null)
            {
                return HttpNotFound();
            }
            return View(productSales);
        }

        // POST: /ProductSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductSales productSales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productSales);
        }

        // GET: /ProductSales/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSales productSales = db.ProductSales.Find(id);
            if (productSales == null)
            {
                return HttpNotFound();
            }
            return View(productSales);
        }

        // POST: /ProductSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ProductSales productSales = db.ProductSales.Find(id);
            db.ProductSales.Remove(productSales);
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

        public ActionResult DailySalesReport()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View();
        }

        public ActionResult GetRequisitionItem(string q)
        {
            q = q.ToUpper();
            var users = db.Products
                .Where(a => a.product.ToUpper().Contains(q))
                .Select(a => new { name = a.product, id = a.productid });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DailySalesReport(string StartDate, string EndDate, int? ItemId, int? BranchId)
        {
            ViewBag.ReportViewer = DailySalesReportView(StartDate, EndDate, ItemId, BranchId);

            return View("_ReportViewer");

        }
        public ReportViewer DailySalesReportView(string StartDate, string EndDate, int? ItemId, int? BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from ps in db.ProductSales.AsEnumerable()
                        join ac in db.AccountsHeadCategorys.AsEnumerable() on ps.AccountsCategoryId equals ac.ID
                        join it in db.Products.AsEnumerable() on ps.ItemId equals it.productid
                        join br in db.Branchs.AsEnumerable() on ps.BranchId equals br.BranchId
                        join um in db.UnitModels.AsEnumerable() on ps.UnitId equals um.Id
                        select new
                        {
                            it.productid,
                            it.product,
                            ac.category,
                            ps.Quantity,
                            um.Name,
                            ps.UnitPrice,
                            ps.SubTotal,
                            ps.SalesDate,
                            br.BranchId,
                            br.BranchName
                        };

            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.SalesDate >= StartDateRange);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.SalesDate <= EndDateRange);
            }

            if (ItemId != null && ItemId != 0)
            {
                query = query.Where(s => s.productid == ItemId);
            }

           
            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.BranchId == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new
                                    {
                                        s.category,
                                        s.product,                                        
                                        s.Quantity,
                                        s.Name,
                                        s.UnitPrice,
                                        s.SubTotal,
                                        s.SalesDate,
                                        s.BranchName
                                    }).ToList();


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ProductSalesReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSProductSales", reportDS));


            return reportViewer;

        }
    }
}
