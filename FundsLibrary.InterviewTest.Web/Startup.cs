using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FundsLibrary.InterviewTest.Web.Startup))]
namespace FundsLibrary.InterviewTest.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
