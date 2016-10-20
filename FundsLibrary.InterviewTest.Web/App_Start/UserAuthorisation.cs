using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FundsLibrary.InterviewTest.Web.App_Start
{
    public class UserAuthorisation : AuthorizeAttribute
    {
        public string Role { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            base.OnAuthorization(filterContext);

            if (!filterContext.HttpContext.User.IsInRole(Role))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}