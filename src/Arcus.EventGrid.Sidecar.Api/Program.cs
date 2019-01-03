using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Arcus.EventGrid.Sidecar.Api
{
    public class Program
    {
        private const string WelcomeText = @"
 █████╗ ██████╗  ██████╗██╗   ██╗███████╗
██╔══██╗██╔══██╗██╔════╝██║   ██║██╔════╝
███████║██████╔╝██║     ██║   ██║███████╗
██╔══██║██╔══██╗██║     ██║   ██║╚════██║
██║  ██║██║  ██║╚██████╗╚██████╔╝███████║
╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚══════╝";

        public static void Main(string[] args)
        {
            Welcome();

            BuildWebHost(args)
                .Run();
        }

        private static void Welcome()
        {
            Console.WriteLine(WelcomeText);
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var httpPort = DetermineHttpPort();
            var httpEndpointUrl = $"http://+:{httpPort}";

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel(kestrelServerOptions =>
                {
                    kestrelServerOptions.AddServerHeader = false;
                })
                .UseUrls(httpEndpointUrl)
                .UseStartup<Startup>()
                .Build();
        }

        private static int DetermineHttpPort()
        {
            var rawConfiguredHttpPort = Environment.GetEnvironmentVariable(EnvironmentVariables.Runtime.Ports.Http);
            if (int.TryParse(rawConfiguredHttpPort, out int configuredHttpPort))
            {
                return configuredHttpPort;
            }

            return 80;
        }
    }
}
