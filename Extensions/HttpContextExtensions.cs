using Microsoft.Extensions.DependencyInjection;
using System.Web;

namespace Arex388.AspNet.Mvc.Startup {
	internal static class HttpContextExtensions {
		public static IServiceScope GetServiceScope(
			this HttpContext context) => context?.Items[Constants.ServiceScopeType] as IServiceScope;
	}
}