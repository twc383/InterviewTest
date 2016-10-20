using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using System.Security.Principal;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserModelRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public UserController()
            : this(new UserModelRepository())
        { }

        public UserController(IUserModelRepository repository)
        {
            _repository = repository;
        }

        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            var result = await _repository.GetAll();
            var user = result.FirstOrDefault(a => a.Username == userLogin.Username);
            if (user != null && user.Username == userLogin.Username && user.Password == userLogin.Password)
            {
                user.isLoggedIn = true;
                Session.Add("LoggedUserGuid", user.Id);
                Session.Add("LoggedUsername", user.Username);
                return RedirectToAction("AfterLogin");
            }

            ModelState.AddModelError("Error", "The username or password entered is not correct");
            return View(userLogin);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LoggedUsername"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserModel newUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.Post(newUser);
                return RedirectToAction("Confirmation", "User", new { id = result });
            }
            return View(newUser);
        }

        public async Task<ActionResult> Confirmation(Guid id)
        {
            var result = await _repository.Get(id);
            return View(result);
        }

        public async Task<ActionResult> UserDetails()
        {
            return View(await _repository.GetAll());
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "FundManager");
        }
    }
}