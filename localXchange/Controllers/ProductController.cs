using System;
using System.IO;
using System.Web;
using System.Net;
using System.Linq;
using System.Data;
using System.Web.Mvc;
using localXchange.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace localXchange.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsAuthenticated
        {
            get
            {
                return this.User != null &&
                       this.User.Identity != null &&
                       this.User.Identity.IsAuthenticated;
            }
        }
        public ActionResult Index()
        {
            if (!IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.StatusMessage = TempData["ErrorMessage"].ToString();
            }
            string curUser = User.Identity.GetUserId();
            var products = db.productmodel.ToList();
            List<productModel> curUserProducts = new List<productModel>();

            foreach (var product in products)
            {
                if (product.sellerID == curUser)
                {
                    curUserProducts.Add(product);
                }
            }
            return View(curUserProducts);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productDetailsViewModel model = new Models.productDetailsViewModel();
            model.productModel = db.productmodel.Find(id);
            model.productModel.sellerModel = db.Users.Find(model.productModel.sellerID);
            model.productModel.unitModel = db.unitsModel.Find(model.productModel.unitID);
            model.productImageCollectionModel = new List<Models.productImage>();
            foreach (var item in db.productImage.ToList())
            {
                if (item.productID == id)
                {
                    model.productImageCollectionModel.Add(item);
                }
            }
            return View(model);
        }

        public ActionResult addPictures(int productID)
        {
            if (User.Identity.GetUserId() == db.productmodel.Find(productID).sellerID)
            {
                addProductImagesModel model = new addProductImagesModel();
                model.productID = productID;
                return View(productID);
            }
            else
            {
                TempData["StatusMessage"] = "This product does not belong to you, therefore you are not allowed to modify it.";
                return RedirectToAction("publicListing");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addPictures(int productID, List<HttpPostedFileBase> files)
        {
            if (User.Identity.GetUserId() == db.productmodel.Find(productID).sellerID)
            {
                productModel product = db.productmodel.Find(productID);
                string relativePath = @"/ProductPhotos/" + product.productName + @"/";
                if (files.Count() > 0)
                {
                    if (!Directory.Exists(Server.MapPath(relativePath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(relativePath));
                    }
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null)
                        {
                            productImage image = new Models.productImage();
                            string fileSavePath = Server.MapPath(relativePath + file.FileName);
                            file.SaveAs(fileSavePath);
                            image.fileName = file.FileName;
                            image.relativePath = relativePath + file.FileName;
                            image.productID = productID;
                            db.productImage.Add(image);
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productID);
                }
            }
            else
            {
                TempData["StatusMessage"] = "This product does not belong to you, therefore you are not allowed to modify it.";
                return RedirectToAction("publicListing");
            }
        }
        // GET: Product/createCategory
        [HttpGet]
        public ActionResult createCategory()
        {
            return View();
        }

        // POST: Product/createCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createCategory(productCategory model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.productCategory.Add(model);
            db.SaveChanges();
            return RedirectToAction("Create", "Product", new { categoryID = model.ID });
        }

        //CREATE PRODUCT STEP ONE [GET]
        public ActionResult createProductStepOne()
        {
            createProductViewModel viewModel = new createProductViewModel();
            List<productCategory> categories = db.productCategory.ToList();
            viewModel.categories = new List<SelectListItem>();
            viewModel.categories.Add(new SelectListItem { Text = "New Category", Value = "-1" });
            foreach (productCategory category in categories)
            {
                viewModel.categories.Add(new SelectListItem { Text = category.categoryName, Value = category.ID.ToString() });
            }
            return View(viewModel);
        }

        //CREATE PRODUCT STEP ONE [POST]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult createProductStepOne(createProductViewModel viewModel)
        {
            if (viewModel.categoryID > 0)
            {
                return RedirectToAction("Create", new { categoryID = viewModel.categoryID });
            }
            else
            {
                return View(viewModel);
            }
        }

        // GET: Product/create
        public ActionResult Create(int categoryID)
        {
            createProductViewModel newProduct = new Models.createProductViewModel();
            newProduct.category = db.productCategory.Find(categoryID);
            if (newProduct.category != null)
            {
                userProfileModel userProfile = db.userprofilemodel.Find(User.Identity.GetUserId());
                if (userProfile != null)
                {
                    newProduct.sellerID = userProfile.ID;
                    newProduct.seller = db.Users.Find(userProfile.UserID);
                    newProduct.unitList = new List<SelectListItem>();
                    foreach (var unit in db.unitsModel.ToList())
                    {
                        newProduct.unitList.Add(new SelectListItem { Text = unit.unitAbvr, Value = unit.ID.ToString() });
                    }
                }
                return View("Create", newProduct);
            }
            else
            {
                return RedirectToAction("createProductStepOne");
            }
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(createProductViewModel oldModel)
        {
            if (oldModel.categoryID == -1)
            {
                return RedirectToAction("createCategory");
            }
            oldModel.category = db.productCategory.Find(oldModel.categoryID);
            oldModel.qtyRemain = oldModel.qtyAvail;
            if(oldModel.tags == null)
            {
                oldModel.tags = "";
            }
            if (ModelState.IsValid)
            {
                productModel newModel = viewToModel(oldModel);
                newModel.sellerID = User.Identity.GetUserId();
                newModel.dateListed = DateTime.Now;
                db.productmodel.Add(newModel);
                db.SaveChanges();
                if (newModel.images)
                {
                    return RedirectToAction("addPictures", "Product", new { productID = newModel.ID });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else if (oldModel.productName == null && oldModel.city == null && oldModel.price == null)
            {
                return View(oldModel);
            }
            else
            {
                return View(oldModel);
            }
        }
        public productModel viewToModel(createProductViewModel oldModel)
        {
            productModel newModel = new productModel();
            newModel.ID = oldModel.ID;
            newModel.address = oldModel.address;
            newModel.zipcode = oldModel.zipcode;
            newModel.city = oldModel.city;
            newModel.state = oldModel.state;
            newModel.images = oldModel.images;
            newModel.categoryID = oldModel.categoryID;
            newModel.productCategory = oldModel.category;
            newModel.price = oldModel.price;
            newModel.qtyAvail = oldModel.qtyAvail;
            newModel.qtyRemain = oldModel.qtyRemain;
            newModel.productName = oldModel.productName;
            newModel.unitQTY = oldModel.unitQty;
            newModel.ratings = oldModel.ratings;
            newModel.unitID = oldModel.unitID;
            newModel.sellerID = oldModel.sellerID;
            newModel.dateListed = oldModel.dateListed;
            newModel.tags = new List<string>();
            if(oldModel.tags != null)
            {
                newModel.tags = oldModel.tags.Split(',').ToList();
            }
            return newModel;
        }
        public createProductViewModel modelToView(productModel model)
        {
            createProductViewModel viewModel = new createProductViewModel();
            viewModel.ID = model.ID;
            viewModel.dateListed = model.dateListed;
            viewModel.address = model.address;
            viewModel.zipcode = model.zipcode;
            viewModel.city = model.city;
            viewModel.state = model.state;
            viewModel.images = model.images;
            viewModel.categoryID = model.categoryID;
            viewModel.category = model.productCategory;
            viewModel.price = model.price;
            viewModel.qtyAvail = model.qtyAvail;
            viewModel.qtyRemain = model.qtyRemain;
            viewModel.productName = model.productName;
            viewModel.unitQty = model.unitQTY;
            viewModel.ratings = model.ratings;
            viewModel.unitID = model.unitID;
            viewModel.sellerID = model.sellerID;
            if (model.tags != null)
            {
                foreach (string tag in model.tags) { if (tag == model.tags.Last()) { viewModel.tags += tag; } else { viewModel.tags += (tag + ','); } }
            }
            else
            {
                viewModel.tags = "";
            }
            return viewModel;
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productModel productModel = db.productmodel.Find(id);
            if (productModel.sellerID != User.Identity.GetUserId())
            {
                TempData["ErrorMessage"] = "You may not edit a product that is not your own.";
                return RedirectToAction("Index");
            }
            createProductViewModel vm = modelToView(productModel);
            vm.seller = db.Users.Find(productModel.sellerID);
            if (productModel.sellerID != User.Identity.GetUserId()) { ViewBag.StatusMessage = "You cannot edit a product that is not yours."; return RedirectToAction("Index"); }
            vm.unitList = new List<SelectListItem>();
            foreach (var unit in db.unitsModel.ToList())
            {
                vm.unitList.Add(new SelectListItem { Text = unit.unitAbvr, Value = unit.ID.ToString() });
            }
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(createProductViewModel viewModel)
        {
            productModel productmodel = viewToModel(viewModel);
            if (ModelState.IsValid)
            {
                db.Entry(productmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productModel productModel = db.productmodel.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productModel productModel = db.productmodel.Find(id);
            db.productmodel.Remove(productModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #region product list mods
        public ActionResult publicListing(int? catid)
        {
            if (catid != null)
            {
                return View(loadProductData(db.productmodel.Where(x => x.categoryID == catid).ToList()));
            }
            List<productModel> model = new List<productModel>();
            List<publicListingViewModel> viewModelList = new List<Models.publicListingViewModel>();
            var categories = db.productCategory.ToList().OrderBy(x => x.ID).ToList();
            model = db.productmodel.OrderBy(x => x.dateListed).Skip(0).Take(50).OrderBy(x => x.categoryID).ToList();
            foreach(var cat in categories)
            {
                publicListingViewModel viewModel = new publicListingViewModel();
                viewModel.productCat = new productCategory();
                viewModel.productCat = cat;
                viewModel.products = new List<productModel>();
                foreach (var item in model)
                {
                    if(item.categoryID == cat.ID)
                    {
                        item.sellerModel = db.Users.Find(item.sellerID);
                        viewModel.products.Add(item);
                    }
                }
                if(viewModel.products.Count() > 0) { viewModelList.Add(viewModel); }
            }
            return View(viewModelList);
        }
        #endregion product list mods

        #region product Partial Helpers
        
        public List<publicListingViewModel> loadProductData(List<productModel> productsIN)
        {
            List<publicListingViewModel> updatedList = new List<publicListingViewModel>();
            productCategory cat = db.productCategory.Find(productsIN[0].categoryID);
            foreach(productModel product in productsIN) { product.sellerModel = db.Users.Find(product.sellerID); }
            updatedList.Add(new publicListingViewModel { products = productsIN, productCat = cat});
            return updatedList;
        }
        public ActionResult _partialCategoryList()
        {
            List<productCatViewModel> viewModelList = new List<productCatViewModel>();
            productCatViewModel viewModel = new productCatViewModel();
            var prods = db.productmodel.ToList();
            foreach(var cat in db.productCategory.ToList())
            {
                viewModel = new productCatViewModel();
                viewModel.productCount = prods.Where(x => x.categoryID == cat.ID).Count();
                viewModel.productCat = cat; 
                viewModelList.Add(viewModel);
            }
            return PartialView(viewModelList);
        }
        public ActionResult _partialProductList()
        {
            //List<productDetailsViewModel> viewModelList = new List<productDetailsViewModel>();
            //productDetailsViewModel viewModel = new productDetailsViewModel();
            //List<productModel> products = db.productmodel.OrderBy(x => x.dateListed).Take(8).ToList();
            //var images = db.productImage.ToList();
            //foreach (var product in products)
            //{
            //    viewModel = new productDetailsViewModel();
            //    viewModel.productModel = new productModel();
            //    viewModel.productSeller = new ApplicationUser();
            //    viewModel.productImageCollectionModel = new List<productImage>();
            //    viewModel.productModel = product;
            //    viewModel.productModel.sellerModel = db.Users.Find(product.sellerID);
            //    viewModel.productModel.unitModel = db.unitsModel.Find(product.unitID);
            //    foreach (var image in images) { if(image.productID == product.ID) { viewModel.productImageCollectionModel.Add(image); } }
            //    viewModelList.Add(viewModel);
            //}
            
            //Create PartialProdPack
            homepageProductListPack prodListDataPack = new homepageProductListPack();
            ///new product list
            prodListDataPack.products = new List<productDetailsViewModel>();
            ///create usermodel and populate
            prodListDataPack.currentUserModel = new userProfileModel();
            prodListDataPack.currentUserModel = db.userprofilemodel.Find(User.Identity.GetUserId());
            ///create product detailsVM
            productDetailsViewModel viewModel = new productDetailsViewModel();
            //product data for product pack
            var prodModel = db.productmodel.OrderBy(x => x.dateListed).Take(8).ToList();
            var prodImages = db.productImage.ToList();
            var prodSellers = db.userprofilemodel.ToList();
            //populate product list with products, pics and sellers
            foreach (var prod in prodModel)
            {
                viewModel = new productDetailsViewModel();
                viewModel.productModel = prod;
                viewModel.productModel.unitModel = db.unitsModel.Find(prod.unitID);
                viewModel.productSeller = new ApplicationUser();
                viewModel.productSeller = db.Users.Find(prod.sellerID);
                //new image list
                viewModel.productImageCollectionModel = new List<productImage>();
                //populate w/ prod images
                foreach (var img in prodImages) { if (img.productID == prod.ID) { viewModel.productImageCollectionModel.Add(img); } }
                //add to product datapack
                prodListDataPack.products.Add(viewModel);
            }
            return PartialView(prodListDataPack);
        }
        public ActionResult _partialCategorySelect()
        {
            productCatSelectList selectListModel = new Models.productCatSelectList();
            selectListModel.cats = new List<SelectListItem>();
            var prods = db.productmodel.ToList();
            foreach (var cat in db.productCategory.ToList())
            {
                if(prods.Where(x => x.categoryID == cat.ID).Count() > 0)
                {
                    selectListModel.catID = cat.ID;
                    selectListModel.cats.Add(new SelectListItem { Text = cat.categoryName, Value = cat.ID.ToString(), Selected=false });
                }
            }
            return PartialView(selectListModel);
        }

        #endregion product Partial Helpers

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
