using System.Web;
using System.Web.Http;

namespace FundsLibrary.InterviewTest.Service
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
