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

        private const int MOVE_MINUTES = 1;

        private const int SWITCH_TOOL_MINUTES = 7;

        private readonly Dictionary<Region, List<Tool>> regionTools = new Dictionary<Region, List<Tool>>()
        {
             { Region.Rocky, new List<Tool>() { Tool.ClimbingGear, Tool.Torch } },
             { Region.Wet, new List<Tool>() { Tool.ClimbingGear, Tool.Neither } },
             { Region.Narrow, new List<Tool>() { Tool.Torch, Tool.Neither } },
        };

        public int CalculateTotalRiskLevelForTheSmallestRectangleThatIncludesCaveMouthAndTargetCoordinates(
            int depth,
            (int X, int Y) targetPosition
        )
        {
            Region[,] caveRegions = GetCaveRegions(depth, targetPosition);

            int totalRiskLevel = CalculateTotalRiskLevel(caveRegions, targetPosition.Y, targetPosition.X);

            return totalRiskLevel;
        }

        public int CalculateFewestNumberOfMinutesNeededToReachTheTarget(int depth, (int X, int Y) targetPosition)
        {
            int fewestNumberOfMinutes = int.MaxValue;

            Region[,] caveRegions = GetCaveRegions(depth, targetPosition);

            // Key is position and tool, value is best minute in which position is reached
            Dictionary<string, int> stateCache = new Dictionary<string, int>();

            DoCalculateFewestNumberOfMinutesNeededToReachTheTarget(
                caveRegions, targetPosition, (0, 0), Tool.Torch, stateCache, 0, ref fewestNumberOfMinutes);

            return fewestNumberOfMinutes;
        }

        private Region[,] GetCaveRegions(int depth, (int X, int Y) targetPosition)
        {
            int[,] caveErosionIndex = GetCaveErosionIndex(depth, targetPosition);

            // X increasing to the right, and Y increasing downward
            Region[,] caveRegions = new Region[caveErosionIndex.GetLength(1), caveErosionIndex.GetLength(0)];

            for (int x = 0; x < caveRegions.GetLength(1); x++)
            {
                for (int y = 0; y < caveRegions.GetLength(0); y++)
                {
                    caveRegions[y, x] = (Region)(caveErosionIndex[x, y] % REGION_TYPE_MODULO);
                }
            }

            return caveRegions;
        }

        private int[,] GetCaveErosionIndex(int depth, (int X, int Y) targetPosition)
        {
            int rows = (targetPosition.X + 1) * 2;
            int columns = (targetPosition.Y + 1) * 2;

            long[,] caveGeologicIndex = new long[rows, columns];
            int[,] caveErosionIndex = new int[rows, columns];

            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if ((x == 0 && y == 0) || (x == targetPosition.X && y == targetPosition.Y))
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

        private void DoCalculateFewestNumberOfMinutesNeededToReachTheTarget(
            Region[,] caveRegions,
            (int X, int Y) targetPosition,
            (int X, int Y) currentPosition,
            Tool currentTool,
            Dictionary<string, int> stateCache,
            int totalMinutes,
            ref int fewestNumberOfMinutes
        )
        {
            // If better solution is already found
            if (totalMinutes >= fewestNumberOfMinutes)
            {
                return;
            }

            string state = StringifyState(currentPosition, currentTool);
            // If this position is already reached with same tool in equal or less minutes
            if (stateCache.ContainsKey(state) && totalMinutes >= stateCache[state])
            {
                return;
            }
            stateCache[state] = totalMinutes;

            // If target position is reached
            if (currentPosition == targetPosition)
            {
                if (currentTool != Tool.Torch)
                {
                    totalMinutes += SWITCH_TOOL_MINUTES;
                }

                fewestNumberOfMinutes = totalMinutes;
                return;
            }

            List<(int X, int Y)> adjacentPositions = GetAdjacentPositions(caveRegions, currentPosition);
            List<Tool> currentRegionTools = regionTools[caveRegions[currentPosition.X, currentPosition.Y]];

            foreach ((int X, int Y) adjacentPosition in adjacentPositions)
            {
                List<Tool> adjacentPositionTools = regionTools[caveRegions[adjacentPosition.X, adjacentPosition.Y]];
                IEnumerable<Tool> commonTools = currentRegionTools.Intersect(adjacentPositionTools);

                // Iterate through possible tools in next region
                foreach (Tool adjacentPositionTool in commonTools)
                {
                    int minutes = totalMinutes + MOVE_MINUTES;
                    // If current tool is different than the adjacent region tool switch tool
                    if (currentTool != adjacentPositionTool)
                    {
                        minutes += SWITCH_TOOL_MINUTES;
                    }

                    DoCalculateFewestNumberOfMinutesNeededToReachTheTarget(
                        caveRegions,
                        targetPosition,
                        adjacentPosition,
                        adjacentPositionTool,
                        stateCache,
                        minutes,
                        ref fewestNumberOfMinutes
                    );
                }
            }
        }

        private string StringifyState((int X, int Y) currentPosition, Tool currentTool)
        {
            return $"Position:({currentPosition.X},{currentPosition.Y}),Tool:{currentTool}";
        }

        private List<(int X, int Y)> GetAdjacentPositions(Region[,] caveRegions, (int X, int Y) currentPosition)
        {
            List<(int X, int Y)> adjacentPositions = new List<(int X, int Y)>();

            // Up
            if (currentPosition.X - 1 >= 0)
            {
                adjacentPositions.Add((currentPosition.X - 1, currentPosition.Y));
            }

            // Down
            if (currentPosition.X + 1 < caveRegions.GetLength(0))
            {
                adjacentPositions.Add((currentPosition.X + 1, currentPosition.Y));
            }

            // Left
            if (currentPosition.Y - 1 >= 0)
            {
                adjacentPositions.Add((currentPosition.X, currentPosition.Y - 1));
            }

            // Right
            if (currentPosition.Y + 1 < caveRegions.GetLength(1))
            {
                adjacentPositions.Add((currentPosition.X, currentPosition.Y + 1));
            }

            return adjacentPositions;
        }
    }
}
