/*
--- Part Two ---

How many safe tiles are there in a total of 400000 rows?
*/

namespace App.Tasks.Year2016.Day18
{
    public class Part2 : ITask<int>
    {
        private readonly int totalRows = 400000;

        private readonly TilesRowRepository tilesRowRepository;

        private readonly Tiles tiles;

        public Part2()
        {
            tilesRowRepository = new TilesRowRepository();
            tiles = new Tiles();
        }

        public int Solution(string input)
        {
            bool[] initialTilesRow = tilesRowRepository.GetInitialTilesRow(input);
            int safeTiles = tiles.CountSafeTiles(initialTilesRow, totalRows);

            return safeTiles;
        }
    }
}
