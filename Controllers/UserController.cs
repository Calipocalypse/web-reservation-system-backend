using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wsr.Models;
using Wsr.Data;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> Get()
        {
            using (var context = new ApiContext())
            {
                return context.Users.ToArray();
            }
        }
    }
}