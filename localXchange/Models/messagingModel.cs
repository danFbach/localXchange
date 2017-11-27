using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace localXchange.Models
{
    public class messagingModel
    {
        public messagingModel()
        {
            important = false;
            readUnread = false;
            if (subject == null)
            {
                if (message != null)
                {
                    subject = String.Format(message).Substring(0, 75);
                }
                else
                {
                    subject = "";
                }
            }
        }
        [Key]
        public int messageID { get; set; }

        [Required]
        public string senderID { get; set; }
        public ApplicationUser sender { get; set; }

        [Required]
        public string receiverID { get; set; }
        public ApplicationUser receiver { get; set; }
        
        [StringLength(maximumLength: 150, ErrorMessage = "Subject must be at least {2} and no more than 150 characters in length.", MinimumLength = 5)]
        public string subject { get; set; }

        [Required]
        [StringLength(maximumLength: 2500, ErrorMessage = "Subject must be at least {2} and no more than 2500 characters in length.", MinimumLength = 1)]
        public string message { get; set; }

        [Display(Name = "Date/Time Sent")]
        public DateTime datetimeSent { get; set; }
        
        public bool important { get; set; }

        public bool readUnread { get; set; }
    }
    public class InOutBox
    {
        public List<messagingModel> messagesIN { get; set; }
        public List<messagingModel> messagesOUT { get; set; }
        public List<SelectListItem> addresses { get; set; }
        public string addressBookEntryUsername { get; set; }
    }
}