using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wsr.Models
{
    public class Session
    {
        public Guid Id { get; init; }
        public string Cookie { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Session()
        {

        }
    }

}
