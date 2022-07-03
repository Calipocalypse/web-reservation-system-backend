using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Misc;
using Wsr.Data;
using Wsr.Models;
using Microsoft.EntityFrameworkCore;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    public class PoolTableController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new ApiContext())
            {
                var query = from table in context.PoolTables
                            join cost in context.Costs on table.CostId equals cost.Id
                            select new 
                            {Id = table.Id,
                            Name = table.Name,
                            Description = table.Description,
                            CostId = table.CostId,
                            CostName = cost.Name,
                            CostValue = cost.CostValue};
                return Ok(query.ToArray());
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            using (var context = new ApiContext())
            {
                PoolTable poolTable;
                try { poolTable = context.PoolTables.Include("Cost").FirstOrDefault(x => x.Id == id); }
                catch { return NotFound(); }
                return Ok(poolTable.Cost.Name);
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] string name, [FromForm] string description, [FromForm] string costGuid)
        {
            Guid costId;
            try { costId = Guid.Parse(costGuid); }
            catch { return BadRequest(); }
            PoolTable poolTableToAdd = new PoolTable(name, description, costId);
            using (var context = new ApiContext())
            {
                context.Add(poolTableToAdd);
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
                PoolTable toDelete;
                try { toDelete = context.PoolTables.FirstOrDefault(x => x.Id == id); }
                catch { return BadRequest(); }
                context.Remove(toDelete);
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public IActionResult Update(Guid id, [FromForm] string name, [FromForm] string description, [FromForm] Guid costId)
        {
            PoolTable poolTable;
            using(var context = new ApiContext())
            {
                try { poolTable = context.PoolTables.FirstOrDefault(x => x.Id == id); }
                catch { return BadRequest(); }

                poolTable.Name = name;
                poolTable.Description = description;
                poolTable.CostId = costId;

                context.Update(poolTable);
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
