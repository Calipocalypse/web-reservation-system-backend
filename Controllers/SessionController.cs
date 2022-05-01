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
        [Route("[controller]" + "/login")]
        [HttpPost]
        public bool Post([FromForm] string login, [FromForm] string password)
        {
            if (SessionVerifier.IsLoggedSucessfully(login, password))
            {
                using (var context = new ApiContext())
                {
                    var newSession = new Session(login);
                    // 1:3.4028237e+38 probability that it will happen, lol
                    while (context.Sessions.Where(s => s.Cookie == newSession.Cookie).Count() > 0)
                    {
                        newSession = new Session(login);
                    }
                    context.Add(newSession);
                    context.SaveChanges();
                }
                return true;
            }
            else return false;
        }
        [Route("[controller]")]
        [HttpPost]
        public bool AuthorizeSession([FromForm] string sessionCookie, [FromForm] string userName)
        {
            if (SessionVerifier.IsSessionVerifiedSucessfully(sessionCookie, userName)) return true;

            return false;
        }

        [Route("[controller]")]
        [HttpDelete]
        public IActionResult Delete([FromForm] string sessionCookie, [FromForm] string userName)
        {
            if (SessionVerifier.IsSessionVerifiedSucessfully(sessionCookie, userName))
            {
                using (var context = new ApiContext())
                {
                    IEnumerable<Session> sessionsToRemove = context.Sessions.Where(s => s.User == userName);
                    var test = sessionsToRemove.Count();
                    context.RemoveRange(sessionsToRemove);
                    context.SaveChanges();
                    return Ok();
                }
            }
            else return Unauthorized();
        }
    }
}
