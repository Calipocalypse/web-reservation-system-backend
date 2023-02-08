using Wsr.Models.Database.Enums;

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
        public string Note { get; set; }
        public string ReservationType { get; set; }
    }
}
