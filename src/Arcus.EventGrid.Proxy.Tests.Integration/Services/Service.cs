using System.Net.Http;
using GuardNet;
using Microsoft.Extensions.Configuration;

namespace Arcus.EventGrid.Proxy.Tests.Integration.Services
{
    public class Service
    {
        private readonly IConfiguration _configuration;

        protected Service(IConfiguration configuration)
        {
            Guard.NotNull(configuration, nameof(configuration));

            _configuration = configuration;

            BaseUrl = _configuration.GetValue<string>("Arcus:Api:BaseUrl");
        }

        protected HttpClient HttpClient { get; } = new HttpClient();
        protected string BaseUrl { get; }
    }
}