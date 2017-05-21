using Xunit;

namespace ProductCatalog.Tests.EndToEnd.Setup
{
    [CollectionDefinition("Web API End to End Tests")]
    public class ApplicationWebApiEndToEndTestCollectionDefinition : ICollectionFixture<ApplicationFixture>
    {
    }
}
