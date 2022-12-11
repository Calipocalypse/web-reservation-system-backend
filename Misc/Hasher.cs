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
        public static (string hash, string salt) Hash(string toHash, string readableSalt = null)
        {

            /* 1. Generating salt */
            if (readableSalt == null)
            {
                Random random = new Random();
                var salt = new Byte[12];
                random.NextBytes(salt);
                readableSalt = Convert.ToBase64String(salt);
            }

            /* 2. Generating hash from password + salt */
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(toHash + readableSalt));

                var hashed = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    hashed.Append(bytes[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return (hashed.ToString(), readableSalt);

            }
        }

        public static bool VerifyHash(string hashed, string salt, string notHashed)
        {
            if (hashed == Hash(notHashed, salt).Item1) return true;
            else return false;
        }

    }
}