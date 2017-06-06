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

namespace BlackSys.Controllers
{
    //[Authorize]
    //[AccessValidation]
    public class MemberShipInfoController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        CommonRepository cm = new CommonRepository();

        // GET: /MemberShipInfo/
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var branchId = db.AspNetUsers.Where(u => u.Id == userId).Select(p => p.BranchId).FirstOrDefault();
            var memberInfo = (from mi in db.MembershipInfoes.Where(x => branchId == 1 ? x.BranchId != 0 : x.BranchId == branchId).AsEnumerable()
                              join br in db.Branchs.AsEnumerable()
                              on mi.BranchId equals br.BranchId
                              select new MembershipInfo
                              { 
                                  ID = mi.ID,
                                  MemberName = mi.MemberName,
                                  MemMobileNo = mi.MemMobileNo,
                                  MemEmail = mi.MemEmail,
                                  CardId = mi.CardId,
                                  CardType = db.CardTypes.Where(c => mi.CardTypeId != null && mi.CardTypeId != 0 && c.Id == mi.CardTypeId).Select(s => s.Type).SingleOrDefault(),
                                  Discount = db.CardTypes.Where(c => mi.CardTypeId != null && mi.CardTypeId != 0 && c.Id == mi.CardTypeId).Select(s => s.DiscountPercentage).SingleOrDefault(),
                                  TotalPointAchieved = mi.TotalPointAchieved,
                                  MemberSource = db.MemberSources.Where(c => mi.MemberSourceId != null && mi.MemberSourceId != 0 && c.Id == mi.MemberSourceId).Select(s => s.Name).SingleOrDefault(),
                                  BranchName = br.BranchName,
                              }).ToList(); 

            //var memberinfo = (from mi in db.MembershipInfoes.AsEnumerable()
            //                  join ct in db.CardTypes.AsEnumerable()
            //                  on mi.CardTypeId equals ct.Id
            //                  select new MembershipInfo
            //                  {
            //                      ID = mi.ID,
            //                      MemberName = mi.MemberName,
            //                      MemMobileNo = mi.MemMobileNo,
            //                      MemEmail = mi.MemEmail,
            //                      CardId = mi.CardId,
            //                      CardType = ct.Type,
            //                      Discount = mi.Discount,
            //                      TotalPointAchieved = mi.TotalPointAchieved
            //                  }).ToList();

            return View(memberInfo);
        }

        // GET: /MemberShipInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipInfo membershipinfo = db.MembershipInfoes.Find(id);
            if (membershipinfo == null)
            {
                return HttpNotFound();
            }
            return View(membershipinfo);
        }

        // GET: /MemberShipInfo/Create
        public ActionResult CreateMember()
        {
            ViewBag.CardType = db.CardTypes.ToList();
            ViewBag.MemberSource = db.MemberSources.ToList();
            ViewBag.Braches = db.Branchs.ToList();
            return View();
        }

        public ActionResult CreateCustomer()
        {
           
            ViewBag.MemberSource = db.MemberSources.ToList();
            
            return View();
        }

        //Get Discount percentage

        public ActionResult GetDiscountByCardId(int CardTypeId)
        {
            var data = db.CardTypes.Where(w => w.Id == CardTypeId).Select(s => s.DiscountPercentage).SingleOrDefault();

            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        // POST: /MemberShipInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember(MembershipInfo membershipinfo)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var userBranchId = CommonRepository.GetBranchId(userId);
                membershipinfo.BranchId = userBranchId;
                membershipinfo.RegistrationDate = DateTime.Now;

                db.MembershipInfoes.Add(membershipinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CardType = db.CardTypes.ToList();
            ViewBag.Braches = db.Branchs.ToList();

            return View(membershipinfo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(MembershipInfo membershipinfo)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var userBranchId = CommonRepository.GetBranchId(userId);
                membershipinfo.BranchId = userBranchId;
                membershipinfo.RegistrationDate = DateTime.Now;
                
                db.MembershipInfoes.Add(membershipinfo);
                db.SaveChanges();
                return RedirectToAction("Create","Appointment");
            }
            return View(membershipinfo);
        }

        // GET: /MemberShipInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipInfo membershipinfo = db.MembershipInfoes.Find(id);
            if (membershipinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CardType = db.CardTypes.ToList();
            ViewBag.Braches = db.Branchs.ToList();
            ViewBag.MemberSource = db.MemberSources.ToList();
            return View(membershipinfo);
        }

        // POST: /MemberShipInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MembershipInfo membershipinfo)
        {
            var modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);
            if (ModelState.IsValid)
            {
                db.Entry(membershipinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CardType = db.CardTypes.ToList();
            ViewBag.Braches = db.Branchs.ToList();
            return View(membershipinfo);
        }

        // GET: /MemberShipInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipInfo membershipinfo = db.MembershipInfoes.Find(id);
            if (membershipinfo == null)
            {
                return HttpNotFound();
            }
            return View(membershipinfo);
        }

        // POST: /MemberShipInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipInfo membershipinfo = db.MembershipInfoes.Find(id);
            db.MembershipInfoes.Remove(membershipinfo);
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
