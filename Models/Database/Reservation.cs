using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Wsr.Models.Database
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        public PoolTable PoolTable { get; set; }
        [Required]
        public Guid PoolTableId { get; set; }
        [Required]
        public string BookerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        public virtual Note Note { get; set; }
        public Guid? NoteId { get; set; }

        public Reservation() 
        {
            CreatedDate = DateTime.Now;
            IsPaid = false;
        }
    }

}
