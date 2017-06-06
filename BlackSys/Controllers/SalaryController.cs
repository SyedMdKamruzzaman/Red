using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using System.Dynamic;
using System.ComponentModel;
using System.Data;
using BlackSys.Models;
using BlackSys.Models.ViewModels;

namespace BlackSys.Controllers
{
    public class SalaryController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: Salary
        public ActionResult Index()
        {
            return View();
        }


        public List<Dictionary<string, object>> Read(DbDataReader reader)
        {
            List<Dictionary<string, object>> expandolist = new List<Dictionary<string, object>>();
            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }
                expandolist.Add(new Dictionary<string, object>(expando));
            }
            return expandolist;
        }


        public ActionResult PreProcessSalarySheet()
        {
            DataTable dt = GetPreProcessDataTable();
            return View(dt);
        }

        public ActionResult Export()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=PreProcessSalarySheet.xls");
            Response.ContentType = "application/ms-excel";
            DataTable dt = GetPreProcessDataTable();
            return View(dt);
        }


        private DataTable GetPreProcessDataTable()
        {
            List<Dictionary<string, object>> model = new List<Dictionary<string, object>>();
            using (var ctx = new BlackSysEntities())
            using (var cmd = ctx.Database.Connection.CreateCommand())
            {
                ctx.Database.Connection.Open();
                cmd.CommandText = "EXEC uspPreProSalSheet";
                using (var reader = cmd.ExecuteReader())
                {
                    model = Read(reader).ToList();
                }
            }

            DataTable dt = new DataTable();

            bool IsDataColumnDefined = false;
            foreach (Dictionary<string, object> entry in model)
            {
                if (!IsDataColumnDefined)
                {
                    foreach (var item in entry.Keys)
                    {
                        if (!item.Equals("99999"))
                        {
                            string columnName = "";
                            columnName = db.Allowances.Where(a => a.Id.ToString() == item).Select(a => a.AllowanceName).SingleOrDefault();
                            if (columnName == null)
                            {
                                columnName = db.Deductions.Where(a => a.Id.ToString() == item).Select(a => a.DeductionName).SingleOrDefault();
                                if (columnName == null)
                                { columnName = "Employee Id"; }
                            }

                            dt.Columns.Add(columnName, typeof(string));
                        }
                        else { dt.Columns.Add("Total Salary", typeof(string)); }

                    }
                    IsDataColumnDefined = true;
                }
                DataRow dtr = dt.NewRow();
                int j = 0;
                foreach (var item in entry.Values)
                {
                    dtr[j] = item;
                    j++;
                }
                dt.Rows.Add(dtr);
            }

            return dt;
        }

        public ActionResult GrossSalaryDistribution()
        {
            ViewBag.Branch = db.Branchs.ToList();

            EmpGrossSalViewModel empGrossSalViewModel = new EmpGrossSalViewModel();

            empGrossSalViewModel.EmpGrossSalListViewModel = (from emp in db.Employees
                                                             select new EmpGrossSalViewModel
                                                             {
                                                                 selected = false,
                                                                 EmployeeId = emp.EmployeeId,
                                                                 EmployeeName = emp.EmpName,
                                                                 GrossSalary = emp.GrossSalary
                                                             }).ToList();
            return View(empGrossSalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DistributeGrossSalary(EmpGrossSalViewModel empGrossSalViewModel)
        {

            for (int i = 0; i < empGrossSalViewModel.EmpGrossSalListViewModel.Count; i++)
            {
                if (empGrossSalViewModel.EmpGrossSalListViewModel[i].selected)
                {
                    //To be distributed

                    var employeeId = empGrossSalViewModel.EmpGrossSalListViewModel[i].EmployeeId;

                    var grossSalaryAmt = empGrossSalViewModel.EmpGrossSalListViewModel[i].GrossSalary;

                    var grossSalaryPer = db.GrossSalaryDistributions.ToList();

                    decimal totalAllowanceAmt = 0;
                    FixedAllowance fxdAllowance;

                    using (BlackSysEntities dbEntities = new BlackSysEntities())
                    {

                        for (int j = 0; j < grossSalaryPer.Count; j++)
                        {
                            var allowanceId = grossSalaryPer[j].AllowanceId;
                            var percentage = grossSalaryPer[j].PercentageOfGross;

                            var fxdAllowanceDB = from fa in dbEntities.FixedAllowances where (fa.EmployeeId == employeeId && fa.AllowanceId == allowanceId) select fa;
                            if (fxdAllowanceDB.Count() != 0)
                            {
                                fxdAllowance = fxdAllowanceDB.First();
                                fxdAllowance.EmployeeId = employeeId;
                                fxdAllowance.AllowanceId = allowanceId;
                                var allowanceAmount = Math.Round((grossSalaryAmt * percentage) / 100);
                                fxdAllowance.AllowanceAmount = allowanceAmount;
                                totalAllowanceAmt += allowanceAmount;
                                fxdAllowance.IsActive = true;

                            }
                            else
                            {
                                fxdAllowance = new FixedAllowance();
                                fxdAllowance.EmployeeId = employeeId;
                                fxdAllowance.AllowanceId = allowanceId;
                                var allowanceAmount = Math.Round((grossSalaryAmt * percentage) / 100);
                                fxdAllowance.AllowanceAmount = allowanceAmount;
                                totalAllowanceAmt += allowanceAmount;
                                fxdAllowance.IsActive = true;

                                dbEntities.FixedAllowances.Add(fxdAllowance);

                            }

                            dbEntities.SaveChanges();

                        }


                        var fixedAllowance = from fa in dbEntities.FixedAllowances where (fa.EmployeeId == employeeId && fa.AllowanceId == 3) select fa;
                        if (fixedAllowance.Count() != 0)
                        {
                            fxdAllowance = fixedAllowance.First();
                            fxdAllowance.EmployeeId = employeeId;
                            fxdAllowance.AllowanceId = 3;
                            fxdAllowance.AllowanceAmount = grossSalaryAmt - totalAllowanceAmt;
                            fxdAllowance.IsActive = true;

                        }
                        else
                        {
                            fxdAllowance = new FixedAllowance();
                            fxdAllowance.EmployeeId = employeeId;
                            fxdAllowance.AllowanceId = 3;
                            fxdAllowance.AllowanceAmount = grossSalaryAmt - totalAllowanceAmt;
                            fxdAllowance.IsActive = true;
                            dbEntities.FixedAllowances.Add(fxdAllowance);

                        }


                        var fxdDeductionDB = from fd in dbEntities.FixedDeductions where (fd.EmployeeId == employeeId && fd.DeductionId == 1001) select fd;
                        if (fxdDeductionDB.Count() != 0)
                        {
                            var fxdDeduction = fxdDeductionDB.First();
                            fxdDeduction.EmployeeId = employeeId;
                            fxdDeduction.DeductionId = 1001;
                            fxdDeduction.DeductionAmount = 30;
                            fxdDeduction.IsActive = true;

                        }
                        else
                        {
                            FixedDeduction fxdDeduction = new FixedDeduction();
                            fxdDeduction.EmployeeId = employeeId;
                            fxdDeduction.DeductionId = 1001;
                            fxdDeduction.DeductionAmount = 30;
                            fxdDeduction.IsActive = true;

                            dbEntities.FixedDeductions.Add(fxdDeduction);

                        }

                        dbEntities.SaveChanges();

                    }

                }
            }

            ViewBag.Branch = db.Branchs.ToList();

            empGrossSalViewModel = new EmpGrossSalViewModel();

            empGrossSalViewModel.EmpGrossSalListViewModel = (from emp in db.Employees
                                                             select new EmpGrossSalViewModel
                                                             {
                                                                 selected = false,
                                                                 EmployeeId = emp.EmployeeId,
                                                                 EmployeeName = emp.EmpName,
                                                                 GrossSalary = emp.GrossSalary
                                                             }).ToList();

            TempData["ResultMessage"] = "Gross Salary Distributed Successfully.";
            TempData["ResultType"] = "S";

            return View("GrossSalaryDistribution", empGrossSalViewModel);
        }





        public ActionResult GetSalaryProcessData()
        {
            return View();
        }

    }
}