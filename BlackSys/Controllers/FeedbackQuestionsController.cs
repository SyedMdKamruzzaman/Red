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
    public class FeedbackQuestionsController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /FeedbackQuestions/
        public ActionResult Index()
        {
            return View(db.FeedbackQuestion.ToList());
        }

        // GET: /FeedbackQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackQuest feedbackquestions = db.FeedbackQuestion.Find(id);
            if (feedbackquestions == null)
            {
                return HttpNotFound();
            }
            return View(feedbackquestions);
        }

        // GET: /FeedbackQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FeedbackQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Questions,SelectedAnswer")] FeedbackQuest feedbackquestions)
        {
            if (ModelState.IsValid)
            {
                db.FeedbackQuestion.Add(feedbackquestions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedbackquestions);
        }

        // GET: /FeedbackQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackQuest feedbackquestions = db.FeedbackQuestion.Find(id);
            if (feedbackquestions == null)
            {
                return HttpNotFound();
            }
            return View(feedbackquestions);
        }

        // POST: /FeedbackQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Questions,SelectedAnswer")] FeedbackQuest feedbackquestions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackquestions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedbackquestions);
        }

        // GET: /FeedbackQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackQuest feedbackquestions = db.FeedbackQuestion.Find(id);
            if (feedbackquestions == null)
            {
                return HttpNotFound();
            }
            return View(feedbackquestions);
        }

        // POST: /FeedbackQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedbackQuest feedbackquestions = db.FeedbackQuestion.Find(id);
            db.FeedbackQuestion.Remove(feedbackquestions);
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
