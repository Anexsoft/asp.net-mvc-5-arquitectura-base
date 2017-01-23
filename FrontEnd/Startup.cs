using Microsoft.Owin;
using Owin;
using Service.Config;

[assembly: OwinStartupAttribute(typeof(FrontEnd.Startup))]
namespace FrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DependecyRegistration();
        }

        public void DependecyRegistration()
        {
            var container = new LightInject.ServiceContainer();
            container.RegisterAssembly("Service.dll");
        }
    }
}
