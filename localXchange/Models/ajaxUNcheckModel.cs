using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using localXchange.Models;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;


namespace localXchange.Models
{

    public class ajaxUNcheckModel
    {
        public string username { get; set; }
        public bool isAvailable { get; set; }

    }
}
