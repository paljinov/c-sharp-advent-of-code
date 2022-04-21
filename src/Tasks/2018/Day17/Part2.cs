/*
--- Part Two ---

After a very long time, the water spring will run dry. How much water will be
retained?

In the example above, water that won't eventually drain out is shown as ~, a
total of 29 tiles.

How many water tiles are left after the water spring stops producing water and
all remaining water not at rest has drained?
*/

namespace App.Tasks.Year2018.Day17
{
    public class Part2 : ITask<int>
    {
        private readonly ClayVeinsRepository clayVeinsRepository;

        private readonly Tiles tiles;

        public Part2()
        {
            clayVeinsRepository = new ClayVeinsRepository();
            tiles = new Tiles();
        }
        public int Solution(string input)
        {
            ClayVein[] clayVeins = clayVeinsRepository.GetClayVeins(input);
            int leftWaterTiles = tiles.CountLeftWaterTiles(clayVeins);

            return leftWaterTiles;
        }
    }
}
