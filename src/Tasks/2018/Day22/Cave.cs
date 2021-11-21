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
            int[,] caveErosionIndex = GetCaveErosionIndex(depth, targetPosition);
            Region[,] caveRegions = GetCaveRegions(caveErosionIndex);

            int totalRiskLevel = CalculateTotalRiskLevel(caveRegions);

            return totalRiskLevel;
        }

        public int CalculateFewestNumberOfMinutesNeededToReachTheTarget(int depth, (int X, int Y) targetPosition)
        {
            int fewestNumberOfMinutes = int.MaxValue;

            int[,] caveErosionIndex = GetCaveErosionIndex(depth, targetPosition);
            Region[,] caveRegions = GetCaveRegions(caveErosionIndex);

            // Key is position and tool, value is best minute in which position is reached
            Dictionary<string, int> positionStateCache = new Dictionary<string, int>();

            DoCalculateFewestNumberOfMinutesNeededToReachTheTarget(
                caveRegions, targetPosition, (0, 0), Tool.Torch, positionStateCache, 0, ref fewestNumberOfMinutes);

            return fewestNumberOfMinutes;
        }

        private int[,] GetCaveErosionIndex(int depth, (int X, int Y) targetPosition)
        {
            int rows = targetPosition.X + 1;
            int columns = targetPosition.Y + 1;

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

        private void DoCalculateFewestNumberOfMinutesNeededToReachTheTarget(
            Region[,] caveRegions,
            (int X, int Y) targetPosition,
            (int X, int Y) currentPosition,
            Tool currentTool,
            Dictionary<string, int> positionStateCache,
            int minutes,
            ref int fewestNumberOfMinutes)
        {
            // If better solution is already found
            if (minutes >= fewestNumberOfMinutes)
            {
                return;
            }

            string positionState = StringifyPositionState(currentPosition, currentTool);
            // If this position is already reached with same tool in equal or less minutes
            if (positionStateCache.ContainsKey(positionState) && minutes >= positionStateCache[positionState])
            {
                return;
            }
            positionStateCache[positionState] = minutes;

            // If target position is reached
            if (currentPosition == targetPosition)
            {
                if (currentTool != Tool.Torch)
                {
                    minutes += SWITCH_TOOL_MINUTES;
                }

                fewestNumberOfMinutes = minutes;
                return;
            }

            List<(int X, int Y)> adjacentPositions = GetAdjacentPositions(caveRegions, currentPosition);
            List<Tool> currentRegionTools = regionTools[caveRegions[currentPosition.X, currentPosition.Y]];

            foreach ((int X, int Y) adjacentPosition in adjacentPositions)
            {
                List<Tool> nextRegionTools = regionTools[caveRegions[adjacentPosition.X, adjacentPosition.Y]];
                IEnumerable<Tool> commonTools = currentRegionTools.Intersect(nextRegionTools);

                // Iterate through possible tools in next region
                foreach (Tool nextRegionTool in commonTools)
                {
                    int nextMinutes = minutes + MOVE_MINUTES;

                    // If current tool is different than next region tool
                    if (currentTool != nextRegionTool)
                    {
                        nextMinutes += SWITCH_TOOL_MINUTES;
                    }

                    DoCalculateFewestNumberOfMinutesNeededToReachTheTarget(
                        caveRegions,
                        targetPosition,
                        adjacentPosition,
                        nextRegionTool,
                        positionStateCache,
                        nextMinutes,
                        ref fewestNumberOfMinutes
                    );
                }
            }
        }

        private string StringifyPositionState((int X, int Y) currentPosition, Tool currentTool)
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
