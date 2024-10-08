﻿using System.Security.Cryptography;
using System.Text;


namespace HotelReservationSystem.Service.Services.Helper
{
    public  class PasswordHasher
    {
       public static string HashPassword(string password)
       {
            var sha256 = SHA256.Create ();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hashBytes = sha256.ComputeHash(bytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static bool checkPassword(string password, string stordPassword)
        {
            var hashPassword = HashPassword(password);

            return hashPassword == stordPassword;
        }
    }
}
