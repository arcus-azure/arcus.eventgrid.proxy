using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Arcus.EventGrid.Proxy.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Add support for Open API documentation with API explorer
        /// </summary>
        /// <param name="applicationBuilder">Application Builder</param>
        public static void UseOpenApiDocsWithExplorer(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger(swaggerOptions =>
            {
                swaggerOptions.RouteTemplate = "api/{documentName}/docs.json";
            });
            applicationBuilder.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.SwaggerEndpoint("/api/v1/docs.json", "Arcus Event Grid Proxy v1 API");
                swaggerUiOptions.RoutePrefix = "api/docs";
                swaggerUiOptions.DisplayOperationId();
                swaggerUiOptions.EnableDeepLinking();
                swaggerUiOptions.DocumentTitle = "Arcus Event Grid Proxy API";
                swaggerUiOptions.DocExpansion(DocExpansion.List);
                swaggerUiOptions.DisplayRequestDuration();
                swaggerUiOptions.EnableFilter();
            });
        }
    }
}
