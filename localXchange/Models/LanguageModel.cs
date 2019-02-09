using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace localXchange.Models
{
    public class LanguageModel
    {

    }

    public class languageNationalities
    {
        [Key]
        public int id { get; set; }

        [Display(Name ="langFullName")]
        public string language { get; set; }

        [StringLength(maximumLength: 2, ErrorMessage = "exactly2Chars",MinimumLength = 2)]
        [Display(Name ="langAbbr")]
        public string lanAbbr { get; set; }

    }
    public class langLabels
    {
        [Key]
        public int id { get; set; }

        public int langId { get; set; }
        public languageNationalities language { get; set; }

        [Display(Name = "langDisplayTextTitle")]
        public string langDisplayTextTitle { get; set; }

        [Display(Name ="langDisplayTextValue")]
        public string langDisplayTextValue { get; set; }
    }

    public class langContent
    {
        [Key]
        public int id { get; set; }

        public int langId { get; set; }
        public languageNationalities language { get; set; }

        [Display(Name = "langContentTitle")]
        public string langContentTitle { get; set; }

        [Display(Name = "langContentValue")]
        public string langContentValue { get; set; }

    }
}