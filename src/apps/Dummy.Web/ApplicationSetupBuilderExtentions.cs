using CoreLib.Builders;
using Microsoft.Extensions.DependencyInjection;
using Dummy.Web.Services;

namespace Dummy.Web
{
    public static class ApplicationSetupBuilderExtentions
    {
        public static IApplicationSetupBuilder AddDummyServices(this IApplicationSetupBuilder applicationSetup)
        {
            applicationSetup.Services.AddScoped<ISampleService, SampleService>();
            return applicationSetup;
        }
    }
}
