using Arex388.AspNet.Mvc.Startup;
using Microsoft.Extensions.DependencyInjection;
using Owin;
using System;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(StartupApplication), nameof(StartupApplication.InitModule))]
namespace Arex388.AspNet.Mvc.Startup {
    public abstract class StartupApplication :
        HttpApplication {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void InitModule() => RegisterModule(Constants.ServiceScopeModuleType);

        public void Configuration(
            IAppBuilder app) {
            BuildServiceProvider();
            Configure(app);
        }

        public abstract void Configure(
            IAppBuilder app);

        [Obsolete("ServiceProvider is now configured before Configure().")]
        public void ConfigureServices() => BuildServiceProvider();

        protected virtual void BuildServiceProvider() {
            if (ServiceProvider != null) {
                return;
            }

            var services = new ServiceCollection();

            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            ServiceProvider = provider;
        }

        public abstract void ConfigureServices(
            IServiceCollection services);

        public static IServiceScope CreateScope(
            HttpContext context) {
            var scope = ServiceProvider.CreateScope();

            context.Items[Constants.ServiceScopeType] = scope;

            return scope;
        }

        internal static void DisposeScope(
            HttpContext context) {
            if (context.Items[Constants.ServiceScopeType] is IServiceScope scope) {
                scope.Dispose();
            }
        }
    }
}