using System.Collections.Generic;

namespace App.Tasks.Year2021.Day15
{
    public class CavePaths
    {
        private const int WRAP_BACK_RISK_LEVEL = 9;

        public int CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRight(int[,] riskLevelMap)
        {
            int lowestTotalRisk = int.MaxValue;

            (int X, int Y) startPosition = (0, 0);
            Dictionary<(int X, int Y), int> positionRiskCache = new Dictionary<(int X, int Y), int>();

            // Go just down and right
            DoFindPathWithLowestTotalRisk(riskLevelMap, startPosition, positionRiskCache, 0, ref lowestTotalRisk, true);
            // Check potentially better solution with up and left paths too
            // when lowest total risk is already initialized
            positionRiskCache.Clear();
            DoFindPathWithLowestTotalRisk(riskLevelMap, startPosition, positionRiskCache, 0, ref lowestTotalRisk);

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
            Dictionary<(int X, int Y), int> positionRiskCache,
            int risk,
            ref int lowestTotalRisk,
            bool justDownAndRight = false
        )
        {
            // The starting position is never entered, so its risk is not counted
            if (currentPosition != (0, 0))
            {
                risk += riskLevelMap[currentPosition.X, currentPosition.Y];
            }

            // If better solution is already found
            if (risk >= lowestTotalRisk)
            {
                return;
            }

            // If this position is already reached with equal or less risk
            if (positionRiskCache.ContainsKey(currentPosition) && risk >= positionRiskCache[currentPosition])
            {
                return;
            }
            positionRiskCache[currentPosition] = risk;

            // If end location is reached
            if (currentPosition == (riskLevelMap.GetLength(0) - 1, riskLevelMap.GetLength(1) - 1))
            {
                lowestTotalRisk = risk;
                return;
            }

            List<(int X, int Y)> nextStepPositions =
                GetNextStepPositions(riskLevelMap, currentPosition, justDownAndRight);

            foreach ((int X, int Y) nextPosition in nextStepPositions)
            {
                DoFindPathWithLowestTotalRisk(
                    riskLevelMap,
                    nextPosition,
                    positionRiskCache,
                    risk,
                    ref lowestTotalRisk,
                    justDownAndRight
                );
            }
        }

        private List<(int X, int Y)> GetNextStepPositions(
            int[,] riskLevelMap,
            (int X, int Y) currentPosition,
            bool justDownAndRight
        )
        {
            List<(int X, int Y)> nextPositions = new List<(int X, int Y)>();

            // Down
            if (currentPosition.X + 1 < riskLevelMap.GetLength(0))
            {
                nextPositions.Add((currentPosition.X + 1, currentPosition.Y));
            }

            // Right
            if (currentPosition.Y + 1 < riskLevelMap.GetLength(1))
            {
                nextPositions.Add((currentPosition.X, currentPosition.Y + 1));
            }

            if (!justDownAndRight)
            {
                // Up
                if (currentPosition.X - 1 >= 0)
                {
                    nextPositions.Add((currentPosition.X - 1, currentPosition.Y));
                }

                // Left
                if (currentPosition.Y - 1 >= 0)
                {
                    nextPositions.Add((currentPosition.X, currentPosition.Y - 1));
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
