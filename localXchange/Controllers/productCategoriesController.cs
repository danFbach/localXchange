using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using localXchange.Models;

namespace localXchange.Controllers
{
    public class productCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: productCategories
        public ActionResult Index()
        {
            return View(db.productCategory.ToList());
        }

        // GET: productCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productCategory productCategory = db.productCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // GET: productCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: productCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(productCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.productCategory.Add(productCategory);
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productCategory);
        }

        // GET: productCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productCategory productCategory = db.productCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: productCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(productCategory productCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCat).State = EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productCat);
        }

        // GET: productCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productCategory productCategory = db.productCategory.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: productCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productCategory productCategory = db.productCategory.Find(id);
            db.productCategory.Remove(productCategory);
            db.SaveChangesAsync();
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
