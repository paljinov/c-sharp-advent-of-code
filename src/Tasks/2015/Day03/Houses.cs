using System.Collections.Generic;

namespace App.Tasks.Year2015.Day3
{
    public class Houses
    {
        public int CountHousesThatReceiveAtLeastOnePresent(CardinalDirection[] instructions)
        {
            HashSet<(int, int)> visitedHouses = GetVisitedHouses(instructions);

            return visitedHouses.Count;
        }

        public int CountHousesThatReceiveAtLeastOnePresentWithRoboSanta(CardinalDirection[] instructions)
        {
            CardinalDirection[] santaMoves = new CardinalDirection[instructions.Length / 2];
            CardinalDirection[] roboSantaMoves = new CardinalDirection[instructions.Length / 2];

            for (int i = 0; i < instructions.Length; i++)
            {
                CardinalDirection instruction = instructions[i];

                if (i % 2 == 0)
                {
                    santaMoves[i / 2] = instruction;
                }
                else
                {
                    roboSantaMoves[i / 2] = instruction;
                }
            }

            HashSet<(int, int)> santaHouseLocations = GetVisitedHouses(santaMoves);
            HashSet<(int, int)> roboSantaHouseLocations = GetVisitedHouses(roboSantaMoves);

            santaHouseLocations.UnionWith(roboSantaHouseLocations);

            return santaHouseLocations.Count;
        }

        private HashSet<(int, int)> GetVisitedHouses(CardinalDirection[] instructions)
        {
            HashSet<(int, int)> visitedHouses = new HashSet<(int, int)>();

            // Starting location is (0,0)
            int x = 0;
            int y = 0;

            visitedHouses.Add((x, y));

            foreach (CardinalDirection instruction in instructions)
            {
                switch (instruction)
                {
                    case CardinalDirection.North:
                        y += 1;
                        break;
                    case CardinalDirection.South:
                        y -= 1;
                        break;
                    case CardinalDirection.East:
                        x += 1;
                        break;
                    default:
                        x -= 1;
                        break;
                }

                visitedHouses.Add((x, y));
            }

            return visitedHouses;
        }
    }
}
