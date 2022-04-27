using System;
using Wsr.Misc;

namespace Wsr.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
        public bool IsAdmin { get; set; }

        public User(string name, string password, bool isAdmin)
        {
            Id = Guid.NewGuid();
            Name = name;
            HashedPassword = Hasher.Hash(password);
            IsAdmin = isAdmin;
        }
        public User()
        {
        }
    }
}