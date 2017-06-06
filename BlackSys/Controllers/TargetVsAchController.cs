using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;

namespace BlackSys.Controllers
{
    public class TargetVsAchController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: /TargetVsAch/
        public ActionResult Index()
        {

            DateTime startTime = DateTime.Now;
            var endtime = db.TargetLevels2.AsEnumerable().Select(x => x.ExpireDate).FirstOrDefault();
            TimeSpan span = endtime.Subtract(startTime);

            var findrelation = (from or in db.OrderDetails.AsEnumerable() join sp in db.ServicePayments.AsEnumerable()
                         on or.BookingID equals sp.OrderID
                         select new
                         {
                             or.Beautician,
                             sp.AdvancePayment
                         });
         //var getachamt =  

            var tarvcachlist = from tva in db.TargetVsAchs.AsEnumerable()
                               join tr1 in db.TargetLevels2.AsEnumerable() on new { tva.TargetDate, tva.ExpireDate, tva.AssignTo } equals new { tr1.TargetDate, tr1.ExpireDate, tr1.AssignTo }
                               join bnh in db.Branchs.AsEnumerable() on tva.BranchId equals bnh.BranchId
                                join emp in db.Employees.AsEnumerable() on tva.AssignTo equals emp.EmpID

                                select new TargetVsAch()
                                {
                                    BranchName = bnh.BranchName,
                                    SPName = emp.EmpName,
                                    TargetAmount = tr1.TargetAmount,
                                    TargetMonthInText = tva.TargetDate.ToString("MMMM"),
                                    AchAmount = findrelation.Where(x=>x.Beautician == tr1.AssignTo).Sum(s => s.AdvancePayment),
                                    GapAmount = (tr1.TargetAmount - (findrelation.Where(x => x.Beautician == tr1.AssignTo).Sum(s => s.AdvancePayment))),
                                    DaysRemain = span.Days.ToString()
                                };

            return View(tarvcachlist.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTargetVsAchByMonth(string TargetStartDate, string TargetEndDate)
        {
            DateTime startTime = DateTime.Now;
            var endtime = db.TargetLevels2.AsEnumerable().Select(x => x.ExpireDate).FirstOrDefault();
            TimeSpan span = endtime.Subtract(startTime);

            string TgtStartDate = string.Empty;
            string TgtEndDate = string.Empty;

           

            if (!TargetStartDate.Equals(string.Empty) && TargetStartDate != null)
            {
                TgtStartDate = Convert.ToDateTime(TargetStartDate).ToString("dd-MMM-yyyy");
            }
            else
            {
                TgtStartDate = DateTime.Now.ToString("dd-MMM-yyyy"); 
            }

            if (!TargetEndDate.Equals(string.Empty) && TargetEndDate != null)
            {
                TgtEndDate = Convert.ToDateTime(TargetEndDate).ToString("dd-MMM-yyyy");
            }
            else
            {
                TgtEndDate = DateTime.Now.ToString("dd-MMM-yyyy");
            }

            DateTime StartDate = new DateTime();
            DateTime EndDate = new DateTime();

            StartDate = Convert.ToDateTime(TgtStartDate);
            EndDate = Convert.ToDateTime(TgtEndDate);

            var findrelation = (from or in db.OrderDetails.AsEnumerable()
                                join sp in db.ServicePayments.AsEnumerable()
on or.BookingID equals sp.OrderID
                                select new
                                {
                                    or.Beautician,
                                    sp.AdvancePayment
                                });
            //var getachamt =  

            var tarvcachlist = (from tva in db.TargetVsAchs.AsEnumerable().Where(s => s.TargetDate == StartDate && s.ExpireDate == EndDate)
                                join tr1 in db.TargetLevels2.AsEnumerable() on new { tva.TargetDate, tva.ExpireDate, tva.AssignTo } equals new { tr1.TargetDate, tr1.ExpireDate, tr1.AssignTo }
                               join bnh in db.Branchs.AsEnumerable() on tva.BranchId equals bnh.BranchId
                               join emp in db.Employees.AsEnumerable() on tva.AssignTo equals emp.EmpID

                               select new TargetVsAch()
                               {
                                   BranchName = bnh.BranchName,
                                   SPName = emp.EmpName,
                                   TargetAmount = tr1.TargetAmount,
                                   TargetMonthInText = tva.TargetDate.ToString("MMMM"),
                                   AchAmount = findrelation.Where(x => x.Beautician == tr1.AssignTo).Sum(s => s.AdvancePayment),
                                   GapAmount = (tr1.TargetAmount - (findrelation.Where(x => x.Beautician == tr1.AssignTo).Sum(s => s.AdvancePayment))),
                                   DaysRemain = span.Days.ToString()
                               }) ;

            return View("Index",tarvcachlist.ToList());
        }

    }
}