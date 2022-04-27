using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wsr.Models;
using Wsr.Data;
using Wsr.Misc;

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


    [ApiController]
    [Route("test")]
    public class Test : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            using (var context = new ApiContext())
            {
                string password = "Password12345";

                return new string[]
                {
                    Hasher.Hash("hello word"),
                    Hasher.Hash("12345"),
                    Hasher.Hash("rerere"),
                    Hasher.Hash("25sdfcdsas xcvxczczgfwet235g"),
                    Hasher.Hash("00000000"),
                    Hasher.Hash("tes234523t"),
                    Hasher.Hash("xxxxxxxxxxxxxxxxx"),

                    Hasher.VerifyHash(Hasher.Hash(password),password).ToString()
                };
            }

        }
    }
}