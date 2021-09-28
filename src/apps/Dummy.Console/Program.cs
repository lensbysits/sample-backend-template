using CoreApp.Console;
using CoreLib;
using CoreLib.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dummy.Console.Services;
using System.Threading.Tasks;

namespace Dummy.Console
{
    class Program : ProgramBase
    {
        static async Task<int> Main(string[] args)
        {
            ApplicationSetup = MyApplicationSetup; 
            return await Start(args);
        }

        private static void MyApplicationSetup(HostBuilderContext context, IApplicationSetupBuilder applicationSetup)
        {
            applicationSetup
                .AddProgramInitializer<ProgramInitializerService>()
                .Services
                .AddHostedService<LongRunningBackgroundService>();
        }
    }
}
