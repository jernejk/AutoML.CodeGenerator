using AutoML.CodeGenerator.Core.CodeGeneration;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AutoML.CodeGenerator.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<AutoMLCodeQuery>();
            services.AddTransient<GenerateModelCodeQuery>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
