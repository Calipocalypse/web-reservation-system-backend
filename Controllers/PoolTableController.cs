using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Misc;
using Wsr.Data;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class PoolTableController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get([FromForm] string sessionCookie, [FromForm] string userName)
        {
            if (SessionVerifier.IsSessionVerifiedSucessfully(sessionCookie, userName))
            {
                using (var context = new ApiContext())
                {
                    //return Ok(context.PoolTables.ToArray());
                    return Ok();
                }
            }
            else return Unauthorized();
        }
    }
}
