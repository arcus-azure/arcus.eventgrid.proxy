using Microsoft.Extensions.Configuration;

namespace Arcus.EventGrid.Proxy.Tests.Integration.Endpoints
{
    public class EndpointTest
    {
        protected IConfiguration Configuration { get; }

        public EndpointTest()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: true)
                .AddJsonFile(path: "appsettings.local.json", optional: true)
                .AddJsonFile(path: "appsettings.private.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}