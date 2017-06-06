using BlackSys.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using BlackSys.Models.ViewModels;
using Newtonsoft.Json.Linq;
using System;

namespace BlackSys.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        public ActionResult Index()
        {
           
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult LoadMenu()
        {
            var menu = from a in db.MenuTemp
                               select a;
            return View(menu.ToList());
        }


       
    }
}