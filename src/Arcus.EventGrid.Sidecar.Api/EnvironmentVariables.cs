namespace Arcus.EventGrid.Sidecar.Api
{
    public class EnvironmentVariables
    {
        public class Runtime
        {
            public class Ports
            {
                public const string Http = "ARCUS_HTTP_PORT";
            }

            public class EventGrid
            {
                public const string TopicEndpoint = "ARCUS_EVENTGRID_TOPICENDPOINT";
                public const string AuthKey = "ARCUS_EVENTGRID_AUTHKEY";
            }
        }
    }
}