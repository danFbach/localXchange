using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localXchange.Models
{
    public class locationModel
    {
        [Key]
        public int ID { get; set; }

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
}