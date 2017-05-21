using Microsoft.Owin.Hosting;
using System;

namespace ProductCatalog.Web
{
    public class OwinService
    {
        private IDisposable _webApp;
        public string ListeningEndpoint => "http://localhost:9000";
        public void Start()
        {
            _webApp = WebApp.Start<StartOwin>(ListeningEndpoint);
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }
}
