using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Service.Services.Helper
{
    public  class PasswordHasher
    {
       public static string HashPassword (string password)
       {
            var sha256 = SHA256.Create ();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hashPassword = sha256.ComputeHash(bytes);

            return hashPassword.ToString();
       }

        public static bool checkPassword(string password, string stordPassword)
        {
            var hashPassword = HashPassword(password);

            return hashPassword == stordPassword;
        }
    }
}
