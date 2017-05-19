namespace FrontEnd.App_Start
{
    public class DependecyConfig
    {
        public static void Initialize()
        {
            var container = new LightInject.ServiceContainer();
            container.RegisterAssembly("Service.dll");
        }
    }
}