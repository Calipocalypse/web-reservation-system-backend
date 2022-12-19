using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Wsr.Models.Database
{
    [Index(nameof(Name), IsUnique = true)]
    public class Cost
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal CostValue { get; set; }

        public Cost(string name, decimal cost)
        {
            Id = Guid.NewGuid();
            Name = name;
            CostValue = cost;
        }

        public Cost() { }
    }
}
