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

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "First name may not be more than {0} characters.", MinimumLength = 0)]
        public string fName { get; set; }
        
        [Display(Name = "Middel Initial")]
        [StringLength(1,ErrorMessage = "An Initial is {0} character.", MinimumLength = 0)]
        public string MInitial { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(35, ErrorMessage = "Last name may not be more than {0} characters.", MinimumLength = 0)]
        public string lName { get; set; }

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

        [Display(Name = "")]
        public List<ratingModel> sellerRep { get; set; }

        [Display(Name = "")]
        public List<ratingModel> buyerRep { get; set; }

        [Phone]
        [Display(Name = "Phone Number (only shown if allowed)")]
        public string phoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Contact Email (only shown if allowed)")]
        public string contactEmail { get; set; }

        [Display(Name = "Send Private messages to contact email.")]
        public bool messagesToEmail { get; set; }
    }
}   