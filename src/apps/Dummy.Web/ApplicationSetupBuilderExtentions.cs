using Dummy.Web.Services;
using Lens.Core.Lib.Builders;
using Microsoft.Extensions.DependencyInjection;

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
