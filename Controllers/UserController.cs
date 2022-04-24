using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wsr.Models;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<User> Get()
        {
            List<User> users = new List<User>();
            users.Add(new User("JohnyKAsteroid", "45hj23j523jnpsdgkpng", false));
            return users;
        }
    }
}