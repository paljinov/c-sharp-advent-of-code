using System;
using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2016.Day5
{
    public class Password
    {
        private const int PasswordLength = 8;

        private const string HashStartsWithPrefix = "00000";

        public string FindPasswordForDoorId(string doorId)
        {
            StringBuilder password = new StringBuilder();

            int i = 0;
            while (password.Length < PasswordLength)
            {
                i++;
                string hash = GetMd5HashForString(doorId + i);
                if (hash.StartsWith(HashStartsWithPrefix))
                {
                    password.Append(hash[HashStartsWithPrefix.Length]);
                }
            }

            return password.ToString();
        }

        public string FindPasswordForDoorIdWithPositionCondition(string doorId)
        {
            char[] password = new char[PasswordLength];
            int foundPasswordCharacters = 0;

            int i = 0;
            while (foundPasswordCharacters < PasswordLength)
            {
                i++;
                string hash = GetMd5HashForString(doorId + i);
                if (hash.StartsWith(HashStartsWithPrefix))
                {
                    int position = (int)char.GetNumericValue(hash[HashStartsWithPrefix.Length]);
                    // If character is integer in allowed password length range
                    if (position >= 0 && position < PasswordLength)
                    {
                        char character = hash[HashStartsWithPrefix.Length + 1];

                        if (password[position] == '\0')
                        {
                            password[position] = character;
                            foundPasswordCharacters++;
                        }
                    }
                }
            }

            return new string(password);
        }

        private static string GetMd5HashForString(string input)
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
