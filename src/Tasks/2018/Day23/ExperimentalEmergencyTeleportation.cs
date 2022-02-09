using System;
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
            int maxNanobotsWhichHavePositionInRange = 0;
            Position bestPosition = nanobots.First().Position;

            (Position min, Position max) = GetPositionsRange(nanobots);
            int xRange = max.X - min.X;
            int yRange = max.Y - min.Y;
            int zRange = max.Z - min.Z;

            while (xRange >= 1 && yRange >= 1 && zRange >= 1)
            {
                for (int x = min.X; x <= max.X; x += xRange)
                {
                    for (int y = min.Y; y <= max.Y; y += yRange)
                    {
                        for (int z = min.Z; z <= max.Z; z += zRange)
                        {
                            Position position = new Position
                            {
                                X = x,
                                Y = y,
                                Z = z
                            };

                            int nanobotsWhichHavePositionInRange =
                                CountNanobotsWhichHavePositionInRange(nanobots, position);

                            if (nanobotsWhichHavePositionInRange > maxNanobotsWhichHavePositionInRange)
                            {
                                shortestManhattanDistance = Math.Abs(x - originPosition.X)
                                    + Math.Abs(y - originPosition.Y)
                                    + Math.Abs(z - originPosition.Z);

                                bestPosition = position;
                                maxNanobotsWhichHavePositionInRange = nanobotsWhichHavePositionInRange;
                            }
                        }
                    }
                }

                min.X = bestPosition.X - xRange / 4;
                min.Y = bestPosition.Y - yRange / 4;
                min.Z = bestPosition.Z - zRange / 4;

                max.X = bestPosition.X + xRange / 4;
                max.Y = bestPosition.Y + yRange / 4;
                max.Z = bestPosition.Z + zRange / 4;

                xRange = max.X - min.X;
                yRange = max.Y - min.Y;
                zRange = max.Z - min.Z;
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
                int manhattanDistance = Math.Abs(nanobotWithLargestSignalRadius.Position.X - nanobot.Position.X)
                    + Math.Abs(nanobotWithLargestSignalRadius.Position.Y - nanobot.Position.Y)
                    + Math.Abs(nanobotWithLargestSignalRadius.Position.Z - nanobot.Position.Z);

                if (nanobotWithLargestSignalRadius.SignalRadius >= manhattanDistance)
                {
                    nanobotsWhichAreInRangeOfTheLargestSignalRadius++;
                }
            }

            return nanobotsWhichAreInRangeOfTheLargestSignalRadius;
        }

        private (Position Min, Position Max) GetPositionsRange(Nanobot[] nanobots)
        {
            Position min = new Position
            {
                X = 0,
                Y = 0,
                Z = 0
            };

            Position max = new Position
            {
                X = 0,
                Y = 0,
                Z = 0
            };

            foreach (Nanobot nanobot in nanobots)
            {
                if (nanobot.Position.X < min.X)
                {
                    min.X = nanobot.Position.X;
                }
                if (nanobot.Position.Y < min.Y)
                {
                    min.Y = nanobot.Position.Y;
                }
                if (nanobot.Position.Z < min.Z)
                {
                    min.Z = nanobot.Position.Z;
                }

                if (nanobot.Position.X > max.X)
                {
                    max.X = nanobot.Position.X;
                }
                if (nanobot.Position.Y > max.Y)
                {
                    max.Y = nanobot.Position.Y;
                }
                if (nanobot.Position.Z > max.Z)
                {
                    max.Z = nanobot.Position.Z;
                }
            }

            return (min, max);
        }

        private int CountNanobotsWhichHavePositionInRange(Nanobot[] nanobots, Position position)
        {
            int nanobotsWhichHavePositionInRange = 0;

            foreach (Nanobot nanobot in nanobots)
            {
                int manhattanDistance = Math.Abs(position.X - nanobot.Position.X)
                    + Math.Abs(position.Y - nanobot.Position.Y)
                    + Math.Abs(position.Z - nanobot.Position.Z);

                if (nanobot.SignalRadius >= manhattanDistance)
                {
                    nanobotsWhichHavePositionInRange++;
                }
            }

            return nanobotsWhichHavePositionInRange;
        }
    }
}
