using System.Xml.Linq;
using System;
using Wsr.Misc;
using Wsr.Models.Authentication.Enums;
using System.ComponentModel.DataAnnotations;

namespace Wsr.Models.Authentication
{
    public class UserModel
    {
        [Key]
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public UserRole Role { get; set; }

        public UserModel(string userName, string password, UserRole role)
        {
            UserName = userName;
            (HashedPassword, Salt) = Hasher.Hash(password);
            Role = role;
        }

        public UserModel()
        {

        }
    }
}
