using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wsr.Models;
using Wsr.Data;
using Wsr.Misc;
using System;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new ApiContext())
            {
                return Ok(context.Users.ToArray());
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            using (var context = new ApiContext())
            {
                return Ok(context.Users.FirstOrDefault(x => x.Id == id));
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] string newLogin, [FromForm] string newPassword)
        {
            using (var context = new ApiContext())
            {
                context.Add(new User(newLogin, newPassword, false));
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            using (var context = new ApiContext())
            {
                var toDelete = context.Users.FirstOrDefault(x => x.Id == id);
                if (toDelete == null) return BadRequest();
                else
                {
                    context.Remove(toDelete);
                    context.SaveChanges();
                    return Ok();
                }
            }
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public IActionResult UpdatePasword(Guid id, [FromForm] string newPassword)
        {
            using (var context = new ApiContext())
            {
                var toEdit = context.Users.FirstOrDefault(x => x.Id == id);
                (string hashed, string salt) = Hasher.Hash(newPassword);
                toEdit.HashedPassword = hashed;
                toEdit.Salt = salt;
                context.Update(toEdit);
                context.SaveChanges();
                return Ok();
            }
        }

    }
}