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
            public class Ports
            {
                public const string Http = "ARCUS_HTTP_PORT";
                public const string Https = "ARCUS_HTTPS_PORT";
            }
        }
    }
}
