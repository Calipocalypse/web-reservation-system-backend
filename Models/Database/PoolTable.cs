using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wsr.Models.Database
{
    [Index(nameof(Name), IsUnique = true)]
    public class PoolTable
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Cost Cost { get; set; }
        [Required]
        public Guid CostId { get; set; }

        public PoolTable(string name, string description, Guid costId)
        {
            if (description == null)
            {
                description = String.Empty;
            }

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CostId = costId;
        }
    }
}
