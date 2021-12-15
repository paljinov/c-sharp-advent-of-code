using System;

namespace App.Tasks.Year2021.Day9
{
    public class HeightmapRepository
    {
        public int[,] GetHeightmap(string input)
        {
            string[] heightmapString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = heightmapString.Length;
            int columns = heightmapString[0].Length;
            int[,] heightmap = new int[rows, columns];

            for (int i = 0; i < heightmapString.Length; i++)
            {
                for (int j = 0; j < heightmapString[i].Length; j++)
                {
                    heightmap[i, j] = (int)char.GetNumericValue(heightmapString[i][j]);
                }
            }

            return heightmap;
        }
    }
}
