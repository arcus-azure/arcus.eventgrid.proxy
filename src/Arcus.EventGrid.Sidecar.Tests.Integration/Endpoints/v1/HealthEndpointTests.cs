using System.Net;
using System.Threading.Tasks;
using Arcus.EventGrid.Sidecar.Tests.Integration.Services;
using Xunit;

namespace Arcus.EventGrid.Sidecar.Tests.Integration.Endpoints.v1
{
    [Collection("Integration")]
    public class HealthEndpointTests
    {
        private readonly HealthService healthService = new HealthService();

        [Fact]
        public async Task Health_Get_Succeeds()
        {
            // Act
            var response = await healthService.GetAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}