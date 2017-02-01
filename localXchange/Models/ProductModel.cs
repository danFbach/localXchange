using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace localXchange.Models
{
    public class ProductModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public string sellerID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string productName { get; set; }

        [Display(Name = "Images")]
        public List<String> images { get; set; }

        [Required]
        [Display(Name = "Category")]
        public List<int> category { get; set; }

        [Display(Name = "Sub-Catugory")]
        public List<int> subCategory { get; set; }

        [Required]
        [Display(Name = "Quantity Available")]
        public decimal qtyAvail { get; set; }

        [Required]
        [Display(Name = "Remaining Quantity")]
        public decimal qtyRemain { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "City")]
        public string city { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public int zipcode { get; set; }
        
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Unit sold by")]
        public int unitSoldBy { get; set; }

        [Display(Name = "Quanity Sold By")]
        public decimal qtySoldBy { get; set; }

        [Required]
        [Display(Name = "Price per Unit")]
        public decimal price { get; set; }

        [Display(Name = "Product Rating")]
        public List<ratingModel> ratings { get; set; }


    }
}