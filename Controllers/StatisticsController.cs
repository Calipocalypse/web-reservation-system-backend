using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Data;
using Wsr.Misc;
using Wsr.Models.Authentication.Enums;
using Wsr.Models.JsonModels;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        [Route("transactions/average/daysofweek")]
        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var context = new ApiContext())
            {
                var reservations = context.Reservations;
                var sum = reservations.Count();
                var firstReservation = reservations.OrderBy(x => x.StartDate).First();
                var lastReservation = reservations.OrderBy(x => x.StartDate).Last();

                var reservationsInterval = lastReservation.StartDate - firstReservation.StartDate;
                var totalDays = reservationsInterval.Days;
                var singleDayOccurences = totalDays / 7;

                var final = new List<StatWeekDto>();

                foreach (var dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
                {
                    var dayOfWeekCorrection = (DayOfWeek)dayOfWeek;
                    var number = reservations.AsEnumerable().Where(x => (DayOfWeek)x.StartDate.Date.DayOfWeek == dayOfWeekCorrection).ToList();
                    var realNumber = number.Count();
                    var finalNumber = (double)realNumber / singleDayOccurences;
                    var dto = new StatWeekDto { DayOfWeek = dayOfWeekCorrection.ToString(), Number = finalNumber.ToString() };
                    final.Add(dto);
                }
                return Ok(final);
            }
        }

        [Route("transactions/count")]
        [AuthorizeRole(UserRole.Administrator, UserRole.Operator)]
        [HttpPost]
        public async Task<IActionResult> Get2([FromBody] TimeScopeDto timeScopeDto)
        {
            DateTime.TryParse(timeScopeDto.StartDate, out DateTime startDate);
            DateTime.TryParse(timeScopeDto.EndDate, out DateTime endDate);

            if (startDate == null || endDate == null)
            {
                return BadRequest();
            }

            using (var context = new ApiContext())
            {
                var number = context.Reservations.AsEnumerable().Where(x => (x.StartDate >= startDate && x.EndDate <= endDate)).Count();
                var numberDto = new {number};
                return Ok(numberDto);
            }
        }
    }

    
}
