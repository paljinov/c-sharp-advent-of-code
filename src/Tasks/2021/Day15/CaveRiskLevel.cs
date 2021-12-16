using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day15
{
    public class CaveRiskLevel
    {
        private const int WRAP_BACK_RISK_LEVEL = 9;

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

        public int CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRight(int[,] riskLevelMap)
        {
            int lowestTotalRisk = DoFindPathWithLowestTotalRisk(riskLevelMap);

            return lowestTotalRisk;
        }

        public int CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRightForLargerMap(
            int[,] riskLevelMap,
            int timesLarger
        )
        {
            int[,] largerRiskLevelMap = GetLargerRiskLevelMap(riskLevelMap, timesLarger);
            int lowestTotalRisk = DoFindPathWithLowestTotalRisk(largerRiskLevelMap);

            return lowestTotalRisk;
        }

        private int DoFindPathWithLowestTotalRisk(int[,] riskLevelMap)
        {
            (int X, int Y) startPosition = (0, 0);
            (int X, int Y) endPosition = (riskLevelMap.GetLength(0) - 1, riskLevelMap.GetLength(1) - 1);

            // Lowest risk of every visited position
            Dictionary<(int X, int Y), int> positionsLowestRisk = new Dictionary<(int X, int Y), int>
            {
                // The starting position is never entered, so its risk is not counted
                { startPosition, 0 }
            };
            Dictionary<(int X, int Y), int> nextPositionsToVisit =
                positionsLowestRisk.ToDictionary(plr => plr.Key, pr => pr.Value);

            // Using Dijkstra's algorithm to visit positions
            while (nextPositionsToVisit.Count > 0)
            {
                // Visit position with lowest risk
                (int X, int Y) currentPosition = nextPositionsToVisit.MinBy(np => np.Value).Key;
                int currentPositionRisk = nextPositionsToVisit[currentPosition];
                // Remove current position from visit list
                nextPositionsToVisit.Remove(currentPosition);

                List<(int X, int Y)> nextPositions = GetNextStepPositions(riskLevelMap, currentPosition);
                foreach ((int X, int Y) nextPosition in nextPositions)
                {
                    int nextPositionRisk = currentPositionRisk + riskLevelMap[nextPosition.X, nextPosition.Y];

                    // If this position is already reached with equal or less risk
                    if (positionsLowestRisk.ContainsKey(nextPosition)
                        && nextPositionRisk >= positionsLowestRisk[nextPosition])
                    {
                        continue;
                    }

                    positionsLowestRisk[nextPosition] = nextPositionRisk;
                    nextPositionsToVisit[nextPosition] = nextPositionRisk;
                }
            }

            // End position lowest risk
            int lowestTotalRisk = positionsLowestRisk[endPosition];

            return lowestTotalRisk;
        }

        private List<(int X, int Y)> GetNextStepPositions(int[,] riskLevelMap, (int X, int Y) currentPosition)
        {
            List<(int X, int Y)> nextPositions = new List<(int X, int Y)>();

            foreach ((int X, int Y) step in steps)
            {
                (int X, int Y) nextPosition = (currentPosition.X + step.X, currentPosition.Y + step.Y);

                // If next position is inside map boundaries
                if (nextPosition.X >= 0 && nextPosition.X < riskLevelMap.GetLength(0)
                    && nextPosition.Y >= 0 && nextPosition.Y < riskLevelMap.GetLength(1))
                {
                    nextPositions.Add(nextPosition);
                }
            }

            return nextPositions;
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
