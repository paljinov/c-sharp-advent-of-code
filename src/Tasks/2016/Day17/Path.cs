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
            List<string> paths = new List<string>();
            FindShortestPath(passcode, START_X, START_Y, string.Empty, paths);

            string shortestPath = paths[0];
            foreach (string path in paths)
            {
                if (path.Length < shortestPath.Length)
                {
                    shortestPath = path;
                }
            }

            return shortestPath;
        }

        public int FindLengthOfLongestPathToVault(string passcode)
        {
            List<string> paths = new List<string>();
            FindShortestPath(passcode, START_X, START_Y, string.Empty, paths);

            int lengthOfLongestPathToVault = 0;
            foreach (string path in paths)
            {
                if (path.Length > lengthOfLongestPathToVault)
                {
                    lengthOfLongestPathToVault = path.Length;
                }
            }

            return lengthOfLongestPathToVault;
        }

        private void FindShortestPath(string passcode, int x, int y, string path, List<string> paths)
        {
            if (x == VAULT_X && y == VAULT_Y)
            {
                paths.Add(path);
                return;
            }

            string hash = GetMd5HashForString(passcode + path);

            // If it is possible to go up
            if (y - 1 >= 0 && openDoorsCharacters.Contains(hash[Direction.Up.Index]))
            {
                FindShortestPath(passcode, x, y - 1, path + Direction.Up.Letter, paths);
            }

            // If it is possible to go down
            if (y + 1 <= VAULT_Y && openDoorsCharacters.Contains(hash[Direction.Down.Index]))
            {
                FindShortestPath(passcode, x, y + 1, path + Direction.Down.Letter, paths);
            }

            // If it is possible to go left
            if (x - 1 >= 0 && openDoorsCharacters.Contains(hash[Direction.Left.Index]))
            {
                FindShortestPath(passcode, x - 1, y, path + Direction.Left.Letter, paths);
            }

            // If it is possible to go right
            if (x + 1 <= VAULT_X && openDoorsCharacters.Contains(hash[Direction.Right.Index]))
            {
                FindShortestPath(passcode, x + 1, y, path + Direction.Right.Letter, paths);
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
