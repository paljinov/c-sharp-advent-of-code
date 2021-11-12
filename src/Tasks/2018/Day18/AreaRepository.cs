using System;

namespace App.Tasks.Year2018.Day18
{
    public class AreaRepository
    {
        public char[,] GetArea(string input)
        {
            string[] areaString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            char[,] area = new char[areaString.Length, areaString[0].Length];

            for (int i = 0; i < areaString.Length; i++)
            {
                for (int j = 0; j < areaString[i].Length; j++)
                {
                    area[i, j] = areaString[i][j];
                }
            }

            return area;
        }
    }
}
