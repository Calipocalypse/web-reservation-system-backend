using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wsr.Models;
using Wsr.Data;
using Wsr.Misc;
using System;
using Wsr.Models.Authentication.Enums;
using Wsr.Models.Database;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Wsr.Controllers.Misc;
using Wsr.Models.JsonModels;
using Microsoft.Identity.Web;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    [Authorize]
    public class UserController : ControllerBase
    {
        ////Creating first administrator account
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostFirstAccount([FromBody] UserLogin userLogin)
        {
            if (areNoAccountsInDatabase())
            {
                using (var context = new ApiContext())
                {
                    var newUser = new UserModel(userLogin.Username, userLogin.Password, UserRole.Administrator);
                    context.Add(newUser);
                    await context.SaveChangesAsync();
                    return Ok($"Administrator account of name {userLogin.Username} has been created succesfully");
                }
            }

            var error = new Error("There are already accounts in database");
            return BadRequest(error);

            bool areNoAccountsInDatabase()
            {
                using (var api = new ApiContext())
                {
                    var count = api.Users.Count();
                    if (count == 0) return true;
                }
                return false;
            }
        }

        //Creating another accounts, administrators too
        [AuthorizeRole(UserRole.Administrator)]
        [HttpPost]
        public IActionResult Post([FromBody] UserLoginRole userLoginRole)
        {
            using (var context = new ApiContext())
            {
                UserRole userRole;
                try
                {
                    userRole = (UserRole)Enum.Parse(typeof(UserRole), userLoginRole.UserRole);
                }
                catch
                {
                    var message = $"Unknown role: {userLoginRole.UserRole}";
                    return BadRequest(message);
                }

                var userAlreadyExists = true;//context.Users.Any(x => x.UserName == userLoginRole.UserName;
                if (!userAlreadyExists)
                {
                    var newUser = new UserModel(userLoginRole.UserName, userLoginRole.Password, userRole);
                    context.Add(newUser);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest($"User {userLoginRole.UserName} already exists");
                }
            }
        }

        //Deleting accounts
        [AuthorizeRole(UserRole.Administrator)]
        [HttpDelete]
        [Route("{userName}")]
        public IActionResult Delete(string userName)
        {
            using (var context = new ApiContext())
            {
                var toDelete = context.Users.FirstOrDefault(x => x.UserName == userName);
                if (toDelete == null)
                {
                    var message = "User doesn't exist";
                    return BadRequest(message);
                }
                else
                {
                    context.Remove(toDelete);
                    context.SaveChanges();
                    return Ok();
                }
            }
        }

        [HttpPatch]
        [Route("{userName}")]
        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        public IActionResult UpdatePasword(string userName, [FromBody] string newPassword)
        {
            using (var context = new ApiContext())
            {
                var toEdit = context.Users.FirstOrDefault(x => x.UserName == userName);
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