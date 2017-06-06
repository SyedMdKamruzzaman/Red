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
using Microsoft.Reporting.WebForms;
using System.Web.Configuration;
using OfficeOpenXml;

namespace BlackSys.Controllers
{
    public class TransactionController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        private CommonRepository commonRepository = new CommonRepository();
        // GET: /Transaction/
        public ActionResult Index()
        {
            var myResult = (from data in db.Transactions.AsEnumerable()
                            join lang in db.AccountsHeadCategorys.AsEnumerable() on data.CategoryID equals lang.ID
                            join sc in db.AccountsHeadSubCategories.AsEnumerable() on data.SubCategoryID equals sc.Id
                            join sub in db.AccountsSubHeads.AsEnumerable() on data.AccountsName equals sub.ID.ToString()

                            select new Transaction()
                            {
                                ID = data.ID,
                                CategoryID = lang.ID,
                                Category = lang.category,
                                SubCategoryID = sc.Id,
                                SubCategory = sc.SubCategoryName,
                                AccountsName = sub.AccountsName,
                                Amount = data.Amount,
                                Date = data.Date,
                                Remarks = data.Remarks,
                                CreditDebit = data.CreditDebitFlag.Equals("C") ? "Cr" : "Dr"

                            }).ToList();

            return View(myResult);
            //return View(db.Transactions.ToList());

        }

        // GET: /Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }


        public ActionResult GeneralExpCreate()
        {
            int AccountHeadCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseCatId"]);
            int AccountSubHeadCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseSubCatId"]);
            int AccountID = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseAccId"]);

            ViewBag.Category = db.AccountsHeadCategorys.Where(x => x.ID == AccountHeadCategoryID).ToList();
            ViewBag.SubCategory = db.AccountsHeadSubCategories.Where(x => x.Id == AccountSubHeadCategoryID).ToList();
            ViewBag.Accounts = db.AccountsSubHeads.Where(x => x.SubCategoryID == AccountSubHeadCategoryID).Select(a => new { a.ID, a.AccountsName }).ToList();
            return View();
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GeneralExpCreate(Transaction transaction)
        {
            int AccountHeadCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseCatId"]);
            int AccountSubHeadCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseSubCatId"]);
            int AccountID = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseAccId"]);

            ViewBag.Category = db.AccountsHeadCategorys.Where(x => x.ID == AccountHeadCategoryID).ToList();
            ViewBag.SubCategory = db.AccountsHeadSubCategories.Where(x => x.Id == AccountSubHeadCategoryID).ToList();
            ViewBag.Accounts = db.AccountsSubHeads.Where(x => x.SubCategoryID == AccountSubHeadCategoryID).Select(a=>new { a.ID, a.AccountsName }).ToList();


            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                
               
                TempData["ResultMessage"] = "Expenses information Saved Successfully.";
                TempData["ResultType"] = "S";
                return RedirectToAction("GeneralExpCreate", "Transaction");
            }
            else
            {
                TempData["ResultMessage"] = "Either fields are Empty or Enter valid Amount & Remarks";
                TempData["ResultType"] = "E";
            }
            return View();


        }


        // GET: /Transaction/Create
        public ActionResult Create()
        {
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            ViewBag.SubCategory = db.AccountsHeadSubCategories.ToList();
            ViewBag.AccountsName = db.AccountsSubHeads.ToList();
            var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            //ViewBag.Braches = db.Branchs.Where(w => w.BranchId == 1 ? w.BranchId != 0 : w.BranchId == branchId).ToList();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            return View();
        }

        public ActionResult GetSubHeadCategoryByCatId(int? CategoryId)
        {
            if(CategoryId == null)
            {
                TempData["ResultMessage"] = "Please Select Category Name";
                TempData["ResultType"] = "E";
                return Json("", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            else
            { 
                var subHeadCategory = db.AccountsHeadSubCategories.Where(x => x.CategoryID == CategoryId).ToList();
                return Json(subHeadCategory, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }

           
        }

        public ActionResult GetAccountBySubCatId(int? SubCategoryID)
        {
            if (SubCategoryID == null)
            {
                TempData["ResultMessage"] = "Please Select SubCategory Name";
                TempData["ResultType"] = "E";
                return Json("", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Account = db.AccountsSubHeads.Where(x => x.SubCategoryID == SubCategoryID).ToList();
                return Json(Account, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }


        }

        // POST: /Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
           
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category = db.AccountsHeadCategorys.ToList();
            ViewBag.SubCategory = db.AccountsHeadSubCategories.ToList();
            ViewBag.AccountsName = db.AccountsSubHeads.ToList();
            var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            //ViewBag.Braches = db.Branchs.Where(w => w.BranchId == 1 ? w.BranchId != 0 : w.BranchId == branchId).ToList();
            ViewBag.Braches =CommonRepository.GetBranchList(userId);
            return View(transaction);
        }

        // GET: /Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            ViewBag.SubCategory = db.AccountsHeadSubCategories.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
           
            return View(transaction);
        }


        public ActionResult GetAccountName(string q)
        {
            q = q.ToUpper();
            var users = db.AccountsSubHeads
                .Where(a => a.AccountsName.ToUpper().Contains(q))
                .Select(a => new { id = a.ID, name = a.AccountsName });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }




        // POST: /Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            return View(transaction);
        }

        // GET: /Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.category = db.AccountsHeadCategorys.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();

            }
           
            return View(transaction);
        }

        // POST: /Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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

       
        public ActionResult Report()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View("Report");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(string StartDate, string EndDate, int? CategoryID, int? SubCategoryID, int? AccountsName, int? BranchId)
        {
            ViewBag.ReportViewer = ReportView(StartDate, EndDate, CategoryID, SubCategoryID, AccountsName, BranchId);

            return View("_TransactionReportView");

        }
        public ReportViewer ReportView(string StartDate, string EndDate, int? CategoryID, int? SubCategoryID, int? AccountsName, int? BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from data in db.Transactions.AsEnumerable()
                        join lang in db.AccountsHeadCategorys.AsEnumerable() on data.CategoryID equals lang.ID
                        join sc in db.AccountsHeadSubCategories.AsEnumerable() on data.SubCategoryID equals sc.Id
                        join sub in db.AccountsSubHeads.AsEnumerable() on data.AccountsName equals sub.ID.ToString()
                        join bnh in db.Branchs.AsEnumerable() on data.BranchId equals bnh.BranchId
                        select new 
                        {
                            ID = data.ID,
                            CategoryID = lang.ID,
                            Category = lang.category,
                            SubCategoryID = sc.Id,
                            SubCategory = sc.SubCategoryName,
                            AccountsId= sub.ID,
                            AccountsName = sub.AccountsName,
                            Amount = data.Amount,
                            Date = data.Date,
                            Remarks = data.Remarks,
                            BranchId = data.BranchId,
                            CreditDebit = data.CreditDebitFlag.Equals("C") ? "Cr" : "Dr",
                            BranchName = bnh.BranchName

                        };






            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.Date >= StartDateRange);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.Date <= EndDateRange);
            }

            if (CategoryID != null && CategoryID != 0)
            {
                query = query.Where(s => s.CategoryID == CategoryID);
            }


            if (SubCategoryID != null && SubCategoryID != 0)
            {
                query = query.Where(s => s.SubCategoryID == SubCategoryID);
            }

            if (AccountsName != null && AccountsName != 0)
            {
                query = query.Where(s => s.AccountsId == AccountsName);
            }

            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s=>BranchId == 1 ? s.BranchId != 0 : s.BranchId == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new
                                    {
                                        s.Category,
                                        s.AccountsName,
                                        s.Amount,
                                        s.Date,
                                        s.Remarks,
                                        s.BranchName

                                    }).ToList();


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\TransactionReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSTransaction", reportDS));


            return reportViewer;

        }



        public ActionResult NetIncomeReport()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View("NetIncomeReport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NetIncomeReport(string StartDate, string EndDate, int? BranchId)
        {
            ViewBag.ReportViewer = NetIncomeReportView(StartDate, EndDate,BranchId);

            return View("_ReportViewer");

        }
        public ReportViewer NetIncomeReportView(string StartDate, string EndDate, int? BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from tr in db.Transactions.AsEnumerable().Where(t=>t.CategoryID == 2 || t.CategoryID ==4)
                        join ct in db.AccountsHeadCategorys.AsEnumerable() on tr.CategoryID equals ct.ID
                        join sct in db.AccountsHeadSubCategories.AsEnumerable() on tr.SubCategoryID equals sct.Id
                        join sub in db.AccountsSubHeads.AsEnumerable() on tr.AccountsName equals sub.ID.ToString()
                        select new
                        {
                            ct.category,
                            sct.SubCategoryName,
                            sub.AccountsName,
                            tr.Amount,
                            tr.Date,
                            tr.BranchId
                        };


            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.Date >= StartDateRange);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.Date <= EndDateRange);
            }
            

            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.BranchId == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new
                                    {
                                        Category = s.category,
                                        s.SubCategoryName,
                                        s.AccountsName,
                                        s.Amount,
                                        s.Date
                                    }).ToList();


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\NetIncomeReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSNetIncome", reportDS));


            return reportViewer;

        }


        public ActionResult BalanceSheet()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View("BalanceSheet");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BalanceSheet(string StartDate, string EndDate, int? BranchId)
        {
            ViewBag.ReportViewer = BalanceSheetView(StartDate, EndDate, BranchId);

            return View("_ReportViewer");

        }
        public ReportViewer BalanceSheetView(string StartDate, string EndDate, int? BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from tr in db.Transactions.AsEnumerable().Where(t => t.CategoryID == 1 || t.CategoryID == 3 || t.CategoryID == 5)
                        join ct in db.AccountsHeadCategorys.AsEnumerable() on tr.CategoryID equals ct.ID
                        join sct in db.AccountsHeadSubCategories.AsEnumerable() on tr.SubCategoryID equals sct.Id
                        join sub in db.AccountsSubHeads.AsEnumerable() on tr.AccountsName equals sub.ID.ToString()
                        select new
                        {
                            ct.category,
                            sct.SubCategoryName,
                            sub.AccountsName,
                            tr.Amount,
                            tr.Date,
                            tr.BranchId
                        };


            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.Date >= StartDateRange);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.Date <= EndDateRange);
            }


            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.BranchId == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new
                                    {
                                        Category = s.category,
                                        s.SubCategoryName,
                                        s.AccountsName,
                                        s.Amount,
                                        s.Date
                                    }).ToList();


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\BalanceSheetReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSBalanceSheet", reportDS));


            return reportViewer;

        }


        public ActionResult ProfitLossReport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfitLossReport(string StartDate, string EndDate, int? BranchId)
        {
            DataTable dtProfitLoss = new DataTable();
            DateTime beginDate = new DateTime();
            DateTime endDate = new DateTime();

            beginDate = Convert.ToDateTime(StartDate);
            endDate = Convert.ToDateTime(EndDate);

            dtProfitLoss.Columns.Add("Particulars", typeof(string));

            string monthColumn = string.Empty;

            while (beginDate <= endDate)
            {
                monthColumn = beginDate.ToString("MMM") + "'" + beginDate.Year.ToString();
                
                dtProfitLoss.Columns.Add(monthColumn, typeof(string));

                beginDate = beginDate.AddMonths(1);
            }

            dtProfitLoss.Columns.Add("Total", typeof(string));

            int incomeHeadId = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceCategoryId"]);
            int expenseHeadId = Convert.ToInt16(WebConfigurationManager.AppSettings["GeneralExpenseCatId"]);

            decimal monthlyIncome = 0;
            decimal monthlyExpense = 0;
            decimal monthlyNet = 0;

            decimal totalIncome = 0;
            decimal totalExpense = 0;
            decimal netTotal = 0;

            for (int i = 0; i < 3; i++)
            {
                DataRow dtr = dtProfitLoss.NewRow();
                beginDate = Convert.ToDateTime(StartDate);
                endDate = Convert.ToDateTime(EndDate);
                for (int j=0;j<=dtProfitLoss.Columns.Count;j++)
                {
                    if (j == 0)
                    {
                        if (i == 0 && j == 0)
                        {
                            dtr[j] = "Income";
                        }
                        else if (i == 1 && j == 0)
                        {
                            dtr[j] = "Expense";
                        }
                        else if (i == 2 && j == 0)
                        {
                            dtr[j] = "Net";
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            if (j <= dtProfitLoss.Columns.Count - 2)
                            {
                                monthlyIncome = db.Transactions.Where(w => w.CategoryID == incomeHeadId && w.Date.Month == beginDate.Month && w.Date.Year == beginDate.Year).Sum(s => s.Amount);
                                dtr[j] = Convert.ToString(monthlyIncome);
                                totalIncome = totalIncome + monthlyIncome;
                            }
                            if (j == dtProfitLoss.Columns.Count - 1)
                            {
                                dtr[j] = totalIncome;
                            }
                        }
                        if (i == 1)
                        {
                            if (j <= dtProfitLoss.Columns.Count - 2)
                            {
                                monthlyExpense = db.Transactions.Where(w => w.CategoryID == expenseHeadId && w.Date.Month == beginDate.Month && w.Date.Year == beginDate.Year).Sum(s => s.Amount);
                                dtr[j] = Convert.ToString(monthlyExpense);
                                totalExpense = totalExpense + monthlyExpense;
                            }
                            if (j == dtProfitLoss.Columns.Count - 1)
                            {
                                dtr[j] = totalExpense;
                            }
                        }
                        if (i == 2)
                        {
                            if (j <= dtProfitLoss.Columns.Count - 2)
                            {
                                monthlyNet = monthlyIncome - monthlyExpense;
                                dtr[j] = Convert.ToString(monthlyNet);
                            }
                            if (j == dtProfitLoss.Columns.Count - 1)
                            {
                                dtr[j] = totalIncome -  totalExpense;
                            }
                            
                        }

                            beginDate = beginDate.AddMonths(1);
                    }

                    
                }

                dtProfitLoss.Rows.Add(dtr);
            }

            return View("ProfitLossReportView",dtProfitLoss);

        }

      

    }
}
