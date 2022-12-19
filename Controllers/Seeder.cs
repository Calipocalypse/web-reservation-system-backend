using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wsr.Data;
using Wsr.Misc;
using Wsr.Models;
using Wsr.Models.Authentication.Enums;
using Wsr.Models.Database;

namespace Wsr.Controllers
{
    [Authorize]
    [ApiController]
    [Route("SeedRoute")]
    public class Seeder : ControllerBase
    {
        [AuthorizeRole(UserRole.Administrator)]
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

        [AuthorizeRole(UserRole.Administrator)]
        [HttpGet]
        public IActionResult SeederMethod2()
        {
            return Ok();
        }
    }
}
