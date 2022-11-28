﻿using System.Security.Cryptography;
using System.Text;

namespace DotNet7_WebAPI.Service
{
    public class Security
    {
        private const String AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
        public static String GetSalt()
        {
            var bytes = new Byte[64];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return new String(bytes.Select(x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray());
        }
        public static String MakeHashingPassWord(String saltValue, String pw)
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.ASCII.GetBytes(saltValue + pw));
            var stringBuilder = new StringBuilder();
            foreach (var b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }

            return stringBuilder.ToString();
        }
        public static String CreateAuthToken()
        {
            var bytes = new Byte[25];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            return new String(bytes.Select(x => AllowableCharacters[x % AllowableCharacters.Length]).ToArray());
        }
    }
}
