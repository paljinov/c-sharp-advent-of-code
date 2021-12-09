using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day9
{
    public class Heightmap
    {
        private const int NOT_ANY_BASIN_HEIGHT = 9;

        public int CalculateRiskLevelsSumOfAllHeightmapLowPoints(int[,] heightmap)
        {
            List<(int X, int Y)> lowPointsLocations = FindLowPointsLocations(heightmap);

            int riskLevelsSum = 0;
            foreach ((int x, int y) in lowPointsLocations)
            {
                riskLevelsSum += heightmap[x, y] + 1;
            }

            return riskLevelsSum;
        }

        public int CalculateProductOfThreeLargestBasinsSizes(int[,] heightmap)
        {
            List<int> basinsSizes = new List<int>();

            List<(int X, int Y)> lowPointsLocations = FindLowPointsLocations(heightmap);

            foreach ((int x, int y) in lowPointsLocations)
            {
                HashSet<(int, int)> basin = new HashSet<(int, int)>();
                FindBasin(heightmap, x, y, basin);
                basinsSizes.Add(basin.Count);
            }

            basinsSizes.Sort();
            IEnumerable<int> threeLargestBasinsSizes = basinsSizes.TakeLast(3);
            int productOfThreeLargestBasinsSizes = threeLargestBasinsSizes.Aggregate((x, y) => x * y);

            return productOfThreeLargestBasinsSizes;
        }

        private List<(int X, int Y)> FindLowPointsLocations(int[,] heightmap)
        {
            List<(int, int)> lowPoints = new List<(int, int)>();

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

                    lowPoints.Add((i, j));
                }
            }

            return lowPoints;
        }

        private void FindBasin(int[,] heightmap, int i, int j, HashSet<(int, int)> basin)
        {
            if (heightmap[i, j] == NOT_ANY_BASIN_HEIGHT)
            {
                return;
            }

            basin.Add((i, j));

            // Up
            if (i - 1 >= 0 && heightmap[i - 1, j] - heightmap[i, j] > 0)
            {
                FindBasin(heightmap, i - 1, j, basin);
            }

            // Down
            if (i + 1 < heightmap.GetLength(0) && heightmap[i + 1, j] - heightmap[i, j] > 0)
            {
                FindBasin(heightmap, i + 1, j, basin);
            }

            // Left
            if (j - 1 >= 0 && heightmap[i, j - 1] - heightmap[i, j] > 0)
            {
                FindBasin(heightmap, i, j - 1, basin);
            }

            // Right
            if (j + 1 < heightmap.GetLength(1) && heightmap[i, j + 1] - heightmap[i, j] > 0)
            {
                FindBasin(heightmap, i, j + 1, basin);
            }
        }
    }
}
