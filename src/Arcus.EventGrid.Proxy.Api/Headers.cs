namespace Arcus.EventGrid.Proxy.Api
{
    public class Headers
    {
        public class Response
        {
            public class Events
            {
                public const string Id = "X-Event-Id";
                public const string Subject = "X-Event-Subject";
                public const string Timestamp = "X-Event-Timestamp";
                public const string DataVersion = "X-Event-Data-Version";
            }
        }
    }
}