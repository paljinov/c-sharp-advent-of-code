using System;

namespace App.Tasks.Year2021.Day12
{
    public class CaveSystemRepository
    {
        public (string, string)[] GetCaveSystem(string input)
        {
            string[] caveSystemString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            (string, string)[] caveSystem = new (string, string)[caveSystemString.Length];

            for (int i = 0; i < caveSystemString.Length; i++)
            {
                string[] connection = caveSystemString[i].Split('-');
                caveSystem[i] = (connection[0], connection[1]);
            }

            return caveSystem;
        }
    }
}
