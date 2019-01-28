using System.Net;
using System.Threading.Tasks;
using Arcus.EventGrid.Proxy.Tests.Integration.Services;
using Xunit;

namespace Arcus.EventGrid.Proxy.Tests.Integration.Endpoints.v1
{
    [Collection("Integration")]
    public class HealthEndpointTests : EndpointTest
    {
        private readonly HealthService _healthService;

        public HealthEndpointTests()
        {
            _healthService = new HealthService(Configuration);
        }

        [Fact]
        public async Task Health_Get_Succeeds()
        {
            // Act
            var response = await _healthService.GetAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}