using CoreLib.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dummy.Console.Services
{
    public class LongRunningBackgroundService : BaseBackgroundService<LongRunningBackgroundService>
    {
        public LongRunningBackgroundService(
            IApplicationService<LongRunningBackgroundService> applicationService) : base(applicationService)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000,stoppingToken);
                ApplicationService.Logger.LogInformation($"It's {DateTime.Now.ToLongTimeString()} now.");
            }
        }
    }
}
