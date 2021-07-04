using Arex388.AspNet.Mvc.Startup;
using Microsoft.Extensions.DependencyInjection;
using Owin;
using System;
using System.Web;
using System.Web.Mvc;

[assembly: PreApplicationStartMethod(typeof(StartupApplication), "InitModule")]
namespace Arex388.AspNet.Mvc.Startup {
    public abstract class StartupApplication :
        HttpApplication {
        public static void InitModule() => RegisterModule(Constants.ServiceScopeModuleType);

        public static IServiceProvider ServiceProvider { get; private set; }

        public void Configuration(
            IAppBuilder app) => Configure(app);

        public abstract void Configure(
            IAppBuilder app);

        public void ConfigureServices() {
            var services = new ServiceCollection();

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            ServiceProvider = provider;

            var resolver = new ServiceProviderDependencyResolver();

            DependencyResolver.SetResolver(resolver);
        }

        public abstract void ConfigureServices(
            IServiceCollection services);

        public static IServiceScope CreateScope(HttpContext context) {
            var scope = ServiceProvider.CreateScope();
            context.Items[Constants.ServiceScopeType] = scope;
            return scope;
        }

        internal static void DisposeScope(HttpContext context) {
            if (context.Items[Constants.ServiceScopeType] is IServiceScope scope) {
                scope.Dispose();
            }
        }
    }
}