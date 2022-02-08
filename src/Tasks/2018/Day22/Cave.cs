using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day22
{
    public class Cave
    {
        private const int X_GEOLOGIC_INDEX_MULTIPLICAND = 16807;

        private const int Y_GEOLOGIC_INDEX_MULTIPLICAND = 48271;

        private const int EROSION_MODULO = 20183;

        private const int REGION_TYPE_MODULO = 3;

        private const int CAVE_SIZE_MULTIPLIER = 3;

        private const int MOVE_MINUTES = 1;

        private const int SWITCH_TOOL_MINUTES = 7;

        private readonly Dictionary<Region, List<Tool>> regionTools = new Dictionary<Region, List<Tool>>()
        {
            { Region.Rocky, new List<Tool>() { Tool.ClimbingGear, Tool.Torch } },
            { Region.Wet, new List<Tool>() { Tool.ClimbingGear, Tool.Neither } },
            { Region.Narrow, new List<Tool>() { Tool.Torch, Tool.Neither } },
        };

        private static readonly (int X, int Y)[] steps = new (int X, int Y)[] {
            // Down
            (1, 0),
            // Right
            (0, 1),
            // Up
            (-1, 0),
            // Left
            (0, -1)
        };

        public int CalculateTotalRiskLevelForTheSmallestRectangleThatIncludesCaveMouthAndTargetCoordinates(
            int depth,
            (int X, int Y) targetRegion
        )
        {
            Region[,] caveRegions = GetCaveRegions(depth, targetRegion);

            int totalRiskLevel = CalculateTotalRiskLevel(caveRegions, targetRegion.X, targetRegion.Y);

            return totalRiskLevel;
        }

        public int CalculateFewestNumberOfMinutesNeededToReachTheTarget(int depth, (int X, int Y) targetRegion)
        {
            Region[,] caveRegions = GetCaveRegions(depth, targetRegion);

            (int X, int Y) caveMouth = (0, 0);

            // Fewest minutes needed to visit each region
            Dictionary<((int X, int Y) Region, Tool Tool), int> regionsFewestMinutes =
                new Dictionary<((int X, int Y) Region, Tool Tool), int>()
            {
                { (caveMouth, Tool.Torch), 0 }
            };
            Dictionary<((int X, int Y) Region, Tool Tool), int> adjacentRegionsToVisit =
                regionsFewestMinutes.ToDictionary(rfm => rfm.Key, rfm => rfm.Value);

            // Using Dijkstra's algorithm to visit regions
            while (adjacentRegionsToVisit.Count > 0)
            {
                // Visit region with fewest minutes
                ((int X, int Y) Region, Tool Tool) currentRegionWithTool =
                    adjacentRegionsToVisit.MinBy(ap => ap.Value).Key;

                (int X, int Y) currentRegion = currentRegionWithTool.Region;
                Tool currentTool = currentRegionWithTool.Tool;
                int currentRegionMinutes = adjacentRegionsToVisit[currentRegionWithTool];

                // Remove current region with tool from visit list
                adjacentRegionsToVisit.Remove(currentRegionWithTool);

                List<(int X, int Y)> adjacentRegions = GetAdjacentRegions(caveRegions, currentRegion);
                List<Tool> currentRegionTools = regionTools[caveRegions[currentRegion.X, currentRegion.Y]];

                foreach ((int X, int Y) adjacentRegion in adjacentRegions)
                {
                    List<Tool> adjacentRegionTools = regionTools[caveRegions[adjacentRegion.X, adjacentRegion.Y]];
                    IEnumerable<Tool> commonTools = currentRegionTools.Intersect(adjacentRegionTools);

                    // Iterate through possible tools in adjacent region
                    foreach (Tool adjacentRegionTool in commonTools)
                    {
                        int adjacentRegionMinutes = currentRegionMinutes + MOVE_MINUTES;
                        // If current tool is different than the adjacent region tool switch tool
                        if (currentTool != adjacentRegionTool)
                        {
                            adjacentRegionMinutes += SWITCH_TOOL_MINUTES;
                        }

                        if (adjacentRegion == targetRegion && adjacentRegionTool != Tool.Torch)
                        {
                            continue;
                        }

                        ((int X, int Y), Tool Tool) adjacentRegionWithTool =
                            ((adjacentRegion.X, adjacentRegion.Y), adjacentRegionTool);
                        // If this region is already reached with tool in equal number or less minutes
                        if (regionsFewestMinutes.ContainsKey(adjacentRegionWithTool)
                            && adjacentRegionMinutes >= regionsFewestMinutes[adjacentRegionWithTool])
                        {
                            continue;
                        }

                        regionsFewestMinutes[adjacentRegionWithTool] = adjacentRegionMinutes;
                        adjacentRegionsToVisit[adjacentRegionWithTool] = adjacentRegionMinutes;
                    }
                }
            }

            // Fewest minutes needed to visit target region
            int fewestNumberOfMinutesNeededToReachTheTarget =
                regionsFewestMinutes[((targetRegion.X, targetRegion.Y), Tool.Torch)];

            return fewestNumberOfMinutesNeededToReachTheTarget;
        }

        private Region[,] GetCaveRegions(int depth, (int X, int Y) targetRegion)
        {
            int[,] caveErosionIndex = GetCaveErosionIndex(depth, targetRegion);

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

        private int[,] GetCaveErosionIndex(int depth, (int X, int Y) targetRegion)
        {
            int rows = targetRegion.X * CAVE_SIZE_MULTIPLIER;
            int columns = targetRegion.Y * CAVE_SIZE_MULTIPLIER;

            long[,] caveGeologicIndex = new long[rows, columns];
            int[,] caveErosionIndex = new int[rows, columns];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if ((x == 0 && y == 0) || (x == targetRegion.X && y == targetRegion.Y))
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

        private int CalculateTotalRiskLevel(Region[,] caveRegions, int rows, int columns)
        {
            int totalRiskLevel = 0;

            for (int x = 0; x <= rows; x++)
            {
                for (int y = 0; y <= columns; y++)
                {
                    totalRiskLevel += (int)caveRegions[x, y];
                }
            }

            return totalRiskLevel;
        }

        private List<(int X, int Y)> GetAdjacentRegions(Region[,] caveRegions, (int X, int Y) currentRegion)
        {
            List<(int X, int Y)> adjacentRegions = new List<(int X, int Y)>();

            foreach ((int X, int Y) step in steps)
            {
                (int X, int Y) adjacentRegion = (currentRegion.X + step.X, currentRegion.Y + step.Y);

                // If adjacent region is inside cave boundaries
                if (adjacentRegion.X >= 0 && adjacentRegion.X < caveRegions.GetLength(0)
                    && adjacentRegion.Y >= 0 && adjacentRegion.Y < caveRegions.GetLength(1))
                {
                    adjacentRegions.Add(adjacentRegion);
                }
            }

            return adjacentRegions;
        }
    }
}
