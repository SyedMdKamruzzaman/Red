using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Web.Configuration;

namespace BlackSys.Controllers
{
    public class ServicePaymentController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: /ServicePayment/
        public ActionResult Index()
        {
            return View(db.ServicePayments.ToList());
        }

        // GET: /ServicePayment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePayment servicepayment = db.ServicePayments.Find(id);
            if (servicepayment == null)
            {
                return HttpNotFound();
            }
            return View(servicepayment);
        }

        // GET: /ServicePayment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ServicePayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ServicePayment servicepayment)
        {
            if (ModelState.IsValid)
            {
                db.ServicePayments.Add(servicepayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicepayment);
        }

        // GET: /ServicePayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePayment servicepayment = db.ServicePayments.Find(id);
            if (servicepayment == null)
            {
                return HttpNotFound();
            }
            return View(servicepayment);
        }

        // POST: /ServicePayment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServicePayment servicepayment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var advAmount = db.ServicePayments.Where(s => s.OrderID == servicepayment.OrderID).Select(s => s.AdvancePayment).Single();
                    Transaction trn = db.Transactions.Where(s => s.AppointmentID == servicepayment.OrderID).FirstOrDefault();


                    if (advAmount > 0)
                    {
                        DbEntityEntry<ServicePayment> entry = db.Entry(servicepayment);
                        entry.State = EntityState.Modified;
                        //entry.Property(s => s.ID).IsModified = false;
                        entry.Property(s => s.PaymentDate).IsModified = false;
                        entry.Property(s => s.OrderID).IsModified = false;
                        entry.Property(s => s.MemberCardID).IsModified = false;
                        entry.Property(s => s.CardTypeId).IsModified = false;
                        entry.Property(s => s.CardDiscount).IsModified = false;
                        entry.Property(s => s.CardDiscountAmount).IsModified = false;
                        entry.Property(s => s.TotalServiceAmount).IsModified = false;
                        entry.Property(s => s.BookingPayment).IsModified = false;
                        entry.Property(s => s.SpecialDiscount).IsModified = false;
                        entry.Property(s => s.TotalDiscount).IsModified = false;
                        entry.Property(s => s.PaymentTermsId).IsModified = false;
                        entry.Property(s => s.SlipNo).IsModified = false;
                        entry.Property(s => s.EntryBy).IsModified = false;
                        entry.Property(s => s.EntryDateTime).IsModified = false;
                        entry.Property(s => s.EntryByIp).IsModified = false;
                        entry.Property(s => s.PayableAmount).IsModified = false;


                        //entry.Property(s => s.PayableAmount).CurrentValue = entry.Property(s => s.PayableAmount).OriginalValue + servicepayment.Due;
                        entry.Property(s => s.PayAmount).CurrentValue = entry.Property(s => s.AdvancePayment).OriginalValue - servicepayment.PayableAmount;
                        entry.Property(s => s.Due).CurrentValue = entry.Property(s => s.PayableAmount).OriginalValue - servicepayment.AdvancePayment;
                        db.Transactions.Add(trn);
                        try
                        {
                            db.SaveChanges();
                            TempData["ResultMessage"] = "Payment Update Successfull.";
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
                        TempData["ResultMessage"] = "";
                        TempData["ResultType"] = "E";

                    }


                    return RedirectToAction("Index", "ServicePayment");
                }
                
            }

            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(servicepayment);
            //if (ModelState.IsValid)
            //{
            //    db.Entry(servicepayment).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(servicepayment);
        }

        // GET: /ServicePayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePayment servicepayment = db.ServicePayments.Find(id);
            if (servicepayment == null)
            {
                return HttpNotFound();
            }
            return View(servicepayment);
        }

        // POST: /ServicePayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePayment servicepayment = db.ServicePayments.Find(id);
            db.ServicePayments.Remove(servicepayment);
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


        public ActionResult GetSpecialDiscountById(string SpecialDiscount, string ServicePaymentOrder)
        {
            int happyHourId = Convert.ToInt16(WebConfigurationManager.AppSettings["HappyHourId"]);
            decimal amountConsider = Convert.ToInt16(WebConfigurationManager.AppSettings["ServiceAmountConsiderForSpecial"]);
            int SpecialDiscountId = Convert.ToInt16(SpecialDiscount);
            int ServicePaymentOrderId = Convert.ToInt16(ServicePaymentOrder);

            if (SpecialDiscountId == happyHourId)
            {
                int count = db.HappyHourModels.Count(w => w.IsActive == true);
                int spcecialDiscountConPer = 0;
                if (count > 0)
                {
                    var memberDiscount = (from ap in db.Appointments.AsEnumerable().Where(a => a.BookingID == ServicePaymentOrderId && a.TotalServicesAmount >= amountConsider)
                                         join mi in db.MembershipInfoes.AsEnumerable() on ap.MemberId equals mi.ID
                                         join ct in db.CardTypes.AsEnumerable() on mi.CardTypeId equals ct.Id
                                         select new
                                         {
                                             ct.DiscountPercentage
                                         }).SingleOrDefault();

                    spcecialDiscountConPer = Convert.ToInt16(WebConfigurationManager.AppSettings["SpcecialDiscountConPer"]);

                    decimal DiscountPercentage = 0;
                    if (memberDiscount != null)
                    {
                        DiscountPercentage = Convert.ToDecimal(memberDiscount.DiscountPercentage);
                    }

                    if (DiscountPercentage < spcecialDiscountConPer)
                    {
                        return Json(spcecialDiscountConPer, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(spcecialDiscountConPer, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.SpecialDiscounts.Count(w => w.Id == SpecialDiscountId && w.IsHappyHour == false);
                decimal specialDiscountValue = 0;
                if (count > 0)
                {
                    specialDiscountValue = db.SpecialDiscounts.Where(w => w.Id == SpecialDiscountId && w.IsHappyHour == false).Select(s => s.DiscountValue).SingleOrDefault();

                }
                return Json(specialDiscountValue, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
