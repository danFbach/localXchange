using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localXchange.Models
{
    public class userProfileModel
    {
        [Key]
        public string ID { get; set; }
        
        public string UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string fName { get; set; }
        
        [Display(Name = "Middel Initial")]
        public string MInitial { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lName { get; set; }

        //[Required]
        //[Display(Name = "Country")]
        //public string country { get; set; }

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

        [Display(Name = "")]
        public List<ratingModel> sellerRep { get; set; }

        [Display(Name = "")]
        public List<ratingModel> buyerRep { get; set; }
    }
}