using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
namespace BlackSys.Controllers
{
    public class FileController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
	}
}