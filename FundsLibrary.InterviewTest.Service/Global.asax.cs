using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace FundsLibrary.InterviewTest.Service
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new WindsorHttpControllerActivator(container));
        }
    }
}
