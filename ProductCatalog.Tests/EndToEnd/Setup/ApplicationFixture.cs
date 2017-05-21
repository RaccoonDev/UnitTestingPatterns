using ProductCatalog.Web;
using System;

namespace ProductCatalog.Tests.EndToEnd.Setup
{
    public class ApplicationFixture : IDisposable
    {
        private readonly OwinService _owinService;
        public ApplicationFixture()
        {
            _owinService = new OwinService();
            _owinService.Start();
        }

        public string ListeningEndpoint => _owinService.ListeningEndpoint;

        public void Dispose()
        {
            _owinService.Stop();
        }
    }
}
