using System.Collections.Generic;

namespace App.Tasks.Year2021.Day15
{
    public class CavePaths
    {
        private const int WRAP_BACK_RISK_LEVEL = 9;

        private static readonly (int X, int Y)[] steps = new (int X, int Y)[] {
            (1, 0),     // Down
            (0, 1),     // Right
            (-1, 0),    // Up
            (0, -1)     // Left
        };

        public int CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRight(int[,] riskLevelMap)
        {
            int lowestTotalRisk = int.MaxValue;

            (int X, int Y) startPosition = (0, 0);
            Dictionary<(int X, int Y), int> positionsLowestRisk = new Dictionary<(int X, int Y), int>
            {
                // The starting position is never entered, so its risk is not counted
                { startPosition, 0 }
            };

            DoFindPathWithLowestTotalRisk(riskLevelMap, startPosition, positionsLowestRisk, ref lowestTotalRisk);

            return lowestTotalRisk;
        }

        public int CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRightForLargerMap(
            int[,] riskLevelMap,
            int timesLarger
        )
        {
            int[,] largerRiskLevelMap = GetLargerRiskLevelMap(riskLevelMap, timesLarger);
            int lowestTotalRisk = CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRight(largerRiskLevelMap);

            return lowestTotalRisk;
        }

        private void DoFindPathWithLowestTotalRisk(
            int[,] riskLevelMap,
            (int X, int Y) currentPosition,
            Dictionary<(int X, int Y), int> positionsLowestRisk,
            ref int lowestTotalRisk
        )
        {
            foreach ((int X, int Y) step in steps)
            {
                (int X, int Y) nextPosition = (currentPosition.X + step.X, currentPosition.Y + step.Y);

                // If next position is outside map boundaries
                if (nextPosition.X < 0 || nextPosition.X >= riskLevelMap.GetLength(0)
                    || nextPosition.Y < 0 || nextPosition.Y >= riskLevelMap.GetLength(1))
                {
                    continue;
                }

                int nextPositionRisk =
                    positionsLowestRisk[currentPosition] + riskLevelMap[nextPosition.X, nextPosition.Y];

                // If next position is already visited with lower risk
                if (positionsLowestRisk.ContainsKey(nextPosition)
                    && nextPositionRisk >= positionsLowestRisk[nextPosition])
                {
                    continue;
                }

                positionsLowestRisk[nextPosition] = nextPositionRisk;

                // If end location is reached
                if (nextPosition == (riskLevelMap.GetLength(0) - 1, riskLevelMap.GetLength(1) - 1))
                {
                    lowestTotalRisk = nextPositionRisk;
                    continue;
                }

                DoFindPathWithLowestTotalRisk(
                    riskLevelMap,
                    nextPosition,
                    positionsLowestRisk,
                    ref lowestTotalRisk
                );
            }
        }

        private int[,] GetLargerRiskLevelMap(int[,] riskLevelMap, int timesLarger)
        {
            int rows = riskLevelMap.GetLength(0);
            int columns = riskLevelMap.GetLength(1);

            int largerRiskMapRows = timesLarger * rows;
            int largerRiskMapColumns = timesLarger * columns;
            int[,] largerRiskLevelMap = new int[largerRiskMapRows, largerRiskMapColumns];

            for (int i = 0; i < largerRiskMapRows; i++)
            {
                int k = i % rows;

                for (int j = 0; j < largerRiskMapColumns; j++)
                {
                    int h = j % columns;

                    // All of the risk levels are 1 higher than the tile immediately up or left of it
                    int riskLevel = riskLevelMap[k, h] + (i / rows) + (j / columns);
                    // Risk levels above 9 wrap back
                    if (riskLevel > WRAP_BACK_RISK_LEVEL)
                    {
                        riskLevel -= WRAP_BACK_RISK_LEVEL;
                    }

                    largerRiskLevelMap[i, j] = riskLevel;
                }
            }

            return largerRiskLevelMap;
        }
    }
}
