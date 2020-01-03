using Microsoft.Extensions.DependencyInjection;
using System;
using System.Web;

namespace Arex388.AspNet.Mvc.Startup {
	internal sealed class ServiceScopeModule :
		IHttpModule {
		private static ServiceProvider _serviceProvider;

		public static void SetServiceProvider(
			ServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

		#region IHttpModule
		public void Dispose() {
		}

		public void Init(
			HttpApplication context) {
			context.BeginRequest += OnContextBeginRequest;
			context.EndRequest += OnContextEndRequest;
		}
		#endregion

		private static void OnContextBeginRequest(
			object sender,
			EventArgs e) {
			var context = sender.ToHttpContext();

			context.Items[Constants.ServiceScopeType] = _serviceProvider.CreateScope();
		}

		private static void OnContextEndRequest(
			object sender,
			EventArgs e) {
			var context = sender.ToHttpContext();

			if (context.Items[Constants.ServiceScopeType] is IServiceScope scope) {
				scope.Dispose();
			}
		}
	}
}