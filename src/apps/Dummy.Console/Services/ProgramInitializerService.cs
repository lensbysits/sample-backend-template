using CoreLib.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dummy.Console.Services
{
    public class ProgramInitializerService : BaseService<ProgramInitializerService>, IProgramInitializer
    {
        public ProgramInitializerService(IApplicationService<ProgramInitializerService> applicationService) : base(applicationService)
        {
        }

        public async Task Initialize()
        {
            ApplicationService.Logger.LogInformation("This is where we would do some initialization, like updating and seeding a database, or creating folders, etc.");
            await Task.CompletedTask;
        }
    }
}
