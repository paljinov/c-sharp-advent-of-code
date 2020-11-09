using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2015.Day4
{
    class PrefixHashSolution
    {
        public int FindIntegerWhichGivesMd5HashWithPrefix(string secretKey, string hashStartsWithPrefix)
        {
            string hash = string.Empty;
            int i = 0;

            while (!hash.StartsWith(hashStartsWithPrefix))
            {
                i++;
                hash = GetMd5HashForString(secretKey + i);
            }

            return i;
        }

        public string GetMd5HashForString(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            string hash = sb.ToString();

            return hash;
        }
    }
}
