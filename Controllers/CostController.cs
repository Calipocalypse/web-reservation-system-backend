using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Wsr.Data;
using Wsr.Models;
using System;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class CostController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new ApiContext())
            {
                return Ok(context.Costs.ToArray());
            }
            //else return Unauthorized();
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            using (var context = new ApiContext())
            {
                var cost = context.Costs.FirstOrDefault(x => x.Id == id);
                return Ok(cost);
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] string name, [FromForm] string costValue) //For costValue xx,xx is valid
        {
            using (var context = new ApiContext())
            {
                decimal convertedValue = 0;
                if (!Decimal.TryParse(costValue, out convertedValue)) return BadRequest();

                context.Add(new Cost(name, convertedValue));
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
                Cost toDelete;
                try { toDelete = context.Costs.FirstOrDefault(x => x.Id == id); }
                catch { return NotFound(); }

                context.Remove(toDelete);
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public IActionResult Update(Guid id,[FromForm] string name, [FromForm] string costValue)
        {
            using (var context = new ApiContext())
            {
                decimal convertedValue = 0;
                if (!Decimal.TryParse(costValue, out convertedValue)) return BadRequest();

                Cost toUpdate;
                try { toUpdate = context.Costs.FirstOrDefault(x => x.Id == id); }
                catch { return NotFound(); }

                toUpdate.CostValue = convertedValue;
                toUpdate.Name = name;

                context.Update(toUpdate);
                context.SaveChanges();
                return Ok();
            }
        }

    }
}
