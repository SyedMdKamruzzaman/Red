using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models.ViewModels;
using BlackSys.Models;
using System.Reflection;
using BlackSys.Helpers;
using System.Text;

namespace BlackSys.Controllers
{
    public class FeedbackController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Feedback/
        public ActionResult Index()
        {
                       
            return View(db.FeedbackDetails.ToList());
        }

        // GET: /Feedback/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackViewModel feedbackviewmodel = db.FeedbackViewModels.Find(id);
            if (feedbackviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(feedbackviewmodel);
        }

        // GET: /Feedback/Create
        public ActionResult Create()
        {
            FeedbackViewModel vm = new FeedbackViewModel();

            var questions = db.FeedbackQuestion.ToList();
            var answers = db.FeedbackRatingPoints.ToList();
            ViewBag.BeauticianId = (db.Employees.Where(a => a.DeptID == 7));
            foreach (var q in questions)
            {
                FeedbackQuest fq = new FeedbackQuest();

                fq.Id = q.Id;
                fq.Questions = q.Questions;
                fq.PossibleAnswers = answers;
                
                vm.feedbackQuestionsList.Add(fq);

            }

          

            return View(vm);
        }

        public ActionResult GetBeauticianByOrderId(int OrderId)
        {
            var data = (from od in db.OrderDetails.Where(w=>w.BookingID == OrderId).AsEnumerable()
                        join emp in db.Employees.AsEnumerable() on od.Beautician equals emp.EmpID
                        select new
                        {
                            emp.EmpID,
                            emp.EmpName
                        }).ToList();

            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        // POST: /Feedback/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedbackViewModel feedbackviewmodel)
        {
            if (!ModelState.IsValid)
            {
                for (int i = 0; i < feedbackviewmodel.feedbackQuestionsList.Count; i++)
                {
                    FeedbackDetails feedbackDetails = new FeedbackDetails();
                    feedbackDetails.OrderId = feedbackviewmodel.OrderId;
                    feedbackDetails.BeauticianId = feedbackviewmodel.BeauticianId;
                    feedbackDetails.QuestionId = feedbackviewmodel.feedbackQuestionsList[i].Id;
                    feedbackDetails.RatingPoint =(int)feedbackviewmodel.feedbackQuestionsList[i].SelectedAnswer;
                    feedbackDetails.Comments = feedbackviewmodel.Comments;
                    db.FeedbackDetails.Add(feedbackDetails);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FeedbackViewModel vm = new FeedbackViewModel();

            var questions = db.FeedbackQuestion.ToList();
            var answers = db.FeedbackRatingPoints.ToList();

            foreach (var q in questions)
            {
                FeedbackQuest fq = new FeedbackQuest();

                fq.Id = q.Id;
                fq.Questions = q.Questions;
                fq.PossibleAnswers = answers;

                vm.feedbackQuestionsList.Add(fq);

            }



            return View(vm);
        }

        // GET: /Feedback/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackViewModel feedbackviewmodel = db.FeedbackViewModels.Find(id);
            if (feedbackviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(feedbackviewmodel);
        }

        // POST: /Feedback/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id")] FeedbackViewModel feedbackviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackviewmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedbackviewmodel);
        }

        // GET: /Feedback/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeedbackViewModel feedbackviewmodel = db.FeedbackViewModels.Find(id);
            if (feedbackviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(feedbackviewmodel);
        }

        // POST: /Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedbackViewModel feedbackviewmodel = db.FeedbackViewModels.Find(id);
            db.FeedbackViewModels.Remove(feedbackviewmodel);
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
