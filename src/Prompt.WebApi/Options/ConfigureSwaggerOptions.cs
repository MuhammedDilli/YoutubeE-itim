﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Prompt.WebApi.Filters;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Prompt.WebApi.Options

{
    public sealed class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(string name, SwaggerGenOptions options)
        {
            options.OperationFilter<SwaggerJsonIgnoreFilter>();

            string folder = AppContext.BaseDirectory;

            if (Directory.Exists(folder))
            {
                foreach (string record in Directory.GetFiles(folder, "*.xml", SearchOption.AllDirectories))
                {
                    options.IncludeXmlComments(record);
                }
            }

            options.ExampleFilters();

            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            OpenApiInfo info = new()
            {
                Title = "Prompt API",
                Description = "Prompt API",
                Version = description.ApiVersion.ToString(),
                Contact = new()
                {
                    Name = "Muhammed Ali DİLLİ",
                    Email = "muhammed.bilg@hotmail.com",
                    Url = new("https://github.com/MuhammedDilli")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }
    }
}
