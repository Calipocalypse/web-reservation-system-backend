using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Wsr.Data;
using Wsr.Models;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class NoteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new ApiContext())
            {
                return Ok(context.Notes.ToArray());
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            using var context = new ApiContext();
            {
                return Ok(context.Notes.FirstOrDefault(x=>x.Id==id));
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] string content)
        {
            using (var context = new ApiContext())
            {
                context.Add(new Note(content));
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
                Note toDelete;
                try { toDelete = context.Notes.FirstOrDefault(x => x.Id == id); }
                catch { return BadRequest(); }
                context.Remove(toDelete);
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public IActionResult Update(Guid id, [FromForm] string newContent)
        {
            Note toUpdate;
            using (var context = new ApiContext())
            {
                try { toUpdate = context.Notes.FirstOrDefault(x => x.Id == id); }
                catch { return BadRequest(); }
                toUpdate.Content = newContent;
                context.Update(toUpdate);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
