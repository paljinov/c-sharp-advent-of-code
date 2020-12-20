using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day20
{
    public class AssembleImage
    {
        public long CalculateCornerTilesIdsProduct(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, string[]> tilesBorders = GetTilesBorders(tiles);
            Dictionary<int, int> tilesSharedBorders = CountTilesSharedBorders(tilesBorders);

            // Corner tiles have the least shared borders
            int minSharedBorders = tilesSharedBorders.Values.Min();
            int[] cornerTilesIds = tilesSharedBorders
                .Where(t => t.Value == minSharedBorders)
                .Select(t => t.Key)
                .ToArray();

            long cornerTilesIdsProduct = 1;
            foreach (int cornerTileId in cornerTilesIds)
            {
                cornerTilesIdsProduct *= cornerTileId;
            }

            return cornerTilesIdsProduct;
        }

        private Dictionary<int, string[]> GetTilesBorders(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, string[]> tilesBorders = new Dictionary<int, string[]>();

            foreach (KeyValuePair<int, string[]> tile in tiles)
            {
                string[] tileBorders = new string[8];
                // Top border
                tileBorders[0] = tile.Value[0];
                // Bottom border
                tileBorders[1] = tile.Value[^1];

                StringBuilder left = new StringBuilder();
                StringBuilder right = new StringBuilder();
                foreach (string row in tile.Value)
                {
                    left.Append(row[0]);
                    right.Append(row[^1]);
                }
                // Left border
                tileBorders[2] = left.ToString();
                // Right Border
                tileBorders[3] = right.ToString();

                // Reverse all
                tileBorders[4] = ReverseString(tileBorders[0]);
                tileBorders[5] = ReverseString(tileBorders[1]);
                tileBorders[6] = ReverseString(tileBorders[2]);
                tileBorders[7] = ReverseString(tileBorders[3]);

                tilesBorders.Add(tile.Key, tileBorders);
            }

            return tilesBorders;
        }

        private Dictionary<int, int> CountTilesSharedBorders(Dictionary<int, string[]> tilesBorders)
        {
            Dictionary<int, int> tilesSharedBorders = new Dictionary<int, int>();

            foreach (KeyValuePair<int, string[]> iTileBorders in tilesBorders)
            {
                int tileSharedBorders = 0;
                foreach (KeyValuePair<int, string[]> jTileBorders in tilesBorders)
                {
                    // If not the same tile
                    if (iTileBorders.Key != jTileBorders.Key)
                    {
                        tileSharedBorders += iTileBorders.Value.Intersect(jTileBorders.Value).Count();
                    }
                }

                tilesSharedBorders.Add(iTileBorders.Key, tileSharedBorders);
            }

            return tilesSharedBorders;
        }

        private string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}
