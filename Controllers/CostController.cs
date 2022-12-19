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

namespace Wsr.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]" + "s")]
    public class CostController : ControllerBase
    {
        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var context = new ApiContext())
            {
                var costs = await context.Costs.ToArrayAsync();
                return Ok(costs);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            using (var context = new ApiContext())
            {
                var cost = await context.Costs.FirstAsync(x => x.Id == id);
                return Ok(cost);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CostPostDto costModel) //For costValue xx.xx is valid
        {
            using (var context = new ApiContext())
            {
                string costValue = costModel.CostValue;

                try
                {
                    var convertedValue = ConvertStringToDecimal(costValue);
                    var cost = new Cost(costModel.Name, convertedValue);
                    context.Add(cost);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
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
                    var toDelete = await context.Costs.FirstAsync(x => x.Id == id);
                    context.Remove(toDelete);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch 
                { 
                    return NotFound(); 
                }
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromQuery] CostPostDto costModel)
        {
            using (var context = new ApiContext())
            {
                try
                {
                    var toUpdate = await context.Costs.FirstAsync(x => x.Id == id);

                    if (costModel.CostValue != null)
                    {
                        var costValue = costModel.CostValue;
                        var convertedValue = ConvertStringToDecimal(costValue);
                        toUpdate.CostValue = convertedValue;
                    }

                    if (costModel.Name != null)
                    {
                        toUpdate.Name = costModel.Name;
                    }

                    context.Update(toUpdate);
                    await context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        private decimal ConvertStringToDecimal(string number)
        {
            if (!Decimal.TryParse(number, out var convertedValue))
            {
                var message = $"Can't convert {number} to decimal number. Use xx.xx as template";
                throw new ArgumentException(message);
            }
            return convertedValue;
        }

    }
}
