using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using BlackSys.Models.ViewModels;
using System.Text;
using BlackSys.Filter;
using System.Data.Entity.Infrastructure;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    public class ProductController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();

        // GET: /Product/
        public ActionResult Index()
        {
 
            var products = (from pro in db.Products.AsEnumerable()
                               join unt in db.UnitModels.AsEnumerable() on pro.UnitId equals unt.Id
                               join cat in db.RequisitionItemCategoryModels.AsEnumerable() on pro.categoryid equals cat.ID
                               
                               select new ProductModel()
                               {
                                   productid = pro.productid,
                                   CategoryName = cat.Category,
                                   product=pro.product,
                                   Units = unt.Name,
                                   ProductDetails = pro.ProductDetails
                               }).ToList();
            return View(products);
        }

        // GET: /Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productmodel = db.Products.Include(s => s.ProductPhotos).SingleOrDefault(s => s.productid == id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }
        public ActionResult GetCategory(string q)
        {
            q = q.ToUpper();
            var cat = db.RequisitionItemCategoryModels
                .Where(a => a.Category.ToUpper().Contains(q))
                .Select(a => new { name = a.Category, id = a.ID });

            return Json(cat, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProduct(string q)
        {
            q = q.ToUpper();
            var prod = db.Products
                .Where(a => a.product.ToUpper().Contains(q))
                .Select(a => new { name = a.product, id = a.productid });

            return Json(prod, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,categoryid,product,ProductDetails,EntryBy,EntryDateTime,EntryByIp,UpdatedBy,UpdatedDateTime,UpdatedByIp")] ProductModel productmodel, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var avatar = new ProductPhotosModel
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        productmodel.ProductPhotos = new List<ProductPhotosModel> { avatar };
                    }
                    
                    db.Products.Add(productmodel);
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
            return View(productmodel);
            //if (ModelState.IsValid)
            //{
            //    db.Products.Add(productmodel);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(productmodel);
        }

        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ProductModel productmodel = db.Products.Include(s => s.ProductPhotos).SingleOrDefault(s => s.productid == id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        // POST: /Product/Edit/5
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
            var productToUpdate = db.Products.Find(id);
            if (TryUpdateModel(productToUpdate, "",
                new string[] { "productid", "categoryid", "product", "ProductDetails", "EntryBy", "EntryDateTime", "EntryByIp", "UpdatedBy", "UpdatedDateTime", "UpdatedByIp" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (productToUpdate.ProductPhotos.Any(f => f.FileType == FileType.Avatar))
                        {
                            db.ProductPhotos.Remove(productToUpdate.ProductPhotos.First(f => f.FileType == FileType.Avatar));
                        }
                        var avatar = new ProductPhotosModel
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Avatar,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        productToUpdate.ProductPhotos = new List<ProductPhotosModel> { avatar };
                    }
                    db.Entry(productToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(productToUpdate);
        }

        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productmodel = db.Products.Include(s => s.ProductPhotos).SingleOrDefault(s => s.productid == id);
            if (productmodel == null)
            {
                return HttpNotFound();
            }
            return View(productmodel);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productmodel = db.Products.Find(id);
            db.Products.Remove(productmodel);
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
