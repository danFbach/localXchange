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
        public ApplicationUser user { get; set; }

        public int accountTypeId { get; set; }
        public accountType accountType { get; set; }

        public int languageId { get; set; }
        public languageNationalities languageNationalities { get; set; }

        [Required]
        [Display(Name = "displayText_First Name")]
        [StringLength(25, ErrorMessage = "First name may not be more than {0} characters.", MinimumLength = 0)]
        public string fName { get; set; }
        
        [Display(Name = "displayText_Middel Initial")]
        [StringLength(1,ErrorMessage = "An Initial is {0} character.", MinimumLength = 0)]
        public string MInitial { get; set; }

        [Required]
        [Display(Name = "displayText_Last Name")]
        [StringLength(35, ErrorMessage = "Last name may not be more than {0} characters.", MinimumLength = 0)]
        public string lName { get; set; }

        [Required]
        [Display(Name = "displayText_State")]
        public string state { get; set; }

        [Required]
        [Display(Name = "displayText_City")]
        [StringLength(50, ErrorMessage = "City may not be more than {0} characters.", MinimumLength = 0)]
        public string city { get; set; }

        [Required]
        [Display(Name = "displayText_zip")] //Zip Code
        public int zipcode { get; set; }

        [Display(Name = "displayText_stAddr")] ///Street Address
        [StringLength(50, ErrorMessage = "Address may not be more than {0} characters.", MinimumLength = 0)]
        public string address { get; set; }

        [Display(Name = "displayText_sRep")] //User reputation as seller
        public List<ratingModel> sellerRep { get; set; }

        [Display(Name = "displayText_bRep")] //User reputation as buyer
        public List<ratingModel> buyerRep { get; set; }

        [Phone]
        [Display(Name = "displayText_phone")] ///Phone Number (only shown if allowed)
        public string phoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "displayText_email")] //Contact Email (only shown if allowed)
        public string contactEmail { get; set; }

        [Display(Name = "displayText_msgForwardToEmail")] //Send Private messages to contact email.
        public bool messagesToEmail { get; set; }

    }

    public class accountType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="displayText_AccountType")]
        public string typeName { get; set; }
    }

    public class subscriptionPlan
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="displayText_SubscrpLevel")]
        public string subscriptionLevel { get; set; }

        [Display(Name = "displayText_AbbrName")]
        public string abbreviatedName { get; set; }

        [Display(Name = "displayText_SubscrpListPrice")]
        public decimal listPrice { get; set; }
    }
}   