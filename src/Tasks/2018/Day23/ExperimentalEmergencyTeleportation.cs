using System;
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
    }
}
