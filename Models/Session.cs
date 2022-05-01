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
        public string User { get; set; }
        //public string IpAdress { get; set; }  
        public DateTime ExpirationDate { get; set; }

        public Session(string user)
        {
            Id = Guid.NewGuid();
            User = user;
            Cookie = GenerateCookie();
            ExpirationDate = DateTime.Now.AddDays(7);
        }

        private string GenerateCookie() //ToDO
        {
            Random random = new Random();
            byte[] asciiBytes = new byte[16];
            random.NextBytes(asciiBytes);
            string cookie = Convert.ToBase64String(asciiBytes);
            
            return cookie;
        }

        public Session()
        {

        }
    }

}
