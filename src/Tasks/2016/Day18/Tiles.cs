namespace App.Tasks.Year2016.Day18
{
    public class Tiles
    {
        public int CountSafeTiles(bool[] initialTilesRow, int totalRows)
        {
            int safeTiles = 0;

            bool[,] tiles = new bool[totalRows, initialTilesRow.Length];
            for (int i = 0; i < initialTilesRow.Length; i++)
            {
                if (initialTilesRow[i])
                {
                    safeTiles++;
                }

                tiles[0, i] = initialTilesRow[i];
            }

            int rows = tiles.GetLength(0);
            int columns = tiles.GetLength(1);

            for (int i = 1; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Its left and center tiles are traps, but its right tile is not
                    if (j > 0 && !tiles[i - 1, j - 1] && !tiles[i - 1, j] && (j == columns - 1 || tiles[i - 1, j + 1]))
                    {
                        continue;
                    }

                    // Its center and right tiles are traps, but its left tile is not
                    if ((j == 0 || tiles[i - 1, j - 1]) && !tiles[i - 1, j] && j < columns - 1 && !tiles[i - 1, j + 1])
                    {
                        continue;
                    }

                    // Only its left tile is a trap
                    if (j > 0 && !tiles[i - 1, j - 1] && tiles[i - 1, j] && (j == columns - 1 || tiles[i - 1, j + 1]))
                    {
                        continue;
                    }

                    // Only its right tile is a trap
                    if ((j == 0 || tiles[i - 1, j - 1]) && tiles[i - 1, j] && j < columns - 1 && !tiles[i - 1, j + 1])
                    {
                        continue;
                    }

                    tiles[i, j] = true;
                    safeTiles++;
                }
            }

            return safeTiles;
        }
    }
}
