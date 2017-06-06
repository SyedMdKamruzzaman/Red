using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using BlackSys.Models;
using Blacksys.Controllers;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;



namespace BlackSys.Helpers
{
    public class Utility
    {
        //public ActionResult _ReportView(dynamic Query, string ReportName, string DataSetName)
        //{
        //    ReportViewer reportViewer = new ReportViewer();
        //    reportViewer.ProcessingMode = ProcessingMode.Local;
        //    reportViewer.ShowPrintButton = true;
        //    reportViewer.SizeToReportContent = true;
        //    reportViewer.Width = Unit.Percentage(100);
        //    reportViewer.Height = Unit.Percentage(100);

        //    //reportViewer.LocalReport.ReportPath =
        //    reportViewer.LocalReport.ReportPath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) + @"Reports\"+ ReportName;
        //    reportViewer.LocalReport.DataSources.Add(new ReportDataSource(DataSetName, Query.ToList()));


        //    return View("");

        //}

    }
}