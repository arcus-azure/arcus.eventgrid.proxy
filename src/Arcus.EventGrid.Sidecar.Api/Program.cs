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
            var httpPort = GetConfiguredPortOrDefault(EnvironmentVariables.Runtime.HttpPort, 80);
            var httpsPort = GetConfiguredPortOrDefault(EnvironmentVariables.Runtime.HttpsPort, 443);
            var httpEndpointUrl = $"http://+:{httpPort}";
            var httpsEndpointUrl = $"https://+:{httpsPort}";

            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel(kestrelServerOptions =>
                {
                    kestrelServerOptions.AddServerHeader = false;
                })
                .UseUrls(httpEndpointUrl, httpsEndpointUrl)
                .UseStartup<Startup>()
                .Build();
        }

        private static int GetConfiguredPortOrDefault(string environmentVariableName, int defaultPort)
        {
            var rawConfiguredHttpPort = Environment.GetEnvironmentVariable(environmentVariableName);
            if (int.TryParse(rawConfiguredHttpPort, out int configuredHttpPort))
            {
                return configuredHttpPort;
            }

            return defaultPort;
        }
    }
}
