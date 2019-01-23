using Arcus.EventGrid.Proxy.Api.Extensions;
using System;
using System.Linq;
using Arcus.EventGrid.Proxy.Api.Validation;
using Arcus.EventGrid.Publishing;
using Arcus.EventGrid.Publishing.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Arcus.EventGrid.Proxy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
                    jsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.UseOpenApiSpecifications();
            services.AddSingleton(BuildEventGridPublisher);

            ValidateConfiguration();
        }

        private void ValidateConfiguration()
        {
            var validationOutcomes = RuntimeValidator.Run(Configuration);

            if (validationOutcomes.Any(validationOutcome => validationOutcome.Successful == false))
            {
                throw new Exception("Unable to start up due to invalid configuration");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseOpenApiDocsWithExplorer();
        }

        private IEventGridPublisher BuildEventGridPublisher(IServiceProvider serviceProvider)
        {
            var rawTopicEndpoint = Configuration[EnvironmentVariables.Runtime.EventGrid.TopicEndpoint];
            var authenticationKey = Configuration[EnvironmentVariables.Runtime.EventGrid.AuthKey];
            var topicUri = new Uri(rawTopicEndpoint);
            return EventGridPublisherBuilder.ForTopic(topicUri)
                .UsingAuthenticationKey(authenticationKey)
                .Build();
        }
    }
}