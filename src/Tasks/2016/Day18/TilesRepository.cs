namespace App.Tasks.Year2016.Day18
{
    public class TilesRowRepository
    {
        private const char SAFE_TILE = '.';

        public bool[] GetInitialTilesRow(string input)
        {
            bool[] row = new bool[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == SAFE_TILE)
                {
                    row[i] = true;
                }
            }

            return row;
        }
    }
}
