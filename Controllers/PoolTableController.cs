using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Misc;
using Wsr.Data;
using Microsoft.EntityFrameworkCore;
using Wsr.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Wsr.Models.JsonModels;
using Microsoft.EntityFrameworkCore.Internal;
using Wsr.Models.Authentication.Enums;

namespace Wsr.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]" + "s")]
    public class PoolTableController : ControllerBase
    {
        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var context = new ApiContext())
            {
                var poolTables = await context.PoolTables.ToArrayAsync();
                return Ok(poolTables);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetDetails()
        {
            var context = new ApiContext();
            var query = from table in context.PoolTables
                        join cost in context.Costs on table.CostId equals cost.Id
                        select new
                        {
                            Id = table.Id,
                            Name = table.Name,
                            Description = table.Description,
                            CostId = table.CostId,
                            CostName = cost.Name,
                            CostValue = cost.CostValue
                        };

            var process = await query.ToListAsync();
            return Ok(process);

        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            using (var context = new ApiContext())
            {
                try 
                {
                    var poolTables = await context.PoolTables.ToArrayAsync();
                    var poolTable = poolTables.First(x => x.Id == id);
                    return Ok(poolTable);
                }
                catch 
                { 
                    return NotFound(); 
                }
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("{id:Guid}/details")]
        public async Task<IActionResult> GetDetails(Guid id)
        {
            var context = new ApiContext();
            try
            {
                var query = await (from table in context.PoolTables
                            join cost in context.Costs on table.CostId equals cost.Id
                            where table.Id == id
                            select new
                            {
                                Id = table.Id,
                                Name = table.Name,
                                Description = table.Description,
                                CostId = table.CostId,
                                CostName = cost.Name,
                                CostValue = cost.CostValue
                            }).FirstOrDefaultAsync();

                return Ok(query);
            }
            catch
            {
                return NotFound();
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PoolTableDto poolTableDto)
        {
            if (!Guid.TryParse(poolTableDto.CostId, out var costId))
            {
                return BadRequest();
            }

            var poolTableToAdd = new PoolTable(poolTableDto.Name, poolTableDto.Description, costId);

            try
            {
                using (var context = new ApiContext())
                {
                    context.Add(poolTableToAdd);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (DbUpdateException)
            {
                var message = "Bad input";
                return BadRequest(message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            using (var context = new ApiContext())
            {
                try 
                {
                    var toDelete = await context.PoolTables.FirstAsync(x => x.Id == id);
                    context.Remove(toDelete);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch 
                { 
                    return BadRequest(); 
                }
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromQuery] PoolTableDto poolTableDto)
        {
            using(var context = new ApiContext())
            {
                if (poolTableDto.Name == null && poolTableDto.Description == null && poolTableDto.CostId == null)
                {
                    var message = "All dto object fields are null";
                    return BadRequest(message);
                }

                var toUpdate = await context.PoolTables.FirstAsync(x => x.Id == id);

                if (poolTableDto.Name != null)
                {
                    toUpdate.Name = poolTableDto.Name;
                }
                
                if (poolTableDto.Description != null)
                {
                    toUpdate.Description = poolTableDto.Description;
                }

                try
                {
                    if (poolTableDto.CostId != null)
                    {
                        if (!Guid.TryParse(poolTableDto.CostId, out var costId))
                        {
                            throw new Exception();
                        }
                        toUpdate.CostId = costId;
                    }
                }
                catch
                {
                    var message = "Guid incorrect";
                    return BadRequest(message);
                }

                context.Update(toUpdate);
                await context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
