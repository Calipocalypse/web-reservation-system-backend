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
                var firstUser = context.Users.FirstOrDefault();
                return new string[]
                {
                    Hasher.Hash("hello word").Item1,
                    Hasher.Hash("12345").Item1,
                    Hasher.Hash("rerere").Item1,
                    Hasher.Hash("25sdfcdsas xcvxczczgfwet235g").Item1,
                    Hasher.Hash("00000000").Item1,
                    Hasher.Hash("tes234523t").Item1,
                    Hasher.Hash("xxxxxxxxxxxxxxxxx","staticHash").Item1,

                    Hasher.VerifyHash("b2e44b332807361cd9c13b898614595354c3ffcd7265c472dc702212f82859c8","fw7Onh6DmRfovAM/","test").ToString(),
                    Hasher.VerifyHash(firstUser.HashedPassword,firstUser.Salt,"test").ToString()
                };
            }
        }
    }
}