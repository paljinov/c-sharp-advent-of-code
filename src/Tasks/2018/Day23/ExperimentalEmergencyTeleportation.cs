using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day23
{
    public class ExperimentalEmergencyTeleportation
    {
        public int CountNanobotsWhichAreInRangeOfTheLargestSignalRadius(Nanobot[] nanobots)
        {
            Nanobot nanobotWithLargestSignalRadius = GetNanobotWithLargestSignalRadius(nanobots);

            int nanobotsWhichAreInRangeOfTheLargestSignalRadius =
                CountNanobotsWhichAreInRangeOfSignalRadius(nanobots, nanobotWithLargestSignalRadius);

            return nanobotsWhichAreInRangeOfTheLargestSignalRadius;
        }

        public int CalculateShortestManhattanDistanceForPositionInRangeOfLargestNumberOfNanobots(
            Nanobot[] nanobots,
            Position myPosition
        )
        {
            int shortestManhattanDistance = int.MaxValue;
            int maxNanobotsWhichHavePositionInRange = 0;

            (Position min, Position max) = GetPositionsRange(nanobots);

            for (int x = min.X; x <= max.X; x++)
            {
                for (int y = min.Y; y <= max.Y; y++)
                {
                    for (int z = min.Z; z <= max.Z; z++)
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
                            shortestManhattanDistance = Math.Abs(x - myPosition.X)
                                + Math.Abs(y - myPosition.Y) + Math.Abs(z - myPosition.Z);
                            maxNanobotsWhichHavePositionInRange = nanobotsWhichHavePositionInRange;
                        }
                    }
                }
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
