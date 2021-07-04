using Arex388.AspNet.Mvc.Startup;
using Microsoft.Extensions.DependencyInjection;
using Owin;
using System.Web;
using System.Web.Mvc;

[assembly: PreApplicationStartMethod(typeof(StartupApplication), "InitModule")]
namespace Arex388.AspNet.Mvc.Startup {
    public abstract class StartupApplication :
        HttpApplication {
        public static void InitModule() => RegisterModule(Constants.ServiceScopeModuleType);

        public void Configuration(
            IAppBuilder app) => Configure(app);

        public abstract void Configure(
            IAppBuilder app);

        public void ConfigureServices() {
            var services = new ServiceCollection();

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            ServiceScopeModule.SetServiceProvider(provider);

            var resolver = new ServiceProviderDependencyResolver();

            DependencyResolver.SetResolver(resolver);
        }

        public abstract void ConfigureServices(
            IServiceCollection services);
    }
}