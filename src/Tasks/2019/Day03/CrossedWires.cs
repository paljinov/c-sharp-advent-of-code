using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day3
{
    public class CrossedWires
    {
        public int CalculateManhattanDistanceFromTheCentralPortToTheClosestIntersection(
            Dictionary<int, List<Instruction>> wiresPaths
        )
        {
            Dictionary<int, List<(int, int)>> wiresLocations = new Dictionary<int, List<(int, int)>>();

            foreach (KeyValuePair<int, List<Instruction>> wirePath in wiresPaths)
            {
                List<(int, int)> wireLocations = GetWireLocations(wirePath.Value);
                wiresLocations.Add(wirePath.Key, wireLocations);
            }

            IEnumerable<(int, int)> intersections = wiresLocations[1].Intersect(wiresLocations[2]);
            int minManhattanDistance = int.MaxValue;

            foreach ((int x, int y) in intersections)
            {
                int manhattanDistance = Math.Abs(x) + Math.Abs(y);
                minManhattanDistance = Math.Min(manhattanDistance, minManhattanDistance);
            }

            return minManhattanDistance;
        }

        private List<(int, int)> GetWireLocations(List<Instruction> instructions)
        {
            List<(int, int)> wireLocations = new List<(int, int)>();

            int x = 0;
            int y = 0;

            foreach (Instruction instruction in instructions)
            {
                int steps = instruction.Steps;

                switch (instruction.TurnDirection)
                {
                    case TurnDirection.Left:
                        while (steps > 0)
                        {
                            x--;
                            wireLocations.Add((x, y));
                            steps--;
                        }
                        break;
                    case TurnDirection.Right:
                        while (steps > 0)
                        {
                            x++;
                            wireLocations.Add((x, y));
                            steps--;
                        }
                        break;
                    case TurnDirection.Up:
                        while (steps > 0)
                        {
                            y++;
                            wireLocations.Add((x, y));
                            steps--;
                        }
                        break;
                    case TurnDirection.Down:
                        while (steps > 0)
                        {
                            y--;
                            wireLocations.Add((x, y));
                            steps--;
                        }
                        break;
                }
            }

            return wireLocations;
        }
    }
}
