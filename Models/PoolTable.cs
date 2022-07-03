using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wsr.Models
{
    public class PoolTable
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Cost Cost { get; set; }

        public PoolTable(string name, string description, Cost cost)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Cost = cost;
        }
    }
}
