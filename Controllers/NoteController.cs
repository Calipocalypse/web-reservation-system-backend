using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Data;
using Wsr.Misc;
using Wsr.Models.Authentication.Enums;
using Wsr.Models.Database;
using Wsr.Models.JsonModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var context = new ApiContext())
            {
                var allNotes = await context.Notes.ToListAsync();
                allNotes.OrderByDescending(x => x.CreatedDate);
                return Ok(allNotes);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            using var context = new ApiContext();
            {
                var givenNote = await context.Notes.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(givenNote);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotePostDto content)
        {
            if (content.Content == null || content.Content == String.Empty)
            {
                var message = "Content is null";
                return BadRequest(message);
            }
            try
            {
                using (var context = new ApiContext())
                {
                    var note = new Note(content.Content);
                    context.Add(note);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    var toDelete = await context.Notes.FirstAsync(x => x.Id == id);
                    if (toDelete is null)
                    {
                        return NotFound();
                    }
                    context.Remove(toDelete);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromQuery] string newContent)
        {
            try
            {
                using (var context = new ApiContext())
                {
                    var toUpdate = await context.Notes.FirstAsync(x => x.Id == id);
                    toUpdate.Content = newContent;
                    context.Update(toUpdate);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
