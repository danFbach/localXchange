using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localXchange.Models
{
    public class ratingModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name ="User")]
        public string userID { get; set; }

        [Required]
        [Display(Name ="Rating")]
        public decimal rating { get; set; }

        [Display(Name ="Rating Description")]
        public string description { get; set; }
    }
}