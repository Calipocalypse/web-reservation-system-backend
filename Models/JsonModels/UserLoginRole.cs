using Wsr.Models.Authentication.Enums;

namespace Wsr.Models.JsonModels
{
    public class UserLoginRole
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
