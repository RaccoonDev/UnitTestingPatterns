using Topshelf;

namespace ProductCatalog.Web
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x => {
                x.Service<OwinService>(s =>
                {
                    s.ConstructUsing(() => new OwinService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });
            });
        }
    }
}
