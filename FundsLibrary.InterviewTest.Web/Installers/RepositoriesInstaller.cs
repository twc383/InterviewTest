using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FundsLibrary.InterviewTest.Web.Repositories;

namespace FundsLibrary.InterviewTest.Web.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Repository"))
                .WithService.AllInterfaces());

            container.Register(Component
                .For<IHttpClientWrapper>()
                .UsingFactoryMethod(_ => new HttpClientWrapper("http://localhost:50135/Service/")));
        }
    }
}
