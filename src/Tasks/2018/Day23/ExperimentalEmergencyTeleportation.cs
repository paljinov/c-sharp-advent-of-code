using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day23
{
    public class ExperimentalEmergencyTeleportation
    {
        private static readonly Position originPosition = new Position
        {
            X = 0,
            Y = 0,
            Z = 0
        };

        public int CountNanobotsWhichAreInRangeOfTheLargestSignalRadius(Nanobot[] nanobots)
        {
            Nanobot nanobotWithLargestSignalRadius = GetNanobotWithLargestSignalRadius(nanobots);

            int nanobotsWhichAreInRangeOfTheLargestSignalRadius =
                CountNanobotsWhichAreInRangeOfSignalRadius(nanobots, nanobotWithLargestSignalRadius);

            return nanobotsWhichAreInRangeOfTheLargestSignalRadius;
        }

        public int CalculateShortestManhattanDistanceForPositionInRangeOfLargestNumberOfNanobots(Nanobot[] nanobots)
        {
            int shortestManhattanDistance = int.MaxValue;

            IEnumerable<int> xs = nanobots.Select(n => n.Position.X);
            IEnumerable<int> ys = nanobots.Select(n => n.Position.Y);
            IEnumerable<int> zs = nanobots.Select(n => n.Position.Z);

            int maxX = xs.Max();
            int maxY = ys.Max();
            int maxZ = zs.Max();

            int minX = xs.Min();
            int minY = ys.Min();
            int minZ = zs.Min();

            int xRange = maxX - minX;
            int yRange = maxY - minY;
            int zRange = maxZ - minZ;

            int scanResolution = GetScanResolution(xRange, yRange, zRange);
            Position bestPosition = nanobots.First().Position;

            while (scanResolution >= 1)
            {
                int maxNanobotsWhichHavePositionInRange = 0;
                for (int x = minX; x <= maxX; x += scanResolution)
                {
                    for (int y = minY; y <= maxY; y += scanResolution)
                    {
                        for (int z = minZ; z <= maxZ; z += scanResolution)
                        {
                            Position position = new Position
                            {
                                X = x,
                                Y = y,
                                Z = z
                            };

                            int nanobotsWhichHavePositionInRange =
                                CountNanobotsWhichHavePositionInRangeForScanResolution(
                                    nanobots, position, scanResolution);

                            // If this position is in range of max nanobots
                            if (nanobotsWhichHavePositionInRange >= maxNanobotsWhichHavePositionInRange)
                            {
                                int positionManhattanDistance = CalculateManhattanDistance(position, originPosition);

                                // If this position is only one in range of max nanobots
                                // or if it has the shortest manhattan distance
                                if (nanobotsWhichHavePositionInRange > maxNanobotsWhichHavePositionInRange
                                    || positionManhattanDistance < shortestManhattanDistance)
                                {
                                    bestPosition = position;
                                    shortestManhattanDistance = positionManhattanDistance;
                                    maxNanobotsWhichHavePositionInRange = nanobotsWhichHavePositionInRange;
                                }
                            }
                        }
                    }
                }

                minX = bestPosition.X - scanResolution;
                maxX = bestPosition.X + scanResolution;
                minY = bestPosition.Y - scanResolution;
                maxY = bestPosition.Y + scanResolution;
                minZ = bestPosition.Z - scanResolution;
                maxZ = bestPosition.Z + scanResolution;
                scanResolution /= 2;
            }

            return shortestManhattanDistance;
        }

        private Nanobot GetNanobotWithLargestSignalRadius(Nanobot[] nanobots)
        {
            int largestSignalRadius = 0;
            Nanobot nanobotWithLargestSignalRadius = nanobots.First();

            foreach (Nanobot nanobot in nanobots)
            {
                if (nanobot.SignalRadius > largestSignalRadius)
                {
                    largestSignalRadius = nanobot.SignalRadius;
                    nanobotWithLargestSignalRadius = nanobot;
                }
            }

            return nanobotWithLargestSignalRadius;
        }

        private int CountNanobotsWhichAreInRangeOfSignalRadius(
            Nanobot[] nanobots,
            Nanobot nanobotWithLargestSignalRadius
        )
        {
            int nanobotsWhichAreInRangeOfTheLargestSignalRadius = 0;

            foreach (Nanobot nanobot in nanobots)
            {
                int manhattanDistance =
                    CalculateManhattanDistance(nanobotWithLargestSignalRadius.Position, nanobot.Position);

                if (nanobotWithLargestSignalRadius.SignalRadius >= manhattanDistance)
                {
                    nanobotsWhichAreInRangeOfTheLargestSignalRadius++;
                }
            }

            return nanobotsWhichAreInRangeOfTheLargestSignalRadius;
        }

        private int CalculateManhattanDistance(Position a, Position b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }

        private int CountNanobotsWhichHavePositionInRangeForScanResolution(
            Nanobot[] nanobots,
            Position position,
            int scanResolution
        )
        {
            int nanobotsWhichHavePositionInRange = 0;

            foreach (Nanobot nanobot in nanobots)
            {
                int manhattanDistance = CalculateManhattanDistance(position, nanobot.Position);

                if (manhattanDistance - nanobot.SignalRadius < scanResolution)
                {
                    nanobotsWhichHavePositionInRange++;
                }
            }

            return nanobotsWhichHavePositionInRange;
        }

        private int GetScanResolution(int xRange, int yRange, int zRange)
        {
            int exponent = 0;

            int scanResolution = 0;
            while (scanResolution < xRange || scanResolution < yRange || scanResolution < zRange)
            {
                exponent++;
                scanResolution = (int)Math.Pow(2, exponent);
            }

            return scanResolution;
        }
    }
}
