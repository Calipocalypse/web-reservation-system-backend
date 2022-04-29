using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsr.Data;
using Wsr.Models;

namespace Wsr.Misc
{
    public static class SessionVerifier
    {
        public static bool IsLoggedSucessfully(string user, string password)
        {
            using (var context = new ApiContext())
            {
                /* 1. Searching for given login user */
                IEnumerable<User> users = context.Users;
                User validUser = users.FirstOrDefault(u => u.Name == user); //ToDo, exception to handle

                /* 2. If user not found */
                if (validUser == null) return false;

                /* 3. Authorizing user */
                if (Hasher.VerifyHash(validUser.HashedPassword, validUser.Salt, password)) return true;

                /* 4. Authorization failed */
                return false;
            }
        }

        public static bool IsSessionVerifiedSucessfully(string sessionCookie, string userName)
        {
            using (var context = new ApiContext())
            {
                /* 1. Searching for given session */
                IEnumerable<Session> sessions = context.Sessions;
                Session requestedSession = sessions.FirstOrDefault(s => s.Cookie == sessionCookie);
                
                /* 2. If session not found */
                if (requestedSession == null) return false;

                /* 3. Checking conditions to authorize */
                if (requestedSession.User == userName && requestedSession.ExpirationDate > DateTime.Now) return true;
                
                /* 4. Authorization failed */
                return false;
            }
        }

        //Destroy cookie method ~ OnLogoffAttempt ToDo
    }
}
