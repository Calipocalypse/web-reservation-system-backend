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

        public PoolTable(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
