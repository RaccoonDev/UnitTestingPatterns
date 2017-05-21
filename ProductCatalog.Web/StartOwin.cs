using Autofac;
using Newtonsoft.Json.Serialization;
using Owin;
using ProductCatalog.Business;
using ProductCatalog.DataAccess;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace ProductCatalog.Web
{
    public class StartOwin
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            // Routes configuration
            config.MapHttpAttributeRoutes();

            // Serialization configuration
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Container configuration
            IContainer container = ConfigureContainer(config);

            // OWIN Pipeline configuration
            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        public IContainer ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterApiControllers(typeof(StartOwin).Assembly);
            builder.RegisterWebApiFilterProvider(config);

            // Run other optional steps, like registering model binders,
            // web abstractions, etc., then set the dependency resolver
            // to be Autofac.
            builder.RegisterType<ProductCatalogManager>().As<ProductCatalogManager>();
            builder.RegisterType<ElasticsearchProductRepository>().As<IProductsRepository>()
                .WithParameter("elasticsearchEndpoint", "http://localhost:9200");
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }
    }
}
