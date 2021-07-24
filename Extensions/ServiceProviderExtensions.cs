using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Arex388.AspNet.Mvc.Startup {
    public static class ServiceProviderExtensions {
        public static IServiceCollection AddControllers(
            this IServiceCollection services,
            Assembly assembly) {

            ServiceProviderDependencyResolver.EnsureConfigured();

            var type = typeof(IController);

            var controllers = assembly.GetExportedTypes().Where(
                t =>
                    !t.IsAbstract
                    && !t.IsGenericTypeDefinition).Where(
                t =>
                    type.IsAssignableFrom(t)
                    || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));

            foreach (var controller in controllers) {
                services.AddTransient(controller);
            }

            return services;
        }
    }
}