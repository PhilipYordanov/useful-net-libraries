using Microsoft.AspNetCore.Mvc;

namespace PollyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollyController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public PollyController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Call()
        {
            var client = this.httpClientFactory.CreateClient("BrokenServer");

            var res = await client.GetAsync("/Broken");

            return Ok(res);
        }
    }
}