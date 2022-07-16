using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;
using Wsr.Data;
using Wsr.Models;

namespace Wsr.Controllers
{
    [ApiController]
    [Route("[controller]"+"s")]
    public class ReservationController : ControllerBase
    {
        private const string dateFormat = "dd'/'MM'/'yyyy HH:mm:ss";

        [HttpGet]
        public IActionResult Get([FromForm] string givenStartDate, [FromForm] string givenEndDate)
        {
            DateTime givenStartDateD = DateTime.ParseExact(givenStartDate, dateFormat,CultureInfo.InvariantCulture);
            DateTime givenEndDateD = DateTime.ParseExact(givenEndDate, dateFormat,CultureInfo.InvariantCulture);
            using (var context = new ApiContext())
            {
                var query = from reservation in context.Reservations
                            join note in context.Notes on reservation.NoteId equals note.Id
                            join table in context.PoolTables on reservation.PoolTableId equals table.Id
                            join cost in context.Costs on table.CostId equals cost.Id
                            where reservation.StartDate > givenStartDateD
                            where reservation.EndDate < givenEndDateD
                            select new
                            {
                                ReservationId = reservation.Id,
                                ReservationBookerName = reservation.BookerName,
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
                                NoteId = note.Id,
                                NoteContent = note.Content,
                                NoteCreatedDate = note.CreatedDate.ToString(dateFormat),
                                IsPaid = reservation.IsPaid.ToString()
                            };
                return Ok(query.ToArray());
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult Get(Guid id)
        {

            using (var context = new ApiContext())
            {
                var query = from reservation in context.Reservations
                            join note in context.Notes on reservation.NoteId equals note.Id
                            join table in context.PoolTables on reservation.PoolTableId equals table.Id
                            join cost in context.Costs on table.CostId equals cost.Id
                            where reservation.Id == id
                            select new
                            {
                                ReservationId = reservation.Id,
                                ReservationBookerName = reservation.BookerName,
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
                                NoteId = note.Id,
                                NoteContent = note.Content,
                                NoteCreatedDate = note.CreatedDate.ToString(dateFormat)
                            };
                return Ok(query.ToArray());
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm] Guid poolTableId, /*[FromForm] Guid? noteId,*/ [FromForm] string bookerName, [FromForm] string email, [FromForm] string phoneNumber, [FromForm] string startDate, [FromForm] string endDate)
        {
            using (var context = new ApiContext())
            {
                var newNote = new Note("");
                context.Add(newNote);
                context.SaveChanges();
                DateTime startDateD = DateTime.ParseExact(startDate, dateFormat, CultureInfo.InvariantCulture);
                DateTime endDateD = DateTime.ParseExact(endDate, dateFormat, CultureInfo.InvariantCulture);
                Reservation newReservation = new
                    Reservation(poolTableId, newNote.Id, bookerName, email, phoneNumber, startDateD, endDateD);
                context.Reservations.Add(newReservation);
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
                var toDelete = context.Reservations.FirstOrDefault(r => r.Id == id);
                context.Remove(toDelete);
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpPatch]
        [Route("{id:Guid}"+"/")]
        public IActionResult Patch(Guid id, [FromForm] Guid poolTableId, /*[FromForm] Guid? noteId,*/ [FromForm] string bookerName, [FromForm] string email, [FromForm] string phoneNumber, [FromForm] string startDate, [FromForm] string endDate)
        {
            using (var context = new ApiContext())
            {
                var editedReservation = context.Reservations.FirstOrDefault(x => x.Id == id);
                DateTime startDateD = DateTime.ParseExact(startDate, dateFormat, CultureInfo.InvariantCulture);
                DateTime endDateD = DateTime.ParseExact(endDate, dateFormat, CultureInfo.InvariantCulture);
                editedReservation.PoolTableId = poolTableId;
                editedReservation.StartDate = startDateD;
                editedReservation.EndDate = endDateD;
                editedReservation.BookerName = bookerName;
                editedReservation.PhoneNumber = phoneNumber;
                editedReservation.Email = email;

                context.Update(editedReservation);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPatch]
        [Route("{id:Guid}"+"/"+"{newPaidState}")]
        public IActionResult Patch(Guid id, string newPaidState)
        {
            using (var context = new ApiContext())
            {
                var toUpdate = context.Reservations.FirstOrDefault(x => x.Id == id);
                bool state;
                switch(newPaidState.ToLower())
                {
                    case "paid": state = true;
                        break;
                    case "notpaid": state = false;
                        break;
                    default: state = false;
                        break;
                }
                toUpdate.IsPaid = state;
                context.Update(toUpdate);
                context.SaveChanges();
            }
            return Ok();
        }

    }
}
