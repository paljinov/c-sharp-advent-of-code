using System;
using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2016.Day5
{
    class Password
    {
        private const int PasswordLength = 8;

        public string FindPasswordForDoorId(string doorId, string startsWithPrefix)
        {
            string password = string.Empty;

            int i = 0;
            while (password.Length < PasswordLength)
            {
                i++;
                string hash = GetMd5HashForString(doorId + i);
                if (hash.StartsWith(startsWithPrefix))
                {
                    password += hash[startsWithPrefix.Length];
                }
            }

            return password;
        }

        public static string GetMd5HashForString(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
