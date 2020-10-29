/*
--- Part Two ---

The next year, to speed up the process, Santa creates a robot version of
himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the
same starting house), then take turns moving based on instructions from the elf,
who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

- ^v delivers presents to 3 houses, because Santa goes north, and then
  Robo-Santa goes south.
- ^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back
  where they started.
- ^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction
  and Robo-Santa going the other.
*/

using System.Text;

namespace App.Tasks.Year2015.Day3
{
    class Part2 : ITask<int>
    {
        public int Solution(string moves)
        {
            StringBuilder santaMoves = new StringBuilder();
            StringBuilder roboSantaMoves = new StringBuilder();

            for (int i = 0; i < moves.Length; i++)
            {
                char move = moves[i];

                if (i % 2 == 0)
                {
                    santaMoves.Append(move);
                }
                else
                {
                    roboSantaMoves.Append(move);
                }
            }

            var santaHouseLocations = Houses.GetVisitedHousesLocationsForMoves(santaMoves.ToString());
            var roboSantaHouseLocations = Houses.GetVisitedHousesLocationsForMoves(roboSantaMoves.ToString());

            santaHouseLocations.UnionWith(roboSantaHouseLocations);

            return santaHouseLocations.Count;
        }
    }
}
