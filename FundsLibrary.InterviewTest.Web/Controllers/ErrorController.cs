using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Index(string errorMessage)
        {
            ViewBag.errorMessage = errorMessage;
            return View();

        }
    }
}