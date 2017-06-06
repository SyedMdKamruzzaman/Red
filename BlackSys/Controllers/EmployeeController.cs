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
using BlackSys.Models.ViewModels;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;

namespace BlackSys.Controllers
{
    public class EmployeeController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Employee/
        public ActionResult Index()
        {
            var myResult = (from data in db.Employees.AsEnumerable()
                            join lang in db.Departments.AsEnumerable()                            
                            on data.DeptID equals lang.DeptId
                            join bnh in db.Branchs.AsEnumerable()
                            on data.BranchID equals bnh.BranchId
                            select new Employee()
                            {
                                EmpID = data.EmpID,
                                EmployeeId = data.EmployeeId,
                                EmpName = data.EmpName,
                                EmpMobileNo = data.EmpMobileNo,
                                EmpEmail = data.EmpEmail,
                                DepName = lang.DepartmentName,
                                BloodGroup = data.BloodGroup,
                                BranchName = bnh.BranchName

                            }).ToList();
            return View(myResult);
            
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Include(s => s.Files).SingleOrDefault(s => s.EmpID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            ViewBag.Braches = db.Branchs.ToList();
            ViewBag.Depart = db.Departments.ToList();
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        employee.Files = new List<File> { avatar };
                    }
                    ViewBag.Braches = db.Branchs;
                    ViewBag.Depart = db.Departments;
                    db.Employees.Add(employee);
                    try
                    {
                        db.SaveChanges();
                        TempData["ResultMessage"] = "The data was stored properly";
                        TempData["ResultType"] = "S";
                    }
                    catch (Exception ex)
                    {

                        TempData["ResultMessage"] = "Ops:Error saving data! " + ex.Message;
                        TempData["ResultType"] = "E";
                    };
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }


        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Include(s => s.Files).SingleOrDefault(s => s.EmpID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Braches = db.Branchs;
            ViewBag.Depart = db.Departments;
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employeeToUpdate = db.Employees.Find(id);
            if (TryUpdateModel(employeeToUpdate, "",
                new string[] { "EmpID", "BranchID", "EmployeeId", "IDInMachine", "EmpName", "ResAddress", "EmpMobileNo", "EmpEmail", "JoiningDate", "BirthDate", "FathersName", "MothersName", "Religion", "MaritalStatus", "Nationality", "NationalIDNo", "Sex", "BloodGroup", "DeptID", "ResignationDate", "EntryBy", "EntryDateTime", "EntryByIp", "UpdatedBy", "UpdatedDateTime", "UpdatedByIp" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (employeeToUpdate.Files.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.Files.Remove(employeeToUpdate.Files.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        employeeToUpdate.Files = new List<File> { avatar };
                    }
                    db.Entry(employeeToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(employeeToUpdate);
        }
        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Include(s => s.Files).SingleOrDefault(s => s.EmpID == id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            List<Employee> employeeList = db.Employees.ToList();

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RptEmployees.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RptDSEmployee", employeeList));

            ViewBag.ReportViewer = reportViewer;
            return View("__Report");

            //AppointmentViewModel appointViewModel =new AppointmentViewModel();
            //return View(appointViewModel);
        }

    }
}
