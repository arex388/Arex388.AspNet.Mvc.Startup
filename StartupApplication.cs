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

        public static void InitModule() => RegisterModule(Statics.ServiceScopeModuleType);

        public void Configuration(
            IAppBuilder app) {
            BuildServiceProvider();

            app.Properties.Add(Statics.ServiceProviderKey, ServiceProvider);

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

            ServiceProvider = services.BuildServiceProvider();
        }

        public abstract void ConfigureServices(
            IServiceCollection services);

        public static IServiceScope CreateScope(
            HttpContext context) {
            var scope = ServiceProvider.CreateScope();

            context.Items[Statics.ServiceScopeType] = scope;

            return scope;
        }

        internal static void DisposeScope(
            HttpContext context) {
            if (context.Items[Statics.ServiceScopeType] is IServiceScope scope) {
                scope.Dispose();
            }
        }
    }
}