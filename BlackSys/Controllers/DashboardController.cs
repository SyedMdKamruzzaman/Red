using BlackSys.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using BlackSys.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;
namespace BlackSys.Controllers
{
    public class DashboardController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ServiceWiseSales()
        {

            var dateLimit = DateTime.Today.AddDays(-7);
            var piechart = db.ServicePayments.Where(a => a.PaymentDate >= dateLimit).GroupBy(d => d.PaymentDate)
                             .Select(
                                 g => new
                                 {
                                     Key = g.Key,
                                     Value = g.Sum(s => s.AdvancePayment),
                                     PaymentDate = g.FirstOrDefault().PaymentDate,
                                     Amount = g.FirstOrDefault().AdvancePayment
                                 });

            return Json(piechart, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BranchWiseSales()
        {
            var dateLimit = DateTime.Today.AddDays(-7);
            var piechart = (from data in db.ServicePayments.AsEnumerable()
                            join lang in db.Appointments.AsEnumerable()
                            on data.OrderID equals lang.BookingID
                            join bnh in db.Branchs.AsEnumerable()
                            on lang.BranchId equals bnh.BranchId
                            where (data.PaymentDate >= dateLimit & lang.BranchId == bnh.BranchId)
                            group new { data, bnh } by new { data.AdvancePayment, bnh.BranchName }
                                into grp
                                select new
                                {
                                    Count = grp.Count(),
                                    Value = grp.Sum(s => s.data.AdvancePayment),
                                    BranchName = grp.Key.BranchName,
                                    Amount = grp.FirstOrDefault().data.AdvancePayment
                                });
            return Json(piechart, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public ActionResult IncomeVsExpenses()
        {
            var dateLimit = DateTime.Today.AddDays(-30);
            var piechart = (from data in db.Transactions.AsEnumerable()
                            join lang in db.AccountsHeadCategorys.AsEnumerable()
                            on data.CategoryID equals lang.ID
                            where (data.Date >= dateLimit & data.CategoryID == lang.ID)
                            group new { data, lang } by new { data.Amount, lang.category }
                                into grp
                                select new
                                {
                                    Count = grp.Count(),
                                    Value = grp.Sum(s => s.data.Amount),
                                    category = grp.FirstOrDefault().lang.category,
                                    Amount = grp.FirstOrDefault().data.Amount
                                });
            return Json(piechart, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }
	}
}