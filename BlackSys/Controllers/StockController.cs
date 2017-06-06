using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using Blacksys.Controllers;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace BlackSys.Controllers
{
    public class StockController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Stock/
        public ActionResult Index()
        {
            var stocklist = (from data in db.Stocks.AsEnumerable()
                               join lang in db.RequisitionItemCategoryModels.AsEnumerable() on data.categoryid equals lang.ID
                               join pro in db.Products.AsEnumerable() on data.productid equals pro.productid
                               join brnch in db.Branchs.AsEnumerable() on data.branchid equals brnch.BranchId
                               //join purc in db.PurchaseOrders.AsEnumerable() on data.productid equals purc.Item
                              join unt in db.UnitModels.AsEnumerable() on pro.UnitId equals unt.Id
                               select new Stock()
                               {
                                   stockid = data.stockid,
                                   category = lang.Category,
                                   product = pro.product,                                   
                                   branch = brnch.BranchName,
                                   quantity = data.quantity,
                                   Units= unt.Name
                                   //unitprice = purc.UnitPrice.ToString(),
                                   //totalprice = purc.SubTotal.ToString()

                               }).ToList();

            return View(stocklist);
        }

        // GET: /Stock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }


        public ActionResult StockReport()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(string StartDate, string EndDate, int? ProductId, int BranchId)
        {
            ViewBag.ReportViewer = StockReportView(StartDate, EndDate, ProductId, BranchId);

            return View("_StockReportView");

            //AppointmentViewModel appointViewModel =new AppointmentViewModel();
            //return View(appointViewModel);
        }
        public ReportViewer StockReportView(string StartDate, string EndDate, int? ProductId, int BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from stk in db.Stocks.AsEnumerable()
                        join pd in db.Products.AsEnumerable()
                        on stk.productid equals pd.productid
                        join purc in db.PurchaseOrders.AsEnumerable()
                        on stk.productid equals purc.Item
                        join brn in db.Branchs.AsEnumerable()
                        on stk.branchid equals brn.BranchId
                        select new { 
                                    stk.stocktakedate,
                                    stk.quantity,
                                    pd.product,
                                    stk.condition,
                                    brn.BranchName,
                                    pd.productid,
                                    stk.branchid
                        };







            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.stocktakedate >= StartDateRange);            
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.stocktakedate <= EndDateRange);
            }

            if (ProductId != null && ProductId != 0)
            {
                query = query.Where(s => s.productid == ProductId);
            }

            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.branchid == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new 
                                    { 
                                        s.stocktakedate,
                                        s.product,
                                        s.quantity,
                                        s.condition,
                                        s.BranchName
                                    }).ToList();


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportStock.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSStock", reportDS));


            return reportViewer; 
           
        }

        public ActionResult GetProduct(string q)
        {
            q = q.ToUpper();
            var prod = db.Products
                .Where(a => a.product.ToUpper().Contains(q))
                .Select(a => new { name = a.product, id = a.productid });

            return Json(prod, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }




        // GET: /Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        

        // POST: /Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="stockid,categoryid,productid,stocktakedate,quantity,branchid,invoiceno,condition,remarks,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        // GET: /Stock/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="stockid,categoryid,productid,stocktakedate,quantity,branchid,invoiceno,condition,remarks,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: /Stock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: /Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
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
