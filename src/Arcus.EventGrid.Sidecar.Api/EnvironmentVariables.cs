using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arcus.EventGrid.Sidecar.Api
{
    public class EnvironmentVariables
    {
        public class Runtime
        {
            public const string HttpPort = "ARCUS_HTTP_PORT";
            public const string HttpsPort = "ARCUS_HTTP_PORTS";
        }
    }
}
