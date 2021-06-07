using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day24
{
    public class Tiles
    {
        private const int DAYS = 100;

        public int CountTilesWithBlackSideUp(List<List<Direction>> tilesDirections)
        {
            Dictionary<(int, int), bool> tilesHexagonalGrid = GetTilesHexagonalGrid(tilesDirections);
            int tilesWithBlackSideUp = tilesHexagonalGrid.Where(t => t.Value == true).Count();

            return tilesWithBlackSideUp;
        }

        public int CountTilesWithBlackSideUpForAdjacentRules(List<List<Direction>> tilesDirections)
        {
            Dictionary<(int, int), bool> tilesHexagonalGrid = GetTilesHexagonalGrid(tilesDirections);

            for (int i = 0; i < DAYS; i++)
            {
                Dictionary<(int, int), bool> dayTilesHexagonalGrid =
                    new Dictionary<(int, int), bool>(tilesHexagonalGrid);

                // Get all adjacent tiles
                HashSet<(int, int)> ajacentTiles = new HashSet<(int, int)>();
                foreach (KeyValuePair<(int, int), bool> tile in tilesHexagonalGrid)
                {
                    // If black tile
                    if (tile.Value)
                    {
                        ajacentTiles.Add(tile.Key);
                        ajacentTiles.UnionWith(GetAdjacentTiles(tile.Key));
                    }
                }

                foreach ((int, int) tile in ajacentTiles)
                {
                    int adjacentTilesWithBlackSideUp =
                        CountAdjacentTilesWithBlackSideUp(tile, tilesHexagonalGrid);

                    // Any black tile with zero or more than 2 black tiles
                    // immediately adjacent to it is flipped to white
                    if (tilesHexagonalGrid.ContainsKey(tile) && dayTilesHexagonalGrid[tile]
                        && (adjacentTilesWithBlackSideUp == 0 || adjacentTilesWithBlackSideUp > 2))
                    {
                        dayTilesHexagonalGrid[tile] = false;
                    }
                    // Any white tile with exactly 2 black tiles immediately
                    // adjacent to it is flipped to black
                    else if ((!tilesHexagonalGrid.ContainsKey(tile) || !dayTilesHexagonalGrid[tile]) && adjacentTilesWithBlackSideUp == 2)
                    {
                        dayTilesHexagonalGrid[tile] = true;
                    }
                }

                tilesHexagonalGrid = dayTilesHexagonalGrid;

            }

            int tilesWithBlackSideUp = tilesHexagonalGrid.Where(t => t.Value == true).Count();

            return tilesWithBlackSideUp;
        }

        private Dictionary<(int, int), bool> GetTilesHexagonalGrid(List<List<Direction>> tilesDirections)
        {
            Dictionary<(int, int), bool> tilesHexagonalGrid = new Dictionary<(int, int), bool>();

            foreach (List<Direction> tileDirections in tilesDirections)
            {
                (int x, int y) = GetTileCoordinates(tileDirections);

                // All tiles start with the white side facing up
                bool black = false;
                // If hexagonal grid doesn't contain tile or tile is white
                if (!tilesHexagonalGrid.ContainsKey((x, y)) || !tilesHexagonalGrid[(x, y)])
                {
                    black = true;
                }

                tilesHexagonalGrid[(x, y)] = black;
            }

            return tilesHexagonalGrid;
        }

        private (int, int) GetTileCoordinates(List<Direction> tileDirections)
        {
            // https://www.redblobgames.com/grids/hexagons/#coordinates
            int x = 0;
            int y = 0;

            foreach (Direction direction in tileDirections)
            {
                switch (direction)
                {
                    case Direction.East:
                        x++;
                        break;
                    case Direction.Southeast:
                        y++;
                        break;
                    case Direction.Northeast:
                        x++;
                        y--;
                        break;
                    case Direction.West:
                        x--;
                        break;
                    case Direction.Southwest:
                        x--;
                        y++;
                        break;
                    case Direction.Northwest:
                        y--;
                        break;
                }
            }

            return (x, y);
        }

        private List<(int, int)> GetAdjacentTiles((int, int) tile)
        {
            (int x, int y) = tile;

            List<(int, int)> adjacentTiles = new List<(int, int)>
            {
                (x + 1, y),
                (x, y + 1),
                (x + 1, y - 1),
                (x - 1, y),
                (x - 1, y + 1),
                (x, y - 1)
            };

            return adjacentTiles;
        }

        private int CountAdjacentTilesWithBlackSideUp((int, int) tile, Dictionary<(int, int), bool> tilesHexagonalGrid)
        {
            int tilesWithBlackSideUp = 0;

            (int x, int y) = tile;

            // East
            if (tilesHexagonalGrid.ContainsKey((x + 1, y)) && tilesHexagonalGrid[(x + 1, y)])
            {
                tilesWithBlackSideUp++;
            }
            // Southeast
            if (tilesHexagonalGrid.ContainsKey((x, y + 1)) && tilesHexagonalGrid[(x, y + 1)])
            {
                tilesWithBlackSideUp++;
            }
            // Northeast
            if (tilesHexagonalGrid.ContainsKey((x + 1, y - 1)) && tilesHexagonalGrid[(x + 1, y - 1)])
            {
                tilesWithBlackSideUp++;
            }
            // West
            if (tilesHexagonalGrid.ContainsKey((x - 1, y)) && tilesHexagonalGrid[(x - 1, y)])
            {
                tilesWithBlackSideUp++;
            }
            // Southwest
            if (tilesHexagonalGrid.ContainsKey((x - 1, y + 1)) && tilesHexagonalGrid[(x - 1, y + 1)])
            {
                tilesWithBlackSideUp++;
            }
            // Northwest
            if (tilesHexagonalGrid.ContainsKey((x, y - 1)) && tilesHexagonalGrid[(x, y - 1)])
            {
                tilesWithBlackSideUp++;
            }

            return tilesWithBlackSideUp;
        }
    }
}
