using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VersioningAPI
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            // Add Swagger Documentation for each API Version
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "My .Net API Restful",
                Description = "This is my first API Versioning Control",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact()
                {
                    Email = "wencitera@gmail.com",
                    Name = "Wenceslao Citera"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "This version is deprecated";
            }

            return info;
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }


    }
}
