using Microsoft.AspNetCore.Mvc;

namespace BrokenServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrokenController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            int rnd = new Random().Next(1, 4);

            if (rnd == 2)
            {
                throw new ArgumentNullException("");
            }

            return Ok();
        }
    }
}