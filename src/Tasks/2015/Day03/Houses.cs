using System.Collections.Generic;

namespace App.Tasks.Year2015.Day3
{
    public class Houses
    {
        public static HashSet<(int, int)> GetVisitedHousesLocationsForMoves(string moves)
        {
            HashSet<(int, int)> houseLocations = new HashSet<(int, int)>();

            // Starting location is (0,0) we track visited houses by using Cartesian coordinate system
            int x = 0;
            int y = 0;

            houseLocations.Add((x, y));

            foreach (char move in moves)
            {
                switch (move)
                {
                    case '^': // north
                        y += 1;
                        break;
                    case 'v': // south
                        y -= 1;
                        break;
                    case '>': // east
                        x += 1;
                        break;
                    case '<': // west
                        x -= 1;
                        break;
                    default:
                        break;
                }

                houseLocations.Add((x, y));
            }

            return houseLocations;
        }
    }
}
