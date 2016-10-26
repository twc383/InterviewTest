using System;
using System.Web;
using FundsLibrary.InterviewTest.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FundsLibrary.InterviewTest.Web.Services
{
    public class FundsLibrarySignInManager : SignInManager<User, Guid>
    {
        public FundsLibrarySignInManager(IUserStore<User, Guid> userStore)
            : base(new UserManager<User, Guid>(userStore), HttpContext.Current.GetOwinContext().Authentication)
        {}
    }
}