using System.Web.Http;

namespace ProductCatalog.Web.Controllers
{
    [Route("hello")]
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }
    }
}
