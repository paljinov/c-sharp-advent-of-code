using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace App.Tasks.Year2016.Day17
{
    public class Path
    {
        private const int START_X = 0;

        private const int START_Y = 0;

        private const int VAULT_X = 3;

        private const int VAULT_Y = 3;

        private readonly char[] openDoorsCharacters = new char[] { 'b', 'c', 'd', 'e', 'f' };

        public string FindShortestPathToVault(string passcode)
        {
            StringBuilder shortestPath = new StringBuilder();
            FindShortestPath(passcode, START_X, START_Y, string.Empty, shortestPath);

            return shortestPath.ToString();
        }

        private void FindShortestPath(string passcode, int x, int y, string path, StringBuilder shortestPath)
        {
            if (shortestPath.Length > 0 && path.Length >= shortestPath.Length)
            {
                return;
            }

            if (x == VAULT_X && y == VAULT_Y
                && (shortestPath.Length == 0 || path.Length < shortestPath.Length))
            {
                shortestPath.Remove(0, shortestPath.Length);
                shortestPath.Append(path);
                return;
            }

            string hash = GetMd5HashForString(passcode + path);

            // If it is possible to go up
            if (y - 1 >= 0 && openDoorsCharacters.Contains(hash[(int)Direction.UP]))
            {
                FindShortestPath(passcode, x, y - 1, path + 'U', shortestPath);
            }

            // If it is possible to go down
            if (y + 1 <= VAULT_Y && openDoorsCharacters.Contains(hash[(int)Direction.DOWN]))
            {
                FindShortestPath(passcode, x, y + 1, path + 'D', shortestPath);
            }

            // If it is possible to go left
            if (x - 1 >= 0 && openDoorsCharacters.Contains(hash[(int)Direction.LEFT]))
            {
                FindShortestPath(passcode, x - 1, y, path + 'L', shortestPath);
            }

            // If it is possible to go right
            if (x + 1 <= VAULT_X && openDoorsCharacters.Contains(hash[(int)Direction.RIGHT]))
            {
                FindShortestPath(passcode, x + 1, y, path + 'R', shortestPath);
            }
        }

        private string GetMd5HashForString(string input)
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
