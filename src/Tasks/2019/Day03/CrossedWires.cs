using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day3
{
    public class CrossedWires
    {
        private const int FIRST_WIRE = 1;

        private const int SECOND_WIRE = 2;

        public int CalculateManhattanDistanceFromTheCentralPortToTheClosestIntersection(
            Dictionary<int, List<Instruction>> wiresPaths
        )
        {
            List<(int, int)> wiresIntersections = CalculateWiresIntersections(wiresPaths);
            int minManhattanDistance = CalculateMinManhattanDistance(wiresIntersections);

            return minManhattanDistance;
        }

        public int CalculateFewestCombinedStepsTheWiresMustTakeToReachAnIntersection(
            Dictionary<int, List<Instruction>> wiresPaths
        )
        {
            Dictionary<int, Dictionary<int, (int, int)>> wiresLocationsWithSteps =
                new Dictionary<int, Dictionary<int, (int, int)>>();

            foreach (KeyValuePair<int, List<Instruction>> wirePath in wiresPaths)
            {
                Dictionary<int, (int, int)> wireLocationsWithSteps = GetWireLocationsWithSteps(wirePath.Value);
                wiresLocationsWithSteps.Add(wirePath.Key, wireLocationsWithSteps);
            }

            List<(int, int)> wiresIntersections = CalculateWiresIntersections(wiresPaths);

            int fewestCombinedSteps = CalculateFewestCombinedSteps(wiresLocationsWithSteps, wiresIntersections);

            return fewestCombinedSteps;
        }

        private List<(int, int)> CalculateWiresIntersections(Dictionary<int, List<Instruction>> wiresPaths)
        {
            Dictionary<int, List<(int, int)>> wiresLocations = new Dictionary<int, List<(int, int)>>();

            foreach (KeyValuePair<int, List<Instruction>> wirePath in wiresPaths)
            {
                Dictionary<int, (int, int)> wireLocationsWithSteps = GetWireLocationsWithSteps(wirePath.Value);
                wiresLocations.Add(wirePath.Key, wireLocationsWithSteps.Values.ToList());
            }

            List<(int, int)> wiresIntersections =
                wiresLocations[FIRST_WIRE].Intersect(wiresLocations[SECOND_WIRE]).ToList();

            return wiresIntersections;
        }

        private Dictionary<int, (int, int)> GetWireLocationsWithSteps(List<Instruction> instructions)
        {
            Dictionary<int, (int, int)> wireLocations = new Dictionary<int, (int, int)>();

            int totalSteps = 0;
            int x = 0;
            int y = 0;

            foreach (Instruction instruction in instructions)
            {
                int steps = instruction.Steps;

                while (steps > 0)
                {
                    switch (instruction.TurnDirection)
                    {
                        case TurnDirection.Left:
                            x--;
                            break;
                        case TurnDirection.Right:
                            x++;
                            break;
                        case TurnDirection.Up:
                            y++;
                            break;
                        case TurnDirection.Down:
                            y--;
                            break;
                    }

                    steps--;
                    totalSteps++;

                    wireLocations.Add(totalSteps, (x, y));
                }
            }

            return wireLocations;
        }

        private int CalculateMinManhattanDistance(IEnumerable<(int, int)> wiresIntersections)
        {
            int minManhattanDistance = int.MaxValue;

            foreach ((int x, int y) in wiresIntersections)
            {
                int manhattanDistance = Math.Abs(x) + Math.Abs(y);
                minManhattanDistance = Math.Min(manhattanDistance, minManhattanDistance);
            }

            return minManhattanDistance;
        }

        private int CalculateFewestCombinedSteps(
            Dictionary<int, Dictionary<int, (int x, int y)>> wiresLocationsWithSteps,
            List<(int, int)> wiresIntersections
        )
        {
            int fewestCombinedSteps = int.MaxValue;

            foreach ((int x, int y) in wiresIntersections)
            {
                int firstWireSteps = wiresLocationsWithSteps[FIRST_WIRE]
                    .Where(wl => wl.Value.x == x && wl.Value.y == y).First().Key;
                int secondWireSteps = wiresLocationsWithSteps[SECOND_WIRE]
                    .Where(wl => wl.Value.x == x && wl.Value.y == y).First().Key;

                int steps = firstWireSteps + secondWireSteps;

                fewestCombinedSteps = Math.Min(steps, fewestCombinedSteps);
            }

            return fewestCombinedSteps;
        }
    }
}
