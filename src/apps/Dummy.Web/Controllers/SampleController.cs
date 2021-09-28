using Dummy.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dummy.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _sampleService.Get();
            return Ok(result);
        }
    }
}
