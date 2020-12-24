/*
--- Part Two ---

The tile floor in the lobby is meant to be a living art exhibit. Every day, the
tiles are all flipped according to the following rules:

- Any black tile with zero or more than 2 black tiles immediately adjacent to it
  is flipped to white.
- Any white tile with exactly 2 black tiles immediately adjacent to it is
  flipped to black.

Here, tiles immediately adjacent means the six tiles directly touching the tile
in question.

The rules are applied simultaneously to every tile; put another way, it is first
determined which tiles need to be flipped, then they are all flipped at the same
time.

In the above example, the number of black tiles that are facing up after the
given number of days has passed is as follows:

Day 1: 15
Day 2: 12
Day 3: 25
Day 4: 14
Day 5: 23
Day 6: 28
Day 7: 41
Day 8: 37
Day 9: 49
Day 10: 37

Day 20: 132
Day 30: 259
Day 40: 406
Day 50: 566
Day 60: 788
Day 70: 1106
Day 80: 1373
Day 90: 1844
Day 100: 2208

After executing this process a total of 100 times, there would be 2208 black
tiles facing up.

How many tiles will be black after 100 days?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2020.Day24
{
    public class Part2 : ITask<int>
    {
        private readonly TilesRepository tilesRepository;

        private readonly Tiles tiles;

        public Part2()
        {
            tilesRepository = new TilesRepository();
            tiles = new Tiles();
        }

        public int Solution(string input)
        {
            List<List<Direction>> tilesDirections = tilesRepository.GetTilesDirections(input);
            int tilesWithBlackSideUp = tiles.CountTilesWithBlackSideUpForAdjacentRules(tilesDirections);

            return tilesWithBlackSideUp;
        }
    }
}
