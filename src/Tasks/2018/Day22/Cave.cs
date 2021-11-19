namespace App.Tasks.Year2018.Day22
{
    public class Cave
    {
        private const int X_GEOLOGIC_INDEX_MULTIPLICAND = 16807;

        private const int Y_GEOLOGIC_INDEX_MULTIPLICAND = 48271;

        private const int EROSION_MODULO = 20183;

        private const int REGION_TYPE_MODULO = 3;

        public int CalculateTotalRiskLevelForTheSmallestRectangleThatIncludesCaveMouthAndTargetCoordinates(
            int depth,
            (int X, int Y) targetCoordinates
        )
        {
            int[,] caveErosionIndex = GetCaveErosionIndex(depth, targetCoordinates);
            Region[,] caveRegions = GetCaveRegions(caveErosionIndex);

            int totalRiskLevel = CalculateTotalRiskLevel(caveRegions);

            return totalRiskLevel;
        }

        private int[,] GetCaveErosionIndex(int depth, (int X, int Y) targetCoordinates)
        {
            int rows = targetCoordinates.X + 1;
            int columns = targetCoordinates.Y + 1;

            long[,] caveGeologicIndex = new long[rows, columns];
            int[,] caveErosionIndex = new int[rows, columns];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if ((x == 0 && y == 0) || (x == targetCoordinates.X && y == targetCoordinates.Y))
                    {
                        caveGeologicIndex[x, y] = 0;
                    }
                    else if (y == 0)
                    {
                        caveGeologicIndex[x, y] = x * X_GEOLOGIC_INDEX_MULTIPLICAND;
                    }
                    else if (x == 0)
                    {
                        caveGeologicIndex[x, y] = y * Y_GEOLOGIC_INDEX_MULTIPLICAND;
                    }
                    else
                    {
                        caveGeologicIndex[x, y] = caveErosionIndex[x - 1, y] * caveErosionIndex[x, y - 1];
                    }

                    caveErosionIndex[x, y] = (int)((depth + caveGeologicIndex[x, y]) % EROSION_MODULO);
                }
            }

            return caveErosionIndex;
        }

        private Region[,] GetCaveRegions(int[,] caveErosionIndex)
        {
            Region[,] caveRegions = new Region[caveErosionIndex.GetLength(0), caveErosionIndex.GetLength(1)];

            for (int x = 0; x < caveRegions.GetLength(0); x++)
            {
                for (int y = 0; y < caveRegions.GetLength(1); y++)
                {
                    caveRegions[x, y] = (Region)(caveErosionIndex[x, y] % REGION_TYPE_MODULO);
                }
            }

            return caveRegions;
        }

        private int CalculateTotalRiskLevel(Region[,] caveRegions)
        {
            int totalRiskLevel = 0;

            for (int x = 0; x < caveRegions.GetLength(0); x++)
            {
                for (int y = 0; y < caveRegions.GetLength(1); y++)
                {
                    totalRiskLevel += (int)caveRegions[x, y];
                }
            }

            return totalRiskLevel;
        }
    }
}
