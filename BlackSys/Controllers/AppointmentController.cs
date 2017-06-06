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
    public class AppointmentController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        private CommonRepository commonRepository = new CommonRepository();
        // GET: /Appointment/
        public ActionResult Index()
        {
            return View(db.Appointments.Where(w=>w.IsCompleted==false && w.IsCanceled==false).ToList());
        }

        // GET: /Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: /Appointment/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            var branchId = CommonRepository.GetBranchId(userId);
            int[] branches = commonRepository.GetComSepAllBranchId(userId);

            AppointmentViewModel appointViewModel = new AppointmentViewModel();
           
            List<OrderDetails> orderDetailsList = new List<OrderDetails> { new OrderDetails { ID = 0, BookingID = 0, JobID = 0, ServiceID = 0, ServiceRate = 0, ServiceDate = DateTime.Today, Beautician = 0 } };
            appointViewModel.OrderDetailsList = orderDetailsList;
           
           
           appointViewModel.AppointmentList = (from ap in db.Appointments.Where(w => w.IsCompleted == false && w.IsCanceled == false && (branchId == 1 ? w.BranchId != 0 : w.BranchId == branchId)).AsEnumerable()
                                                join mi in db.MembershipInfoes.AsEnumerable() on ap.MemberId equals mi.ID
                                                join br in db.Branchs.AsEnumerable() on ap.BranchId equals br.BranchId
                                                select new AppointmentListViewModel()
                                                {                                                    
                                                    MemberName = mi.MemberName,
                                                    // MemEmail = mi.MemEmail,
                                                    MemMobileNo = mi.MemMobileNo,
                                                    TotalServicesAmount = ap.TotalServicesAmount,
                                                    AdvancePayment =Convert.ToDecimal(ap.AdvancePayment == null?0 : ap.AdvancePayment),
                                                    PaidAmount = db.ServicePayments.Where(w => w.OrderID == ap.BookingID).Select(x => x.AdvancePayment).SingleOrDefault(),
                                                    ServiceDate = ap.ServiceDate,
                                                    TotalDiscountAmount = db.ServicePayments.Where(w => w.OrderID == ap.BookingID).Select(x => x.TotalDiscount).SingleOrDefault(),
                                                    TotalDueAmount = db.ServicePayments.Where(w => w.OrderID == ap.BookingID).Select(x => x.Due).SingleOrDefault(),
                                                    //BranchName = br.BranchName,
                                                    BookingID = ap.BookingID,
                                                    BranchId = br.BranchId
                                                    
                                                }).OrderByDescending(o=>o.BookingID).ToList();
            
            return View(appointViewModel);

        }

        [HttpGet]
        [AjaxChildActionOnly]
        public ActionResult Create(int? id)
        {
            //var userId = User.Identity.GetUserId();
            //var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            //ViewBag.Braches = db.Branchs.Where(x => branchId==1? x.BranchId != 0 : x.BranchId == branchId).ToList();

            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel();
            orderDetailsViewModel.OrderDetailsList = (from od in db.OrderDetails
                                                      join sd in db.Services on od.ServiceID equals sd.ID
                                                      where od.BookingID == id
                                                      select new
                                                      {
                                                          od.ID,
                                                          od.BookingID,
                                                          od.JobID,
                                                          od.ServiceID,
                                                          sd.ServiceName,
                                                          od.ServiceRate,
                                                          od.ServiceDate,
                                                          od.Beautician,
                                                          od.SpecialSaleBeauticinId
                                                      }).AsEnumerable().Select(ov => new OrderViewModel
                                                      {
                                                          ID = ov.ID,
                                                          BookingID = ov.BookingID,
                                                          JobID = ov.JobID,
                                                          ServiceID = ov.ServiceID,
                                                          ServiceName = ov.ServiceName,
                                                          ServiceRate = ov.ServiceRate,
                                                          ServiceDate = ov.ServiceDate,
                                                          Beautician = ov.Beautician,
                                                          SpecialSaleBeauticinId=ov.SpecialSaleBeauticinId                                                          

                                                      }).ToList();



            orderDetailsViewModel.BeauticianList = (from e in db.Employees
                                                    select new
                                                    {
                                                        e.EmpID,
                                                        e.EmpName,
                                                        e.DeptID
                                                    }).AsEnumerable().Where(s => s.DeptID == 7).
                                                    Select(b => new BeauticianViewModel
                                                    {
                                                        ID = b.EmpID,
                                                        Name = b.EmpName,
                                                        DeptID = b.DeptID
                                                    }).ToList();

            var jsonResult = Json(orderDetailsViewModel, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        [AjaxChildActionOnly]
        public ActionResult Cancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appointmentToUpdate = from ap in db.Appointments where (ap.BookingID == id) select ap;
            if (appointmentToUpdate.Count() != 0)
            {
                var dbAppointments = appointmentToUpdate.First();
                var servicePaymentCount = db.ServicePayments.Count(s => s.OrderID == id);

                if (servicePaymentCount > 0 || dbAppointments.AdvancePayment > 0)
                {
                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = "Order Cannot be Canceled. Please contact Accounts."
                    };
                }
                else
                {                   
                    dbAppointments.IsCanceled = true;
                    dbAppointments.CancelledDate = DateTime.Now;
                    db.SaveChanges();
                }

                //Transaction trn = new Transaction();
                //trn.CategoryID = 2;
                //trn.SubCategoryID = 8;
                //trn.AccountsName = "1033";
                //trn.CreditDebitFlag = "D";
                //trn.Amount = dbAppointments.TotalServicesAmount;
                //trn.Date = DateTime.Now;
                //trn.Remarks = "Sales Return";
                //trn.AppointmentID = Convert.ToInt32(dbAppointments.BookingID);
                //trn.BranchId = dbAppointments.BranchId;
                //db.Transactions.Add(trn);
                //db.SaveChanges();
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = "Order Canceled Successfully"
            };


        }


        [HttpGet]
        [AjaxChildActionOnly]
        public ActionResult Done(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var appointmentToUpdate = from ap in db.Appointments where (ap.BookingID == id) select ap;
            if (appointmentToUpdate.Count() != 0)
            {
                var dbAppointments = appointmentToUpdate.First();
                var servicePaymentCount = db.ServicePayments.Count(s => s.OrderID == id);

                if (servicePaymentCount > 0)
                {
                    var dbServicePayment = (from sp in db.ServicePayments where (sp.OrderID == id) select sp).First();
                    if (dbServicePayment.PayableAmount == dbServicePayment.AdvancePayment)
                    {
                        dbAppointments.IsCompleted = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        return new JsonResult()
                        {
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                            Data = "Order Cannot be Completed Due to incomplete payment."
                        };

                    }

                }
                else
                {
                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = "Order Cannot be Completed Due to incomplete payment."
                    };
                }

            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = "Order Completed Successfully"
            };


        }
        // POST: /Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "BookingID,AppointmentDate,FullName,Mobile,Email,Services,ServicesID,ServiceDate,Venue,BranchName,Franchise,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentViewModel AppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                
                db.Appointments.Add(AppointmentViewModel.Appointments);
                db.SaveChanges();

                string[] strServices = AppointmentViewModel.Appointments.ServicesID.Split(',');

                for (int i = 0; i < strServices.Length; i++)
                {

                    OrderDetails ordDetails = new OrderDetails();
                    ordDetails.BookingID = AppointmentViewModel.Appointments.BookingID;
                    ordDetails.JobID = i + 1;
                    ordDetails.ServiceID = Convert.ToInt64(strServices[i]);
                    ordDetails.ServiceRate = GetServiceRate(Convert.ToInt16(strServices[i]));
                    ordDetails.ServiceDate = AppointmentViewModel.Appointments.ServiceDate;

                    db.OrderDetails.Add(ordDetails);
                    db.SaveChanges();
                }

                if (AppointmentViewModel.Appointments.AdvancePayment > 0)
                {
                    Transaction trn = new Transaction();
                    trn.CategoryID =Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceCategoryId"]);
                    trn.SubCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceSubCategoryId"]);
                    trn.AccountsName = Convert.ToString(WebConfigurationManager.AppSettings["IncomeAdvanceAccountId"]);
                    trn.CreditDebitFlag = "C";
                    trn.Amount = Convert.ToDecimal(AppointmentViewModel.Appointments.AdvancePayment==null?0: AppointmentViewModel.Appointments.AdvancePayment);
                    trn.Date =Convert.ToDateTime( AppointmentViewModel.Appointments.AppointmentDate.Equals("")?DateTime.Today: AppointmentViewModel.Appointments.AppointmentDate);
                    trn.Remarks = "Slip No:" + " " + AppointmentViewModel.Appointments.BookingID;
                    trn.AppointmentID = Convert.ToInt32(AppointmentViewModel.Appointments.BookingID);
                    trn.BranchId = AppointmentViewModel.Appointments.BranchId;
                    db.Transactions.Add(trn);
                    db.SaveChanges();
                }

                TempData["ResultMessage"] = "Appointment Created Successfully.";
                TempData["ResultType"] = "S";

                return RedirectToAction("Create", AppointmentViewModel);
            }
            var userId = User.Identity.GetUserId();
            var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();            
            ViewBag.Braches = db.Branchs.Where(x => x.BranchId == branchId);
            int[] branches = commonRepository.GetComSepAllBranchId(userId);
            AppointmentViewModel.AppointmentList = (from ap in db.Appointments.Where(w => w.IsCompleted == false && w.IsCanceled == false && w.BranchId == 1 ? w.BranchId != 0 : w.BranchId == branchId).AsEnumerable()
                                                join mi in db.MembershipInfoes.AsEnumerable() on ap.MemberId equals mi.ID
                                                join br in db.Branchs.AsEnumerable() on ap.BranchId equals br.BranchId
                                                select new AppointmentListViewModel()
                                                {
                                                    MemberName = mi.MemberName,
                                                    // MemEmail = mi.MemEmail,
                                                    MemMobileNo = mi.MemMobileNo,
                                                    TotalServicesAmount = ap.TotalServicesAmount,
                                                    AdvancePayment = Convert.ToDecimal(ap.AdvancePayment==null?0: ap.AdvancePayment),
                                                    PaidAmount = db.ServicePayments.Where(w => w.OrderID == ap.BookingID).Select(x => x.AdvancePayment).SingleOrDefault(),
                                                    ServiceDate = ap.ServiceDate,
                                                    TotalDiscountAmount = db.ServicePayments.Where(w => w.OrderID == ap.BookingID).Select(x => x.TotalDiscount).SingleOrDefault(),
                                                    TotalDueAmount = db.ServicePayments.Where(w => w.OrderID == ap.BookingID).Select(x => x.Due).SingleOrDefault(),
                                                    //BranchName = br.BranchName,
                                                    BookingID = ap.BookingID
                                                }).OrderByDescending(o => o.BookingID).ToList();

            return View(AppointmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBeautician(string ids, string orderIds, string jobIds, string serviceIds, string serviceRates, string serviceDates, string beauticians, string SpecialSaleBeauticians)
        {
            int[] updatedByID = Array.ConvertAll(ids.Split(','), int.Parse);
            int[] orderIdArr = Array.ConvertAll(orderIds.Split(','), int.Parse);
            string[] jobIdArr = jobIds.Split(',');
            string[] serviceIdArr = serviceIds.Split(',');
            string[] serviceRateArr = serviceRates.Split(',');
            string[] serviceDateArr = serviceDates.Split(',');
            string[] beauticianToUpdate = beauticians.Split(',');
            string[] specialSaleBeauticianToUpdate = SpecialSaleBeauticians.Split(',');



            using (BlackSysEntities dbEntities = new BlackSysEntities())
            {

                var bookingId = orderIdArr[0];
                var orderDetailsCount = dbEntities.OrderDetails.Where(od => od.BookingID == bookingId).Count();
                decimal amount = 0;
                decimal totalAmount = 0;
                for (int i = 0; i < updatedByID.Length; i++)
                {
                    if (i < orderDetailsCount)
                    {
                        var idToSearch = updatedByID[i];

                        var result = from u in dbEntities.OrderDetails where (u.ID == idToSearch) select u;
                        if (result.Count() != 0)
                        {
                            var dbOrderDetails = result.First();
                            dbOrderDetails.ServiceID = Convert.ToInt64(serviceIdArr[i]);
                            amount = Convert.ToDecimal(serviceRateArr[i]);
                            totalAmount += amount;
                            dbOrderDetails.ServiceRate = amount;
                            dbOrderDetails.ServiceDate = Convert.ToDateTime(serviceDateArr[i]);
                            dbOrderDetails.Beautician = Convert.ToInt64(beauticianToUpdate[i]);
                            dbOrderDetails.SpecialSaleBeauticinId = Convert.ToInt64(specialSaleBeauticianToUpdate[i].Equals(null)?"0": specialSaleBeauticianToUpdate[i]);
                            db.SaveChanges();
                        }
                      
                    }
                    else
                    {
                        OrderDetails ordDetails = new OrderDetails();
                        ordDetails.BookingID = orderIdArr[0];
                        ordDetails.JobID =Convert.ToInt64(jobIdArr[i]);
                        ordDetails.ServiceID = Convert.ToInt64(serviceIdArr[i]);
                        amount = Convert.ToDecimal(serviceRateArr[i]);
                        totalAmount += amount;
                        ordDetails.ServiceRate = amount;
                        ordDetails.ServiceDate = Convert.ToDateTime(serviceDateArr[i]);
                        ordDetails.Beautician = Convert.ToInt64(beauticianToUpdate[i]);
                        ordDetails.SpecialSaleBeauticinId = Convert.ToInt64(specialSaleBeauticianToUpdate[i].Equals(null) ? "0" : specialSaleBeauticianToUpdate[i]);
                        dbEntities.OrderDetails.Add(ordDetails);
                        dbEntities.SaveChanges();

 
                    }
                }

                var appointmentToUpdate = from ap in dbEntities.Appointments where (ap.BookingID == bookingId) select ap;
                 if (appointmentToUpdate.Count() != 0)
                 {
                     var dbAppointments = appointmentToUpdate.First();
                     dbAppointments.TotalServicesAmount = totalAmount;
                     dbEntities.SaveChanges();
                 }


                var servicePaymentToUpdate = from sp in dbEntities.ServicePayments where (sp.OrderID == bookingId) select sp;
                if (servicePaymentToUpdate.Count() != 0)
                {
                    var dbServicePayement = servicePaymentToUpdate.First();
                    dbServicePayement.TotalServiceAmount = totalAmount;
                    dbServicePayement.PayableAmount = totalAmount - dbServicePayement.BookingPayment-dbServicePayement.TotalDiscount;
                    dbServicePayement.Due = dbServicePayement.PayableAmount - dbServicePayement.AdvancePayment;
                    dbEntities.SaveChanges();
                }

            }


            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = "Order updated successfully."
            };
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateServicePayment(ServicePayment servicepayment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (BlackSysEntities dbEntities = new BlackSysEntities())
                    {
                        var result = from u in dbEntities.ServicePayments where (u.OrderID == servicepayment.OrderID) select u;
                        if (result.Count() != 0)
                        {
                            var dbServicePayments = result.First();

                            dbServicePayments.SpecialDiscount = servicepayment.SpecialDiscount;
                            dbServicePayments.TotalDiscount = servicepayment.TotalDiscount;
                            dbServicePayments.PayableAmount = servicepayment.PayableAmount;
                            dbServicePayments.AdvancePayment = servicepayment.AdvancePayment + servicepayment.PayAmount; //already paid
                            dbServicePayments.PayAmount = servicepayment.PayAmount;
                            dbServicePayments.Due = servicepayment.Due;
                            dbServicePayments.PaymentTermsId = servicepayment.PaymentTermsId;
                            dbServicePayments.CreditCardNo = (servicepayment.CreditCardNo == null ? 0 : servicepayment.CreditCardNo);
                            dbServicePayments.SlipNo = servicepayment.SlipNo;
                            //dbEntities.ServicePayments.Add(servicepayment);
                            dbEntities.SaveChanges();

                            Transaction trn = new Transaction();
                            trn.CategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceCategoryId"]);
                            trn.SubCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceSubCategoryId"]);
                            trn.AccountsName = Convert.ToString(WebConfigurationManager.AppSettings["IncomeAdvanceAccountId"]);
                            trn.CreditDebitFlag = "C";
                            trn.Amount = Convert.ToDecimal(servicepayment.PayAmount == null ? 0 : servicepayment.PayAmount);
                            trn.Date = Convert.ToDateTime(servicepayment.PaymentDate.Equals("") ? DateTime.Today : servicepayment.PaymentDate);
                            trn.Remarks = "Slip No:" + " " + servicepayment.SlipNo;
                            trn.AppointmentID = Convert.ToInt32(servicepayment.OrderID);
                            trn.BranchId = db.Appointments.Where(x => x.BookingID == servicepayment.OrderID).Select(x => x.BranchId).FirstOrDefault();
                            db.Transactions.Add(trn);
                            db.SaveChanges();


                            TempData["ResultMessage"] = "Payment Created Successfully.";
                            TempData["ResultType"] = "S";

                        }

                        else
                        {
                            servicepayment.AdvancePayment = servicepayment.PayAmount;
                            dbEntities.ServicePayments.Add(servicepayment);

                        }

                    }
                }
                catch (Exception ex)
                { }
            }
            AppointmentViewModel appointViewModel = appviewmodel(servicepayment);
            return View("Create",appointViewModel);
        }


        //public ActionResult CreateServicePayment(ServicePayment servicepayment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (BlackSysEntities dbEntities = new BlackSysEntities())
        //        {

        //            var paymentId = servicepayment.ID;
        //            var result = from u in dbEntities.ServicePayments where (u.OrderID == servicepayment.OrderID) select u;
        //            if (result.Count() != 0)
        //            {
        //                var dbServicePayments = result.First();

        //                dbServicePayments.SpecialDiscount = servicepayment.SpecialDiscount;
        //                dbServicePayments.TotalDiscount = servicepayment.TotalDiscount;
        //                dbServicePayments.PayableAmount = servicepayment.PayableAmount;
        //                dbServicePayments.AdvancePayment = servicepayment.AdvancePayment + servicepayment.PayAmount; //already paid
        //                dbServicePayments.PayAmount = servicepayment.PayAmount;
        //                dbServicePayments.Due = servicepayment.Due;
        //                dbServicePayments.PaymentTerms = servicepayment.PaymentTerms;
        //                dbServicePayments.CreditCardNo = servicepayment.CreditCardNo;
        //                dbServicePayments.SlipNo = servicepayment.SlipNo;
        //            }

        //            else
        //            {
        //                servicepayment.AdvancePayment = servicepayment.PayAmount;
        //                dbEntities.ServicePayments.Add(servicepayment);

        //            }

        //            RePayment repayment = new RePayment();
        //            repayment.OrderID = servicepayment.OrderID;
        //            repayment.PayAmount = servicepayment.PayAmount;
        //            repayment.PaymentDate = servicepayment.PaymentDate;
        //            repayment.PaymentTerms = servicepayment.PaymentTerms;                   
        //            repayment.SlipNo = servicepayment.SlipNo;

        //            int AccountHeadCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceCategoryId"]);
        //            int AccountSubHeadCategoryID = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceSubCategoryId"]);
        //            int AccountID = Convert.ToInt16(WebConfigurationManager.AppSettings["IncomeAdvanceAccountId"]);

        //            Transaction trn = new Transaction();
        //            trn.CategoryID = AccountHeadCategoryID;
        //            trn.SubCategoryID = AccountSubHeadCategoryID;
        //            trn.AccountsName = db.AccountsSubHeads.Where(x=>x.ID==AccountID).Select(x=>x.AccountsName).FirstOrDefault();
        //            trn.CreditDebitFlag = "C";
        //            trn.Amount = db.ServicePayments.Where(x=>x.BookingPayment != 0).Where(x=>x.OrderID == servicepayment.OrderID).Select(x=>x.BookingPayment).FirstOrDefault() + servicepayment.PayAmount;                   
        //            trn.Date = servicepayment.PaymentDate;
        //            trn.Remarks = "Slip No:" + " " + servicepayment.SlipNo;
        //            trn.AppointmentID = Convert.ToInt32(servicepayment.OrderID);
        //            trn.BranchId = db.Appointments.Where(x => x.BookingID == servicepayment.OrderID).Select(x => x.BranchId).SingleOrDefault();
        //           dbEntities.Transactions.Add(trn);
        //            try
        //            {

        //                dbEntities.SaveChanges();
        //                TempData["ResultMessage"] = "Payment Completed Successfully";
        //                TempData["ResultType"] = "S";                    
        //            }
        //            catch
        //            {
        //                TempData["ResultMessage"] = "Sorry, Please Check if any information missing !";
        //                TempData["ResultType"] = "E";
        //            }

        //        }

        //    }

        //    AppointmentViewModel appointViewModel = appviewmodel(servicepayment);

        //    return View("Create",appointViewModel);

        //}
        // GET: /Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: /Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: /Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: /Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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


        public ActionResult GetMember(string q)
        {
            q = q.ToUpper();
            var users = db.MembershipInfoes
                .Where(a => a.MemberName.ToUpper().Contains(q) || a.MemMobileNo.Contains(q))
                .Select(a => new { id = a.ID, name = a.MemberName + " - " + a.MemMobileNo});

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAuthors(string q)
        {
            q = q.ToUpper();
            var users =from ser in db.Services.Where(a => a.ServiceName.ToUpper().Contains(q) || a.Price.ToString().Contains(q))
                       join cat in db.Categorys on ser.ServiceCategory equals cat.categoryid
                       select new
                       {
                           id = ser.ID,
                           name =cat.category + " - " + ser.ServiceName + " - " + ser.Price, price = ser.Price 
                       };

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        //        orderDetailsViewModel.BeauticianList = (from e in db.Employees
        //                                                select new
        //                                                    {
        //                                                        e.EmpID,
        //                                                        e.EmpName,
        //                                                        e.DeptID
        //    }).AsEnumerable().Where(s => s.DeptID == 7).
        //                                                    Select(b => new BeauticianViewModel
        //                                                    {
        //                                                        ID = b.EmpID,
        //                                                        Name = b.EmpName,
        //                                                        DeptID = b.DeptID
        //}).ToList();

        //var jsonResult = Json(orderDetailsViewModel, JsonRequestBehavior.AllowGet);

        public ActionResult GetBeautician(string q)
        {
            q = q.ToUpper();
            var users = db.Employees
                .Where(a => a.EmpName.ToUpper().Contains(q) && a.DeptID == 7)
                .Select(a => new { id = a.EmpID, name = a.EmpName});

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


public ActionResult GetTotalPrice(string id)
        {
            if(id==null || id.ToUpper()=="UNDEFINED")
            {
                TempData["ResultMessage"] = "Please Enter In Appointment Details to Create";
                TempData["ResultType"] = "E";
                return Json("", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }

            int[] ids = Array.ConvertAll(id.Split(','), int.Parse);
            var total_price = db.Services.Where(a => ids.Contains(a.ID)).Sum(p => p.Price);

            return Json(total_price, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalPriceAndBookingPayment(int id)
        {
            ServicePayment servicePayment = new ServicePayment();

            //var servicePaymentCount = db.ServicePayments.Where(p => p.OrderID == id).Count();
            var result = from u in db.ServicePayments where (u.OrderID == id) select u;
            if (result.Count() != 0)
            {
                servicePayment = result.First();
                if (servicePayment.Due == 0)
                {
                    TempData["ResultMessage"] = "Payment Completed on this Order on  " + servicePayment.OrderID.ToString();
                    TempData["ResultType"] = "S";

                    return Json("Payment Completed on this Order.", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
                    //return new HttpStatusCodeResult(400, "Payment Completed on this Order."); 
                }
                servicePayment = db.ServicePayments.Where(p => p.OrderID == id).FirstOrDefault();
            }
            else
            {
                //var appointment = db.Appointments.Where(w => w.BookingID == id).Select(a => new { a.AdvancePayment, a.TotalServicesAmount }).FirstOrDefault();

                servicePayment =(from ap in db.Appointments.AsEnumerable().Where(w => w.BookingID == id)
                                   join mi in db.MembershipInfoes.AsEnumerable()   on ap.MemberId equals mi.ID into apmi                                   
                                   from subpet in apmi.DefaultIfEmpty()                                   
                                   select new ServicePayment
                                   {
                                       BookingPayment= Convert.ToDecimal(ap.AdvancePayment==null?0: ap.AdvancePayment),
                                       TotalServiceAmount =  ap.TotalServicesAmount,
                                       MemberCardID =  (subpet.CardId == null ? "" : subpet.CardId.ToString()),
                                       CardType = db.CardTypes.Where(x => subpet.CardTypeId != null && x.Id == subpet.CardTypeId).Select(s=>s.Type).SingleOrDefault(),
                                       CardDiscount =  (subpet.Discount == null ? 0 : Convert.ToDecimal(subpet.Discount)),
                                       CardDiscountAmount = (subpet.Discount == null ? 0 : Convert.ToDecimal((ap.TotalServicesAmount*subpet.Discount)/100)),
                                       TotalDiscount = (subpet.Discount == null ? 0 : Convert.ToDecimal((ap.TotalServicesAmount * subpet.Discount) / 100))                                       
                                   }).FirstOrDefault();

                servicePayment.PayableAmount = servicePayment.TotalServiceAmount - servicePayment.BookingPayment - servicePayment.TotalDiscount;
            }


            return Json(servicePayment, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceRateById(int id)
        {
            var data = db.Services.Where(a => a.ID == id).Select(p => p.Price).Single();

            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }



        private decimal GetServiceRate(int id)
        {
            var price = db.Services.Where(a => a.ID == id).Select(p => p.Price).Single();
            return Convert.ToDecimal(price);
        }


        public ActionResult GetMemberCardInfo(string q)
        {
            q = q.ToUpper();
            var users = db.MembershipInfoes
                .Where(a => a.CardId.ToUpper().Contains(q))
                .Select(a => new { id = a.ID, name = a.CardId });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCardTypenDiscount(int id)
        {
            var data = (from mi in db.MembershipInfoes.AsEnumerable().Where(w => w.ID == id)
                        join ct in db.CardTypes.AsEnumerable() on mi.CardTypeId equals ct.Id
                        select new
                        { ct.Type, mi.Discount }
                       ).ToList();            

            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(int OrderId)
        {
            ViewBag.ReportViewer = MakePaymentReport(OrderId);
           
            return View("_ReportViewer");

            //AppointmentViewModel appointViewModel =new AppointmentViewModel();
            //return View(appointViewModel);
        }

        private ReportViewer MakePaymentReport(int OrderId)
        {
             ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            

            var paymentReportViewModelList = (from od in db.OrderDetails.AsEnumerable().Where(a => a.BookingID == OrderId)
                                                 join ap in db.Appointments.AsEnumerable() on od.BookingID equals ap.BookingID
                                                 join mi in db.MembershipInfoes.AsEnumerable() on ap.MemberId equals mi.ID
                                              //join ct in db.CardTypes.AsEnumerable() on mi.CardTypeId equals ct.Id
                                              join sp in db.ServicePayments.AsEnumerable() on od.BookingID equals sp.OrderID
                                                 join br in db.Branchs.AsEnumerable() on ap.BranchId equals br.BranchId
                                                 join emp in db.Employees.AsEnumerable() on od.Beautician equals emp.EmpID
                                                 join ser in db.Services.AsEnumerable() on od.ServiceID equals ser.ID
                                                 select new PaymentReportViewModel()
                                                 {
                                                                                                      
                                                    BookingID=od.BookingID,
                                                    JobID=od.JobID,
                                                    ServiceID=od.ServiceID,
                                                    ServiceRate=od.ServiceRate,
                                                    ServiceName = ser.ServiceName,
                                                    ServiceDate =od.ServiceDate,
                                                    Beautician=od.Beautician,
                                                    AppointmentDate=Convert.ToDateTime(ap.AppointmentDate.Equals("")?DateTime.Today:ap.AppointmentDate),
                                                    Venue =ap.Venue,
                                                    EntryDateTime = Convert.ToDateTime(ap.EntryDateTime),
                                                    //CardType = ct.Type,
                                                    MemberName = mi.MemberName,
                                                    MemMobileNo =mi.MemMobileNo,
                                                    MemEmail =mi.MemEmail,
                                                    PaymentDate =sp.PaymentDate,
                                                    TotalServiceAmount =sp.TotalServiceAmount,
                                                    BookingPayment =sp.BookingPayment,
                                                    Due =sp.Due,
                                                    SlipNo = sp.SlipNo,
                                                    TotalPayableAmount =sp.PayAmount,
                                                    PaymentTerms =sp.PaymentTerms,
                                                    BranchName =br.BranchName,
                                                    EmpName =emp.EmpName,
                                                    BranchAddress =br.BranchAddress,
                                                    EntryBy =ap.EntryBy,
                                                    AdvancePayment = Convert.ToDecimal(ap.AdvancePayment == null ? 0 : ap.AdvancePayment),
                                                   PayAmount =sp.AdvancePayment + Convert.ToDecimal(ap.AdvancePayment==null? 0: ap.AdvancePayment)



                                                 }).ToList();

            
            

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\ReportPayment.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSPayment", paymentReportViewModelList));
           

            return reportViewer;
        }

        private AppointmentViewModel appviewmodel(ServicePayment servicepayment)
        {
            AppointmentViewModel appointViewModel = new AppointmentViewModel();
            

            var userId = User.Identity.GetUserId();
            var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            ViewBag.Braches = db.Branchs.Where(x => x.BranchId == branchId);
            ViewBag.toTrigger = "order_payment";

            appointViewModel.AppointmentList = (from ap in db.Appointments.AsEnumerable()
                                                join mi in db.MembershipInfoes.AsEnumerable() on ap.MemberId equals mi.ID
                                                join br in db.Branchs.AsEnumerable() on ap.BranchId equals br.BranchId
                                                select new AppointmentListViewModel()
                                                {
                                                    MemberName = mi.MemberName,
                                                    MemEmail = mi.MemEmail,
                                                    MemMobileNo = mi.MemMobileNo,
                                                    ServiceDate = ap.ServiceDate,
                                                    BranchName = br.BranchName,
                                                    BookingID = ap.BookingID
                                                }).ToList();
            appointViewModel.ServicePayment = servicepayment;

            return appointViewModel;

        }

        public ActionResult DailySalesReport()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DailySalesReport(string StartDate, string EndDate, int? ServiceId, int? BranchId)
        {
            ViewBag.ReportViewer = DailySalesReportView(StartDate, EndDate, ServiceId, BranchId);

            return View("_ReportViewer");

        }
        public ReportViewer DailySalesReportView(string StartDate, string EndDate, int? ServiceId, int? BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from ap in db.Appointments.AsEnumerable()
                        join od in db.OrderDetails.AsEnumerable() on ap.BookingID equals od.BookingID
                        //join emp in db.Employees.AsEnumerable() on od.Beautician equals emp.EmpID
                        join br in db.Branchs.AsEnumerable() on ap.BranchId equals br.BranchId
                        join sp in db.ServicePayments.AsEnumerable() on od.BookingID equals sp.OrderID
                        join sn in db.Services.AsEnumerable() on od.ServiceID equals sn.ID
                        select new
                        {
                            ap.AppointmentDate,
                            ap.ServiceDate,
                            od.ServiceID,
                            sp.TotalServiceAmount,
                            sp.BookingPayment,
                            sp.AdvancePayment,
                            //od.Beautician,
                            ap.BranchId,
                            br.BranchName,
                            sn.ServiceName

                        };







            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.AppointmentDate >= StartDateRange);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.AppointmentDate <= EndDateRange);
            }

            if (ServiceId != null && ServiceId != 0)
            {
                query = query.Where(s => s.ServiceID == ServiceId);
            }

            //if (BeauticianId != null && BeauticianId != 0)
            //{
            //    query = query.Where(s => s.Beautician == BeauticianId);
            //}


            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.BranchId == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new
                                    {
                                        s.AppointmentDate,
                                        s.ServiceDate,
                                        s.ServiceName,
                                        s.TotalServiceAmount,
                                        s.BookingPayment,
                                        s.AdvancePayment,
                                        //Beautician = db.Employees.Where(w=>w.EmpID==s.Beautician).Select(p=>p.EmpName).SingleOrDefault(),
                                        s.BranchName
                                    }).ToList();


            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\DailySalesReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSDailySales", reportDS));


            return reportViewer;

        }


        //Service Wise Sales Report
        public ActionResult DailySalesReportServiceWise()
        {
            ViewBag.Braches = db.Branchs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DailySalesReportServiceWise(string StartDate, string EndDate, int? ServiceId, int? BeauticianId, int? BranchId)
        {
            ViewBag.ReportViewer = DailySalesReportServiceWiseView(StartDate, EndDate, ServiceId, BeauticianId, BranchId);

            return View("_ReportViewer");

        }
        public ReportViewer DailySalesReportServiceWiseView(string StartDate, string EndDate, int? ServiceId, int? BeauticianId, int? BranchId)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = from ap in db.Appointments.AsEnumerable()
                        join od in db.OrderDetails.AsEnumerable() on ap.BookingID equals od.BookingID
                        join emp in db.Employees.AsEnumerable() on od.Beautician equals emp.EmpID
                        join br in db.Branchs.AsEnumerable() on ap.BranchId equals br.BranchId
                        join sp in db.ServicePayments.AsEnumerable() on od.BookingID equals sp.OrderID
                        select new
                        {
                            ap.AppointmentDate,
                            ap.ServiceDate,
                            od.ServiceID,
                            od.ServiceRate,
                            sp.AdvancePayment,
                            sp.BookingPayment,
                            sp.TotalDiscount,
                            od.Beautician,
                            ap.BranchId,
                            br.BranchName,
                            sp.OrderID
                        };

            if (!string.IsNullOrEmpty(StartDate))
            {
                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.AppointmentDate >= StartDateRange);
            }
            else
            {
                StartDate = query.Min(s => s.AppointmentDate).Date.ToString();
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.AppointmentDate <= EndDateRange);
            }
            else
            {
                EndDate = query.Max(s => s.AppointmentDate).Date.ToString();
            }

            if (ServiceId != null && ServiceId != 0)
            {
                query = query.Where(s => s.ServiceID == ServiceId);
            }

            if (BeauticianId != null && BeauticianId != 0)
            {
                query = query.Where(s => s.Beautician == BeauticianId);
            }


            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.BranchId == BranchId);
            }

            var reportDS = query.Select
                                    (
                                    s => new
                                    {
                                        s.AppointmentDate,
                                        s.ServiceDate,
                                        ServiceName = db.Services.Where(w => w.ID == s.ServiceID).Select(p => p.ServiceName).SingleOrDefault(),
                                        s.ServiceRate,
                                        s.BookingPayment,
                                        s.AdvancePayment,
                                        s.TotalDiscount,
                                        Beautician = db.Employees.Where(w=>w.EmpID==s.Beautician).Select(p=>p.EmpName).SingleOrDefault(),
                                        s.BranchName,
                                        s.OrderID
                                    }).ToList();

            var netSales = reportDS.Sum(x => x.ServiceRate - x.TotalDiscount).ToString();

            ReportParameter[] mypr = new ReportParameter[3];

            mypr[0] = new ReportParameter("StartDate", StartDate.ToString());
            mypr[1] = new ReportParameter("EndDate", EndDate.ToString());
            mypr[2] = new ReportParameter("netSales", netSales.ToString());

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\DailySalesReportServiceWise.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSDailySalesServiceWise", reportDS));
            reportViewer.LocalReport.SetParameters(mypr);
            return reportViewer;

        }



    }
}
