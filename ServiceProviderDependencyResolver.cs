using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Arex388.AspNet.Mvc.Startup {
    internal sealed class ServiceProviderDependencyResolver :
        IDependencyResolver {
        public object GetService(
            Type serviceType) {
            var scope = HttpContext.Current.GetServiceScope();

            if (scope is null) {
                throw new InvalidOperationException("IServiceScope was not provided. If this method is being called directly or indirectly from an async sequence of methods that use ConfigureAwait(false), then you're probably loosing the HttpContext.Current due to the thread switch.");
            }

            return scope.ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(
            Type serviceType) {
            var scope = HttpContext.Current.GetServiceScope();

            if (scope is null) {
                throw new InvalidOperationException("IServiceScope was not provided. If this method is being called directly or indirectly from an async sequence of methods that use ConfigureAwait(false), then you're probably loosing the HttpContext.Current due to the thread switch.");
            }

            return scope.ServiceProvider.GetServices(serviceType);
        }

        internal static void EnsureConfigured() {
            if (DependencyResolver.Current is ServiceProviderDependencyResolver) {
                return;
            }

            var resolver = new ServiceProviderDependencyResolver();

            DependencyResolver.SetResolver(resolver);
        }
    }
}