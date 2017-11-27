using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localXchange.Models
{
    public class addressBookModel
    {
        public int ID { get; set; }
        
        public string bookOwnerUserID { get; set; }
        public ApplicationUser bookOwnerUser { get; set; }

        public string bookEntryUserID { get; set; }
        public ApplicationUser bookEntryUser { get; set; }
    }

    public class addressBookViewModel
    {
        public List<addressBookModel> addresses { get; set; }
    }
}