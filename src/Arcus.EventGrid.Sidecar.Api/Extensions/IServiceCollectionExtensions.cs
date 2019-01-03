using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        ///     Use OpenAPI specification
        /// </summary>
        /// <param name="services">Collections of services in application</param>
        public static void UseOpenApiSpecifications(this IServiceCollection services)
        {
            var openApiInformation = new Info
            {
                Contact = new Contact
                {
                    Name = "Arcus",
                    Url = "https://github.com/arcus-azure/arcus.eventgrid.sidecar"
                },
                Title = "Arcus",
                Description = "Push events to Azure Event Grid in a breeze - API allows you to send events over REST without having to worry about how Azure Event Grid works.",
                License = new License
                {
                    Name = "MIT",
                    Url = "https://github.com/arcus-azure/arcus.eventgrid.sidecar/blob/master/LICENSE"
                }
            };

            var xmlDocumentationPath = GetXmlDocumentationPath(services);

            services.AddSwaggerGen(swaggerGenerationOptions =>
            {
                swaggerGenerationOptions.EnableAnnotations();
                swaggerGenerationOptions.SwaggerDoc("v1", openApiInformation);
                swaggerGenerationOptions.DescribeAllEnumsAsStrings();

                if (string.IsNullOrEmpty(xmlDocumentationPath) == false)
                {
                    swaggerGenerationOptions.IncludeXmlComments(xmlDocumentationPath);
                }
            });
        }

        private static string GetXmlDocumentationPath(IServiceCollection services)
        {
            var hostingEnvironment = services.FirstOrDefault(service => service.ServiceType == typeof(IHostingEnvironment));
            if (hostingEnvironment == null)
            {
                return string.Empty;
            }

            var contentRootPath = ((IHostingEnvironment)hostingEnvironment.ImplementationInstance).ContentRootPath;
            var xmlDocumentationPath = $"{contentRootPath}/Open-Api-Docs.xml";

            return File.Exists(xmlDocumentationPath) ? xmlDocumentationPath : string.Empty;
        }
    }
}
