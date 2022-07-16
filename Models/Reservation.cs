using System;

namespace Wsr.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public PoolTable PoolTable { get; set; }
        public Guid PoolTableId { get; set; }

        public string BookerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }
#nullable enable
        public Note? Note { get; set; }
        public Guid? NoteId { get; set; }
#nullable disable

        public Reservation(Guid tableId, Guid? noteId, string bookerName, string email, string phoneNumber, DateTime startDate, DateTime endDate, bool isPaid = false)
        {
            Id = Guid.NewGuid();
            PoolTableId = tableId;
            NoteId = noteId;
            BookerName = bookerName;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedDate = DateTime.Now;
            StartDate = startDate;
            EndDate = endDate;
            IsPaid = isPaid;
        }

        public Reservation() { }
    }

}
