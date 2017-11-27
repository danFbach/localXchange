using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using localXchange.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace localXchange.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }
        ApplicationDbContext db = new ApplicationDbContext();
        #region UTILS
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public bool IsAuthenticated
        {
            get
            {
                return this.User != null &&
                       this.User.Identity != null &&
                       this.User.Identity.IsAuthenticated;
            }
        }
        #endregion UTILS
        public ActionResult Index()
        {
            //create main ViewModel
            homepageViewModel vm = new Models.homepageViewModel();
            vm.loginViewModel = new LoginViewModel();
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "The public market, at your fingertips.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult partialLogInForm()
        {
            homepageViewModel viewModel = new homepageViewModel();
            return PartialView(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> partialLogInForm(homepageViewModel viewModel, string ReturnUrl)
        {
            LoginViewModel model = viewModel.loginViewModel;
            if (String.IsNullOrEmpty(ReturnUrl) || String.IsNullOrWhiteSpace(ReturnUrl))
            {
                return PartialView(viewModel);
            }
            string loginName = await getLoginUserName(viewModel.loginViewModel);
            var result = await SignInManager.PasswordSignInAsync(loginName, model.Password, model.RememberMe, shouldLockout: false);
            if (result == SignInStatus.Success)
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                ViewBag.ErrorMessage = "Login Error.";
                return Redirect(ReturnUrl);
            }
        }
        public async Task<string> getLoginUserName(LoginViewModel user)
        {
            var userBool = await UserManager.FindByNameAsync(user.username);
            if (userBool == null)
            {
                ApplicationUser newuser = await UserManager.FindByEmailAsync(user.username);
                return newuser.UserName;
            }
            else
            {
                return user.username;
            }
        }
    }
}