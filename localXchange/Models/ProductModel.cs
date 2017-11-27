using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace localXchange.Models
{
    public class productModel
    {
        [Required]
        public int ID { get; set; }

        [Display(Name = "Date of Listing")]
        public DateTime dateListed { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public string sellerID { get; set; }
        public ApplicationUser sellerModel { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Display(Name = "Images")]
        public bool images { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int categoryID { get; set; }
        public productCategory productCategory { get; set; }
        
        [Required]
        [Display(Name = "Sold by Unit")]
        public int unitID { get; set; }
        public unitsModel unitModel { get; set; }

        [Display(Name = "Quanity Per Unit")]
        public decimal? unitQTY { get; set; }

        [Required]
        [Display(Name = "Units Available")]
        public decimal? qtyAvail { get; set; }

        [Required]
        [Display(Name = "Remaining Quantity")]
        public decimal? qtyRemain { get; set; }

        [Required]
        [Display(Name = "Price per Unit")]
        public decimal? price { get; set; }

        [Display(Name = "Search Tags, seperated by comma ','")]
        public List<string> tags { get; set; }

        [Display(Name = "Product Rating")]
        public List<ratingModel> ratings { get; set; }
        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "City may not be more than {0} characters.", MinimumLength = 0)]
        public string city { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public int zipcode { get; set; }
        
        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Address may not be more than {0} characters.", MinimumLength = 0)]
        public string address { get; set; }

    }
    public class createProductViewModel
    {
        [Required]
        public int ID { get; set; }
        
        [Display(Name = "Seller")]
        public string sellerID { get; set; }
        public ApplicationUser seller { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Display(Name = "Date of Listing")]
        public DateTime dateListed { get; set; }

        [Display(Name = "Images")]
        public bool images { get; set; }
        
        [Display(Name = "Category")]
        public int categoryID { get; set; }
        public productCategory category { get; set; }
        public List<SelectListItem> categories { get; set; }

        [Required]
        [Display(Name = "Sold by Unit")]
        public int unitID { get; set; }
        public unitsModel unit { get; set; }
        public List<SelectListItem> unitList { get; set; }

        [Display(Name = "Quanity Per Unit")]    
        public decimal? unitQty { get; set; }
        [Required]
        [Display(Name = "Units Available")]
        public decimal? qtyAvail { get; set; }
        
        [Display(Name = "Remaining Quantity")]
        public decimal? qtyRemain { get; set; }

        [Required]
        [Display(Name = "Price per Unit")]
        public decimal? price { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "City may not be more than {0} characters.", MinimumLength = 0)]
        public string city { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public int zipcode { get; set; }
        
        [Display(Name = "Address")]
        [StringLength(50, ErrorMessage = "Address may not be more than {0} characters.", MinimumLength = 0)]
        public string address { get; set; }

        [Display(Name = "Product Rating")]
        public List<ratingModel> ratings { get; set; }

        [Display(Name = "Search Tags, seperated by comma ','")]
        public string tags { get; set; }

    }
    public class productDetailsViewModel
    {
        public productModel productModel { get; set; }
        public ApplicationUser productSeller { get; set; }
        public List<productImage> productImageCollectionModel { get; set; }
    }
    public class productImage
    {
        [Key]
        public int ID { get; set; }
        public string fileName { get; set; }
        public string relativePath { get; set; }

        [Required]
        public int productID { get; set; }
    }
    public class unitsModel
    {
        [Key]
        public int ID { get; set; }

        public string unitName { get; set; }

        public string unitAbvr { get; set; }

        public string unitType { get; set; }
    }
    public class addProductImagesModel
    {
        public int productID { get; set; }
        public IEnumerable<HttpPostedFileBase> files { get; set; }
    }
    public class addProductImagesViewModel
    {
        public productModel product { get; set; }
        public productImage image { get; set; }
    }
    public class productCategory
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(25, ErrorMessage = "Category name must be at least 4 and no more than 25 characters in length.", MinimumLength = 4)]
        public string categoryName { get; set; }
    }
    public class productCatViewModel
    {
        public productCategory productCat { get; set; }
        public int productCount { get; set; }
    }
    public class productCatSelectList
    {
        public List<SelectListItem> cats { get; set; }
        public int catID { get; set; }
    }
    public class publicListingViewModel
    {
        public productCategory productCat { get; set; }
        public List<productModel> products { get; set; }
    }

}