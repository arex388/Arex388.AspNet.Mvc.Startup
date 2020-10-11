using System.Web;

namespace Arex388.AspNet.Mvc.Startup.Extensions {
    internal static class ObjectExtensions {
		public static HttpContext ToHttpContext(
			this object obj) => ((HttpApplication)obj).Context;
	}
}