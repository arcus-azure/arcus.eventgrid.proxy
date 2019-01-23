using System.Net.Http;
using System.Threading.Tasks;
using Flurl;

namespace Arcus.EventGrid.Proxy.Tests.Integration.Services
{
    public class HealthService : Service
    {
        public async Task<HttpResponseMessage> GetAsync()
        {
            var url = BaseUrl.AppendPathSegments("health");

            var response = await HttpClient.GetAsync(url);
            return response;
        }
    }
}