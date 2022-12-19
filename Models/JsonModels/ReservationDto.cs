namespace Wsr.Models.JsonModels
{
    public class ReservationDto
    {
        public string PoolTableId { get; set; }
        public string BookerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string IsPaid { get; set; }
        public string NoteId { get; set; }
    }
}
