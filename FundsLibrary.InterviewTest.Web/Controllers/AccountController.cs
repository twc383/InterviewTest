using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<User, Guid> _signInManager;
        private readonly UserManager<User, Guid> _userManager;

        public AccountController(FundsLibrarySignInManager signInManager, FundsLibraryUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return _RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult _RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName, EmailAddress = model.EmailAddress };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            var owinContext = HttpContext.GetOwinContext();
            owinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}
