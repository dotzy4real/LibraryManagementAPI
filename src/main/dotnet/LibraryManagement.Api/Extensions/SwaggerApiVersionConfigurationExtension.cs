using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryManagement.Api.Extensions
{
    public class SwaggerApiVersionConfigurationExtension : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionProvider;
        public SwaggerApiVersionConfigurationExtension(IApiVersionDescriptionProvider apiVersionProvider)
        {
            _apiVersionProvider = apiVersionProvider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription desc in _apiVersionProvider.ApiVersionDescriptions)
            {
                var _openApiInfo = new OpenApiInfo
                {
                    Title = $"Ayo Integration Api v{desc.ApiVersion}",
                    Version = desc.ApiVersion.ToString(),
                    Description = $"Api Description {desc.ApiVersion}"
                };
                options.SwaggerDoc(desc.GroupName, _openApiInfo);
            }
        }
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
