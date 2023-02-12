using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Data;
using Wsr.Misc;
using Wsr.Models.Authentication.Enums;
using Wsr.Models.Database;
using Wsr.Models.Database.Enums;
using Wsr.Models.Exceptions;
using Wsr.Models.JsonModels;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]" + "s")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private const string dateFormat = "dd'/'MM'/'yyyy HH:mm";

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] TimeScopeDto timeScopeDto)
        {
            var givenStartDateD = DateTime.ParseExact(timeScopeDto.StartDate, dateFormat, CultureInfo.InvariantCulture);
            var givenEndDateD = DateTime.ParseExact(timeScopeDto.EndDate, dateFormat, CultureInfo.InvariantCulture);
            var context = new ApiContext();
            var query = from reservation in context.Reservations
                        where reservation.StartDate > givenStartDateD
                        && reservation.EndDate < givenEndDateD
                        select reservation;
            var result = await query.ToArrayAsync();
            return Ok(result);
        }

        [Route("all")]
        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var context = new ApiContext();
            var query = from reservation in context.Reservations
                        select reservation;
            var result = await query.ToArrayAsync();
            return Ok(result);
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> GetDetailed([FromBody] TimeScopeDto timeScopeDto)
        {
            var givenStartDateD = ParseDate(timeScopeDto.StartDate);
            var givenEndDateD = ParseDate(timeScopeDto.EndDate);
            var context = new ApiContext();

            var reservations = context.Reservations.Count();

            var query = from reservation in context.Reservations
                        join table in context.PoolTables on reservation.PoolTableId equals table.Id
                        join cost in context.Costs on table.CostId equals cost.Id
                        where reservation.StartDate > givenStartDateD
                        && reservation.EndDate < givenEndDateD
                        select new
                        {
                            ReservationId = reservation.Id,
                            ReservationBookerName = reservation.BookerName,
                            ReservationType = reservation.ReservationType.ToString(),
                            ReservationBookerEmail = reservation.Email,
                            ReservationBookerPhoneNumber = reservation.PhoneNumber,
                            ReservationCreatedDate = reservation.CreatedDate.ToString(dateFormat),
                            ReservationStartDate = reservation.StartDate.ToString(dateFormat),
                            ReservationEndDate = reservation.EndDate.ToString(dateFormat),
                            TableId = table.Id,
                            TableName = table.Name,
                            TableDescription = table.Description,
                            CostId = table.CostId,
                            CostName = cost.Name,
                            CostValue = cost.CostValue,
                            IsPaid = reservation.IsPaid.ToString(),
                            Note = reservation.Note
                        };

            return Ok(await query.ToArrayAsync());
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("details/all")]
        public async Task<IActionResult> GetDetailed()
        {
            var context = new ApiContext();

            var reservations = context.Reservations.Count();

            var query = from reservation in context.Reservations
                        join table in context.PoolTables on reservation.PoolTableId equals table.Id
                        join cost in context.Costs on table.CostId equals cost.Id
                        select new
                        {
                            ReservationId = reservation.Id,
                            ReservationBookerName = reservation.BookerName,
                            ReservationType = reservation.ReservationType.ToString(),
                            ReservationBookerEmail = reservation.Email,
                            ReservationBookerPhoneNumber = reservation.PhoneNumber,
                            ReservationCreatedDate = reservation.CreatedDate.ToString(dateFormat),
                            ReservationStartDate = reservation.StartDate.ToString(dateFormat),
                            ReservationEndDate = reservation.EndDate.ToString(dateFormat),
                            TableId = table.Id,
                            TableName = table.Name,
                            TableDescription = table.Description,
                            CostId = table.CostId,
                            CostName = cost.Name,
                            CostValue = cost.CostValue,
                            IsPaid = reservation.IsPaid.ToString(),
                            Note = reservation.Note
                        };

            return Ok(await query.ToArrayAsync());
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetDetailed(Guid id)
        {
            using (var context = new ApiContext())
            {
                var wantedReservation = await context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
                if (wantedReservation == null)
                {
                    return NotFound();
                }
                return Ok(wantedReservation);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpGet]
        [Route("{id:Guid}" + "/details")]
        public async Task<IActionResult> Get(Guid id)
        {
            using (var context = new ApiContext())
            {
                var query = from reservation in context.Reservations
                            join table in context.PoolTables on reservation.PoolTableId equals table.Id
                            join cost in context.Costs on table.CostId equals cost.Id
                            where reservation.Id == id
                            select new
                            {
                                ReservationId = reservation.Id,
                                ReservationBookerName = reservation.BookerName,
                                ReservationType = reservation.ReservationType.ToString(),
                                ReservationBookerEmail = reservation.Email,
                                ReservationBookerPhoneNumber = reservation.PhoneNumber,
                                ReservationCreatedDate = reservation.CreatedDate.ToString(dateFormat),
                                ReservationStartDate = reservation.StartDate.ToString(dateFormat),
                                ReservationEndDate = reservation.EndDate.ToString(dateFormat),
                                TableId = table.Id,
                                TableName = table.Name,
                                TableDescription = table.Description,
                                CostId = table.CostId,
                                CostName = cost.Name,
                                CostValue = cost.CostValue,
                                Note = reservation.Note
                            };
                var result = await query.ToArrayAsync();
                var firstOccurence = result.FirstOrDefault();
                if (firstOccurence == null)
                {
                    return NotFound();
                }
                return Ok(firstOccurence);
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post(ReservationDto reservationDto)
        {
            try
            {
                var reservation = new Reservation();

                if (reservationDto.PoolTableId != null)
                {
                    reservation.PoolTableId = ParseGuid(reservationDto.PoolTableId);
                }
                else
                {
                    var message = "PoolTableId is required";
                    return BadRequest(message);
                }

                if (reservationDto.BookerName != null)
                {
                    reservation.BookerName = reservationDto.BookerName;
                }
                else
                {
                    var message = "BookerName is required";
                    return BadRequest(message);
                }

                if (reservationDto.Email != null)
                {
                    reservation.Email = reservationDto.Email;
                }

                if (reservationDto.PhoneNumber != null)
                {
                    reservation.PhoneNumber = reservationDto.PhoneNumber;
                }

                if (reservationDto.StartDate != null)
                {
                    reservation.StartDate = ParseDate(reservationDto.StartDate);
                }
                else
                {
                    var message = "StartDate is required";
                    return BadRequest(message);
                }

                if (reservationDto.EndDate != null)
                {
                    reservation.EndDate = ParseDate(reservationDto.EndDate);
                }
                else
                {
                    var message = "EndDate is required";
                    return BadRequest(message);
                }

                if (reservationDto.IsPaid != null)
                {
                    reservation.IsPaid = ParseBool(reservationDto.IsPaid);
                }

                if (reservationDto.Note != null)
                {
                    reservation.Note = reservationDto.Note;
                }

                if (reservationDto.ReservationType != null)
                {
                    var failed = Enum.TryParse<ReservationType>(reservationDto.ReservationType, false, out var reservationType);
                    if (failed)
                    {
                        return BadRequest($"Can't parse {reservationDto.ReservationType} to enum");
                    }
                    reservation.ReservationType = (ReservationType)reservationType;
                }

                using (var context = new ApiContext())
                {
                    context.Reservations.Add(reservation);
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (GuidParseFailedException)
            {
                return BadRequest("Guid parsing failed");
            }
            catch (DateParseFailedException)
            {
                return BadRequest("DateTime parsing failed");
            }
            catch (BoolParseFailedException)
            {
                return BadRequest("DateTime parsing failed");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        private bool ParseBool(string probablyBool)
        {
            if (bool.TryParse(probablyBool, out var result))
            {
                return result;
            }
            throw new BoolParseFailedException();
        }

        private Guid ParseGuid(string probablyGuid)
        {
            if (Guid.TryParse(probablyGuid, out Guid result))
            {
                return result;
            }
            else
            {
                throw new GuidParseFailedException();
            }
        }
        private DateTime ParseDate(string probablyGuid)
        {
            if (DateTime.TryParseExact(probablyGuid, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new DateParseFailedException();
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            using (var context = new ApiContext())
            {
                var toDelete = context.Reservations.FirstOrDefault(r => r.Id == id);
                if (toDelete == null)
                {
                    return NotFound();
                }
                context.Remove(toDelete);
                await context.SaveChangesAsync();
                return Ok();
            }
        }

        [AuthorizeRole(UserRole.Operator, UserRole.Administrator)]
        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromQuery] ReservationDto reservationDto)
        {
            using (var context = new ApiContext())
            {
                var toUpdate = await context.Reservations.FirstAsync(x => x.Id == id);

                if (reservationDto.PoolTableId != null)
                {
                    toUpdate.PoolTableId = ParseGuid(reservationDto.PoolTableId);
                }
                if (reservationDto.BookerName != null)
                {
                    toUpdate.BookerName = reservationDto.BookerName;
                }
                if (reservationDto.Email != null)
                {
                    toUpdate.Email = reservationDto.Email;
                }
                if (reservationDto.PhoneNumber != null)
                {
                    toUpdate.PhoneNumber = reservationDto.PhoneNumber;
                }
                if (reservationDto.StartDate != null)
                {
                    toUpdate.StartDate = ParseDate(reservationDto.StartDate);
                }
                if (reservationDto.EndDate != null)
                {
                    toUpdate.EndDate = ParseDate(reservationDto.EndDate);
                }
                if (reservationDto.IsPaid != null)
                {
                    toUpdate.IsPaid = ParseBool(reservationDto.IsPaid);
                }
                if (reservationDto.Note != null)
                {
                    toUpdate.Note = reservationDto.Note;
                }

                context.Update(toUpdate);
                await context.SaveChangesAsync();
                return Ok();
            }
        }

    }
}
