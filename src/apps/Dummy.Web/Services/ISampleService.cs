using Dummy.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dummy.Web.Services
{
    public interface ISampleService
    {
        Task<IEnumerable<SampleBM>> Get();
    }
}
