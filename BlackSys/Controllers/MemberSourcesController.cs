using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace BlackSys.Controllers
{
    public class MemberSourcesController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /MemberSources/
        public ActionResult Index()
        {
            return View(db.MemberSources.ToList());
        }

        // GET: /MemberSources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSource memberSource = db.MemberSources.Find(id);
            if (memberSource == null)
            {
                return HttpNotFound();
            }
            return View(memberSource);
        }

        // GET: /MemberSources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MemberSources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name")] MemberSource memberSource)
        {
            if (ModelState.IsValid)
            {
                db.MemberSources.Add(memberSource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberSource);
        }

        // GET: /MemberSources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSource memberSource = db.MemberSources.Find(id);
            if (memberSource == null)
            {
                return HttpNotFound();
            }
            return View(memberSource);
        }

        // POST: /MemberSources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name")] MemberSource memberSource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberSource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberSource);
        }

        // GET: /MemberSources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSource memberSource = db.MemberSources.Find(id);
            if (memberSource == null)
            {
                return HttpNotFound();
            }
            return View(memberSource);
        }

        // POST: /MemberSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberSource memberSource = db.MemberSources.Find(id);
            db.MemberSources.Remove(memberSource);
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


        public ActionResult MemberSourceReport()
        {
            ViewBag.MemSource = db.MemberSources.ToList();
            ViewBag.Branch = db.Branchs.ToList();
            return View("MemberSourceReport");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberSourceReport(string StartDate, string EndDate, int? BranchId, int? MemberSource)
        {
            ViewBag.ReportViewer = MemberSourceReportView(StartDate, EndDate, BranchId, MemberSource);

            return View("_ReportViewer");

        }
        public ReportViewer MemberSourceReportView(string StartDate, string EndDate, int? BranchId, int? MemberSource)
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ShowPrintButton = true;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);



            var query = (from mi in db.MembershipInfoes.AsEnumerable()
                         join ms in db.MemberSources.AsEnumerable() on mi.MemberSourceId equals ms.Id
                         join bh in db.Branchs.AsEnumerable() on mi.BranchId equals bh.BranchId
                         select new
                         {
                           mi.RegistrationDate,
                             bh.BranchName,
                             bh.BranchId,
                             ms.Id,
                             ms.Name

                         });


            if (!string.IsNullOrEmpty(StartDate))
            {

                var StartDateRange = Convert.ToDateTime(StartDate);
                query = query.Where(s => s.RegistrationDate >= StartDateRange);
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                var EndDateRange = Convert.ToDateTime(EndDate);
                query = query.Where(s => s.RegistrationDate <= EndDateRange);
            }


            if (BranchId != null && BranchId != 0)
            {
                query = query.Where(s => s.BranchId == BranchId);
            }

            if (MemberSource != null && MemberSource != 0)
            {
                query = query.Where(s => s.Id == MemberSource);
            }

            var reportDS = query.GroupBy(x=> new { x.BranchName, x.Name }).Select
                                    (
                                    s => new
                                    { 
                                         s.Key.BranchName,
                                        SourceName =  s.Key.Name,
                                         MemCount = s.Count()                                                                         
                                    }).OrderBy(x=>x.BranchName).OrderByDescending(x=>x.MemCount).ToList();
            ReportParameter[] mypr = new ReportParameter[2];

            mypr[0] = new ReportParameter("StartDate", StartDate.ToString());
            mypr[1] = new ReportParameter("EndDate", EndDate.ToString());

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\MemSourceReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ReportDSMemberSource", reportDS));
            reportViewer.LocalReport.SetParameters(mypr);

            return reportViewer;

        }
    }
}
