using System.Net.Http;

namespace Arcus.EventGrid.Sidecar.Tests.Integration.Services
{
    public class Service
    {
        protected HttpClient HttpClient { get; } = new HttpClient();
        protected string BaseUrl { get; } = "http://localhost:888/api/v1";
    }
}