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

            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        private static void Welcome()
        {
            Console.WriteLine(WelcomeText);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
