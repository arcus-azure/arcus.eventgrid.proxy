using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Arcus.EventGrid.Sidecar.Api;
using Flurl;
using Newtonsoft.Json;

namespace Arcus.EventGrid.Sidecar.Tests.Integration.Services
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