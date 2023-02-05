using System;

namespace Wsr.Models
{
    public class Note
    {
        public Guid Id { get; set; }
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
