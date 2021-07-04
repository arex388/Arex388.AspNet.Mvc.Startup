using System;
using System.Web;

namespace Arex388.AspNet.Mvc.Startup {
    internal sealed class ServiceScopeModule :
        IHttpModule {

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

            StartupApplication.CreateScope(context);
        }

        private static void OnContextEndRequest(
            object sender,
            EventArgs e) {
            var context = sender.ToHttpContext();

            StartupApplication.DisposeScope(context);
        }
    }
}