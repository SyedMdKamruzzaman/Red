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
    public class FeedbackRatingPointsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /FeedbackRatingPoints/
        public ActionResult Index()
        {
            return View(db.FeedbackRatingPoints.ToList());
        }

        // GET: /FeedbackRatingPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackRatingPoints feedbackratingpoints = db.FeedbackRatingPoints.Find(id);
            if (feedbackratingpoints == null)
            {
                return HttpNotFound();
            }
            return View(feedbackratingpoints);
        }

        // GET: /FeedbackRatingPoints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FeedbackRatingPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Points")] FeedbackRatingPoints feedbackratingpoints)
        {
            if (ModelState.IsValid)
            {
                db.FeedbackRatingPoints.Add(feedbackratingpoints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedbackratingpoints);
        }

        // GET: /FeedbackRatingPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackRatingPoints feedbackratingpoints = db.FeedbackRatingPoints.Find(id);
            if (feedbackratingpoints == null)
            {
                return HttpNotFound();
            }
            return View(feedbackratingpoints);
        }

        // POST: /FeedbackRatingPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Points")] FeedbackRatingPoints feedbackratingpoints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackratingpoints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedbackratingpoints);
        }

        // GET: /FeedbackRatingPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackRatingPoints feedbackratingpoints = db.FeedbackRatingPoints.Find(id);
            if (feedbackratingpoints == null)
            {
                return HttpNotFound();
            }
            return View(feedbackratingpoints);
        }

        // POST: /FeedbackRatingPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedbackRatingPoints feedbackratingpoints = db.FeedbackRatingPoints.Find(id);
            db.FeedbackRatingPoints.Remove(feedbackratingpoints);
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
