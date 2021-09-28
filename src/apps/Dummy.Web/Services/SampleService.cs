using CoreLib.Services;
using Dummy.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dummy.Web.Services
{
    public class SampleService : BaseService<SampleService>, ISampleService
    {
        public SampleService(IApplicationService<SampleService> applicationService) : base(applicationService)
        {
        }

        public async Task<IEnumerable<SampleBM>> Get()
        {
            var result = new List<SampleBM>
            {
                new SampleBM { Id = Guid.NewGuid(), Name = "Sample 1" },
                new SampleBM { Id = Guid.NewGuid(), Name = "Sample 2" }
            };

            return await Task.FromResult(result);
        }
    }
}
