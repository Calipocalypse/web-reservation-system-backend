using System;

namespace Wsr.Models
{
    public class Cost
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
