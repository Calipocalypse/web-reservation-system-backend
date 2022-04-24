using System;

namespace Wsr.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public bool IsAdmin { get; set; }

        public User(string name, string hashedPassword, bool isAdmin)
        {
            Name = name;
            HashedPassword = hashedPassword;
            IsAdmin = isAdmin;
        }
    }
}