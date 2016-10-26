using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FundsLibrary.InterviewTest.Web.Installers
{
    public class ManagersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Manager"))
                .LifestylePerWebRequest());
        }
    }
}