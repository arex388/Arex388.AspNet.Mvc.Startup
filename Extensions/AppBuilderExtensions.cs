using Arex388.AspNet.Mvc.Startup;
using System;

namespace Owin {
    public static class AppBuilderExtensions {
        public static IServiceProvider GetServiceProvider(
            this IAppBuilder app) => (IServiceProvider)app.Properties[nameof(StartupApplication.ServiceProvider)];
    }
}