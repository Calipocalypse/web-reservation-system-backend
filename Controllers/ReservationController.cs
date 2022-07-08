using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Wsr.Data;
using Wsr.Models;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]"+"s")]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using (var context = new ApiContext())
            {
                var query = from reservation in context.Reservations
                            join note in context.Notes on reservation.NoteId equals note.Id
                            join table in context.PoolTables on reservation.PoolTableId equals table.Id
                            join cost in context.Costs on table.CostId equals cost.Id
                            select new
                            {
                                ReservationId = reservation.Id,
                                ReservationBookerName = reservation.BookerName,
                                ReservationBookerEmail = reservation.Email,
                                ReservationBookerPhoneNumber = reservation.PhoneNumber,
                                ReservationCreatedDate = reservation.CreatedDate,
                                ReservationStartDate = reservation.StartDate,
                                ReservationEndDate = reservation.EndDate,
                                TableId = table.Id,
                                TableName = table.Name,
                                TableDescription = table.Description,
                                CostId = table.CostId,
                                CostName = cost.Name,
                                CostValue = cost.CostValue,
                                NoteId = note.Id,
                                NoteContent = note.Content,
                                NoteCreatedDate = note.CreatedDate
                            };
                return Ok(query.ToArray());
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] Guid poolTableId, [FromForm] Guid noteId, [FromForm] string bookerName, [FromForm] string email, [FromForm] string phoneNumber)
        {
            using (var context = new ApiContext())
            {
                var newReservation = new Reservation
                {
                    Id = Guid.NewGuid(),
                    PoolTableId = poolTableId,
                    NoteId = noteId,

                }
                /*
                 https://www.codeproject.com/Questions/1020558/Convert-a-string-to-datetime-in-hour-format-in-csh
                String strDate = "24/01/2022 00:00:00";
                DateTime date = DateTime.ParseExact(strDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                Console.WriteLine(date);
                */
            }
        }
    }
}
