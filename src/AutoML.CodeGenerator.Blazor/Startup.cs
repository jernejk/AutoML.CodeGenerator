using AutoML.CodeGenerator.Core;
using AutoML.CodeGenerator.Core.CodeGeneration;
using AutoML.CodeGenerator.Csv;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AutoML.CodeGenerator.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CsvToMetadataQuery>();
            services.AddTransient<DetermineColumnDefinitionsCommand>();
            services.AddTransient<AutoMLCodeQuery>();
            services.AddTransient<GenerateModelCodeQuery>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
