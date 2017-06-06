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

namespace BlackSys.Controllers
{
    public class StockOutController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /StockOut/
        public ActionResult Index()
        {
            var stockballist = (from data in db.StockOuts.AsEnumerable()
                               join lang in db.RequisitionItemCategoryModels.AsEnumerable()
                               on data.categoryid equals lang.ID
                               join bnh in db.RequisitionItems.AsEnumerable()
                               on data.productid equals bnh.ID
                               join brnch in db.Branchs.AsEnumerable()
                               on data.branchid equals brnch.BranchId
                               join dep in db.Departments.AsEnumerable()
                               on data.depid equals dep.DeptId
                               join emp in db.Employees.AsEnumerable()
                               on data.empid equals emp.EmpID
                               select new StockOutModel()
                               {
                                   stockoutdate = data.stockoutdate,
                                   category = lang.Category,
                                   product  =bnh.Item,
                                   quantity = data.quantity,
                                   branch = brnch.BranchName,
                                   department = dep.DepartmentName,
                                   employee = emp.EmpName

                               }).ToList();

            return View(stockballist);
            
        }

        // GET: /StockOut/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOutModel stockoutmodel = db.StockOuts.Find(id);
            if (stockoutmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockoutmodel);
        }

        // GET: /StockOut/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            return View();
        }

        // POST: /StockOut/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockOutModel stockoutmodel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var quantity = db.Stocks.Where(s => s.productid == stockoutmodel.productid).Where(x=>x.branchid==stockoutmodel.branchid).Select(s=>s.quantity).FirstOrDefault();
                    Stock stockModel = db.Stocks.Where(s => s.productid == stockoutmodel.productid).FirstOrDefault();


                    if (quantity > 0)
                    {
                        DbEntityEntry<Stock> entry = db.Entry(stockModel);
                        entry.State = EntityState.Modified;
                        entry.Property(s => s.branchid).IsModified = false;
                        entry.Property(s => s.categoryid).IsModified = false;
                        entry.Property(s => s.condition).IsModified = false;
                        entry.Property(s => s.EntryBy).IsModified = false;
                        entry.Property(s => s.EntryByIp).IsModified = false;
                        entry.Property(s => s.EntryDateTime).IsModified = false;
                        entry.Property(s => s.invoiceno).IsModified = false;
                        entry.Property(s => s.productid).IsModified = false;



                        entry.Property(s => s.quantity).CurrentValue = entry.Property(s => s.quantity).OriginalValue - stockoutmodel.quantity;
                        db.StockOuts.Add(stockoutmodel);
                        try
                        {
                            db.SaveChanges();
                            TempData["ResultMessage"] = "Stock Out Successfull.";
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
                        TempData["ResultMessage"] = "Fill your Stock First then Stock Out";
                        TempData["ResultType"] = "E";
                       
                    }
                                       

                    return RedirectToAction("Index", "Stock");
                }
                var userId = User.Identity.GetUserId();               
                ViewBag.Braches = CommonRepository.GetBranchList(userId);
            }

            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(stockoutmodel);
        }

        // GET: /StockOut/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOutModel stockoutmodel = db.StockOuts.Find(id);
            if (stockoutmodel == null)
            {
                return HttpNotFound();
            }
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            return View(stockoutmodel);
        }

        // POST: /StockOut/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StockOutModel stockoutmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockoutmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var userId = User.Identity.GetUserId();
            ViewBag.Braches = CommonRepository.GetBranchList(userId);
            return View(stockoutmodel);
        }

        // GET: /StockOut/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockOutModel stockoutmodel = db.StockOuts.Find(id);
            if (stockoutmodel == null)
            {
                return HttpNotFound();
            }
            return View(stockoutmodel);
        }

        // POST: /StockOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockOutModel stockoutmodel = db.StockOuts.Find(id);
            db.StockOuts.Remove(stockoutmodel);
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

        public ActionResult GetEmployee(string q)
        {
            q = q.ToUpper();
            //var userId = User.Identity.GetUserId();
            //var empBranch = CommonRepository.GetBranchId(userId);
            var users = db.Employees
                .Where(a => a.EmpName.ToUpper().Contains(q))
                .Select(a => new { id = a.EmpID, name = a.EmpName});

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDepartment(string q)
        {
            q = q.ToUpper();
            var users = db.Departments
                .Where(a => a.DepartmentName.ToUpper().Contains(q))
                .Select(a => new { id = a.DeptId, name = a.DepartmentName });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProduct(string q,string r)
        {
            q = q.ToUpper();
            var users = db.Products
                .Where(a => a.product.ToUpper().Contains(q))
                .Select(a => new { id = a.productid, name = a.product });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategory(string q)
        {
            q = q.ToUpper();
            var users = db.RequisitionItemCategoryModels
                .Where(a => a.Category.ToUpper().Contains(q))
                .Select(a => new { id = a.ID, name = a.Category });

            return Json(users, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAvailableQty(int id)
        {
            var data = db.Stocks.Where(w => w.productid == id).Select(a => new { a.quantity }).FirstOrDefault();

            return Json(data, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

    }
}
