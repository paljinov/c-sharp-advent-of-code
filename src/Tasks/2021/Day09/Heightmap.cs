using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day9
{
    public class Heightmap
    {
        public int CalculateRiskLevelsSumOfAllHeightmapLowPoints(int[,] heightmap)
        {
            List<int> lowPoints = FindLowPoints(heightmap);

            int riskLevelsSum = lowPoints.Sum() + lowPoints.Count;

            return riskLevelsSum;
        }

        private List<int> FindLowPoints(int[,] heightmap)
        {
            List<int> lowPoints = new List<int>();

            for (int i = 0; i < heightmap.GetLength(0); i++)
            {
                for (int j = 0; j < heightmap.GetLength(1); j++)
                {
                    // Up
                    if (i - 1 >= 0 && heightmap[i, j] >= heightmap[i - 1, j])
                    {
                        continue;
                    }

                    // Down
                    if (i + 1 < heightmap.GetLength(0) && heightmap[i, j] >= heightmap[i + 1, j])
                    {
                        continue;
                    }

                    // Left
                    if (j - 1 >= 0 && heightmap[i, j] >= heightmap[i, j - 1])
                    {
                        continue;
                    }

                    // Right
                    if (j + 1 < heightmap.GetLength(1) && heightmap[i, j] >= heightmap[i, j + 1])
                    {
                        continue;
                    }

                    lowPoints.Add(heightmap[i, j]);
                }
            }

            return lowPoints;
        }
    }
}
