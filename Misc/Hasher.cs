using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Wsr.Misc
{
    public static class Hasher
    {
        public static string Hash(string toHash)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(toHash));

                //return Convert.ToBase64String(bytes);

                var hashed = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    hashed.Append(bytes[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return hashed.ToString();

            }
        }

        public static bool VerifyHash(string hashed, string notHashed)
        {
            if (hashed == Hash(notHashed)) return true;
            else return false;
        }

    }
}