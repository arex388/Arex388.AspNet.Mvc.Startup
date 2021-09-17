using Microsoft.Extensions.DependencyInjection;
using System;

namespace Arex388.AspNet.Mvc.Startup {
    internal static class Statics {
        public static readonly string ServiceProviderKey = typeof(IServiceProvider).FullName;
        public static readonly Type ServiceScopeType = typeof(IServiceScope);
        public static readonly Type ServiceScopeModuleType = typeof(ServiceScopeModule);
    }
}