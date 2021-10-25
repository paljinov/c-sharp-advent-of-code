using System;

namespace App.Tasks.Year2019.Day18
{
    public class TunnelsMapRepository
    {
        public char[,] GetTunnelsMap(string input)
        {
            string[] tunnelsMapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            char[,] tunnelsMap = new char[tunnelsMapString.Length, tunnelsMapString[0].Length];

            for (int i = 0; i < tunnelsMapString.Length; i++)
            {
                for (int j = 0; j < tunnelsMapString[i].Length; j++)
                {
                    tunnelsMap[i, j] = tunnelsMapString[i][j];
                }
            }

            return tunnelsMap;
        }
    }
}
