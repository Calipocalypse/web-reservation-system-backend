using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wsr.Misc;
using Wsr.Models;
using Wsr.Data;

namespace Wsr.Controllers
{
    [ApiController]
    public class SessionController : ControllerBase
    {
        [Route("[controller]")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string login, [FromForm] string password)
        {
            if (SessionVerifier.IsLoggedSucessfully(login, password))
            {
                 using(var context = new ApiContext())
                {
                    var newSession = new Session(login);
                    // 1:3.4028237e+38 probability that it will happen, lol
                    while (context.Sessions.Where(s => s.Cookie == newSession.Cookie).Count() > 0)
                    {
                        newSession = new Session(login);
                    }
                    context.Add(newSession);
                    await context.SaveChangesAsync();
                    return Ok(newSession.Cookie);
                }
            }
            else return Unauthorized();
        }
        [Route("[controller]")]
        [HttpGet]
        public IActionResult AuthorizeSession([FromForm] string sessionCookie, [FromForm] string userName)
        {
            //Removes ALL sessions where id matches to USER
            if (SessionVerifier.IsSessionVerifiedSucessfully(sessionCookie, userName)) return Ok();

            return Unauthorized();
        }

        [Route("[controller]")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] string sessionCookie, [FromForm] string userName)
        {
            if (SessionVerifier.IsSessionVerifiedSucessfully(sessionCookie, userName))
            {
                using (var context = new ApiContext())
                {
                    IEnumerable<Session> sessionsToRemove = context.Sessions.Where(s => s.User == userName);
                    var test = sessionsToRemove.Count();
                    context.RemoveRange(sessionsToRemove);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            else return Unauthorized();
        }
    }
}
