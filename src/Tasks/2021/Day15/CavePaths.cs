using System.Collections.Generic;

namespace App.Tasks.Year2021.Day15
{
    public class CavePaths
    {
        public int CalculateLowestTotalRiskOfAnyPathFromTopLeftToBottomRight(int[,] riskLevelMap)
        {
            int lowestTotalRisk = 1000;

            (int X, int Y) startPosition = (0, 0);
            Dictionary<(int X, int Y), int> positionRiskCache = new Dictionary<(int X, int Y), int>();

            DoFindPathWithLowestTotalRisk(riskLevelMap, startPosition, positionRiskCache, 0, ref lowestTotalRisk);

            return lowestTotalRisk;
        }

        private void DoFindPathWithLowestTotalRisk(
            int[,] riskLevelMap,
            (int X, int Y) currentPosition,
            Dictionary<(int X, int Y), int> positionRiskCache,
            int risk,
            ref int lowestTotalRisk
        )
        {
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

            List<(int X, int Y)> nextStepPositions = GetNextStepPositions(riskLevelMap, currentPosition);
            foreach ((int X, int Y) nextPosition in nextStepPositions)
            {
                DoFindPathWithLowestTotalRisk(
                    riskLevelMap,
                    nextPosition,
                    positionRiskCache,
                    risk,
                    ref lowestTotalRisk
                );
            }
        }

        private List<(int X, int Y)> GetNextStepPositions(int[,] riskLevelMap, (int X, int Y) currentPosition)
        {
            List<(int X, int Y)> nextPositions = new List<(int X, int Y)>();

            if (currentPosition.X - 1 >= 0)
            {
                nextPositions.Add((currentPosition.X - 1, currentPosition.Y));
            }

            if (currentPosition.X + 1 < riskLevelMap.GetLength(0))
            {
                nextPositions.Add((currentPosition.X + 1, currentPosition.Y));
            }

            if (currentPosition.Y - 1 >= 0)
            {
                nextPositions.Add((currentPosition.X, currentPosition.Y - 1));
            }

            if (currentPosition.Y + 1 < riskLevelMap.GetLength(1))
            {
                nextPositions.Add((currentPosition.X, currentPosition.Y + 1));
            }

            return nextPositions;
        }
    }
}
