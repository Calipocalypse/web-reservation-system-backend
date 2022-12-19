using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Wsr.Models.Database
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public Note(string content)
        {
            Id = Guid.NewGuid();
            Content = content;
            CreatedDate = DateTime.Now;
        }
    }
}
