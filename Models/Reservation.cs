using System;

namespace Wsr.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public PoolTable PoolTable { get; set; }
        public Guid PoolTableId { get; set; }
        public Note? Note { get; set; }
        public Guid NoteId { get; set; }
        public string BookerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
