using Microsoft.AspNetCore.Mvc;
using Wsr.Data;
using Wsr.Models;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("SeedRoute")]
    public class Seeder : ControllerBase
    {
        [HttpPost]
        public IActionResult SeederMethod()
        {
            using (var context = new ApiContext())
            {
                context.Add(new Note("Ten stół należy do Johnego K Asteroida"));
                context.Add(new Note("Ten stół należy do wroga Johnego K Asteroida"));

                context.Add(new Cost("Normalny", 99.99m));
                context.Add(new Cost("Ulgowy", 49.99m));
            }
            return Ok();
        }
    }
}
