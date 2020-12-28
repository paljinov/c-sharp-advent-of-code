using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day20
{
    public class AssembleImage
    {
        private const int ROTATIONS = 4;

        private const int SEA_MONSTER_HASHES = 15;

        public long CalculateCornerTilesIdsProduct(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, Dictionary<Border, string>> tilesBorders = GetTilesBorders(tiles);
            Dictionary<int, List<(int, string)>> tilesSharedBorders = GetTilesSharedBorders(tilesBorders);
            int[] corners = GetCorners(tilesSharedBorders);

            long cornerTilesIdsProduct = 1;
            foreach (int cornerTile in corners)
            {
                cornerTilesIdsProduct *= cornerTile;
            }

            return cornerTilesIdsProduct;
        }

        public int CountHashSignsWhichAreNotPartOfSeaMonster(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, Dictionary<Border, string>> tilesBorders = GetTilesBorders(tiles);
            Dictionary<int, List<(int, string)>> tilesSharedBorders = GetTilesSharedBorders(tilesBorders);

            // Get all possible tiles position and orientation permutations
            List<Dictionary<(int, int), AlignedTile>> tilesAlignmentPermutations =
                GetTilesAlignmentPermutations(tilesSharedBorders, tiles);

            int hashSignsWhichAreNotPartOfSeaMonster = 0;
            foreach (Dictionary<(int, int), AlignedTile> alignedTiles in tilesAlignmentPermutations)
            {
                List<string[]> images = GetImagePermutationsForAlignedTiles(alignedTiles);
                foreach (string[] image in images)
                {
                    int seaMonsters = SearchSeaMonsters(image);
                    if (seaMonsters > 0)
                    {
                        int totalHashes = CountTotalHashes(image);
                        hashSignsWhichAreNotPartOfSeaMonster = totalHashes - seaMonsters * SEA_MONSTER_HASHES;
                        break;
                    }
                }

                if (hashSignsWhichAreNotPartOfSeaMonster > 0)
                {
                    break;
                }
            }

            return hashSignsWhichAreNotPartOfSeaMonster;
        }

        private Dictionary<int, Dictionary<Border, string>> GetTilesBorders(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, Dictionary<Border, string>> tilesBorders =
                new Dictionary<int, Dictionary<Border, string>>();

            foreach (KeyValuePair<int, string[]> tile in tiles)
            {
                tilesBorders.Add(tile.Key, new Dictionary<Border, string>
                {
                    [Border.Top] = GetTileBorder(tile.Value, Border.Top),
                    [Border.Bottom] = GetTileBorder(tile.Value, Border.Bottom),
                    [Border.Left] = GetTileBorder(tile.Value, Border.Left),
                    [Border.Right] = GetTileBorder(tile.Value, Border.Right)
                });
            }

            return tilesBorders;
        }

        private string GetTileBorder(string[] tile, Border borderType)
        {
            string border = null;
            StringBuilder sb = new StringBuilder();

            switch (borderType)
            {
                case Border.Top:
                    border = tile[0];
                    break;
                case Border.Right:
                    sb = new StringBuilder();
                    foreach (string row in tile)
                    {
                        sb.Append(row[^1]);
                    }
                    border = sb.ToString();
                    break;
                case Border.Bottom:
                    border = tile[^1];
                    break;
                case Border.Left:
                    sb = new StringBuilder();
                    foreach (string row in tile)
                    {
                        sb.Append(row[0]);
                    }
                    border = sb.ToString();
                    break;
            }

            return border;
        }

        private Dictionary<int, List<(int, string)>> GetTilesSharedBorders(
            Dictionary<int, Dictionary<Border, string>> tilesBorders
        )
        {
            Dictionary<int, List<(int, string)>> tilesSharedBorders = new Dictionary<int, List<(int, string)>>();

            foreach (KeyValuePair<int, Dictionary<Border, string>> firstTileBorders in tilesBorders)
            {
                List<(int, string)> firstTileSharedBorders = new List<(int, string)>();

                foreach (KeyValuePair<Border, string> firstTileBorder in firstTileBorders.Value)
                {
                    foreach (KeyValuePair<int, Dictionary<Border, string>> secondTileBorders in tilesBorders)
                    {
                        // If it is not the same tile
                        if (firstTileBorders.Key != secondTileBorders.Key)
                        {
                            foreach (KeyValuePair<Border, string> secondTileBorder in secondTileBorders.Value)
                            {
                                if (firstTileBorder.Value.Equals(secondTileBorder.Value)
                                    || firstTileBorder.Value.Equals(ReverseString(secondTileBorder.Value)))
                                {
                                    firstTileSharedBorders.Add((secondTileBorders.Key, secondTileBorder.Value));
                                }
                            }
                        }
                    }

                }

                tilesSharedBorders.Add(firstTileBorders.Key, firstTileSharedBorders);
            }

            return tilesSharedBorders;
        }

        private int[] GetCorners(Dictionary<int, List<(int, string)>> tilesSharedBorders)
        {
            int[] corners = new int[4];

            int i = 0;
            foreach (KeyValuePair<int, List<(int, string)>> tileSharedBorders in tilesSharedBorders)
            {
                // Corner tiles have only 2 shared borders
                if (tileSharedBorders.Value.Count == 2)
                {
                    corners[i] = tileSharedBorders.Key;
                    i++;
                }
            }

            return corners;
        }

        private List<Dictionary<(int, int), AlignedTile>> GetTilesAlignmentPermutations(
            Dictionary<int, List<(int, string)>> tilesSharedBorders,
            Dictionary<int, string[]> tiles)
        {
            List<Dictionary<(int, int), AlignedTile>> tilesAlignmentPermutations =
                new List<Dictionary<(int, int), AlignedTile>>();

            // Each of these corners can be top left tile
            int[] corners = GetCorners(tilesSharedBorders);

            // Iterating possible corners
            foreach (int cornerTileId in corners)
            {
                string[] tile = tiles[cornerTileId];
                List<string[]> cornerTilePermutations = new List<string[]>();

                for (int i = 0; i < ROTATIONS; i++)
                {
                    cornerTilePermutations.Add(tile);
                    cornerTilePermutations.Add(FlipHorizontally(tile));
                    cornerTilePermutations.Add(FlipVertically(tile));

                    tile = RotateTile(tile);
                }

                foreach (string[] cornerTile in cornerTilePermutations)
                {
                    Dictionary<(int, int), AlignedTile> alignedTiles = GetTilesAlignmentForTopLeftTileOrientation(
                        tilesSharedBorders,
                        tiles,
                        cornerTileId,
                        cornerTile
                    );

                    // If image assemblement for this permutation is possible
                    if (alignedTiles != null)
                    {
                        tilesAlignmentPermutations.Add(alignedTiles);
                    }
                }
            }

            return tilesAlignmentPermutations;
        }

        private Dictionary<(int, int), AlignedTile> GetTilesAlignmentForTopLeftTileOrientation(
            Dictionary<int, List<(int, string)>> tilesSharedBorders,
            Dictionary<int, string[]> tiles,
            int topLeftTileId,
            string[] topLeftTileOrientation
        )
        {
            int tilesPerSquareSide = (int)Math.Sqrt(tilesSharedBorders.Count);

            Dictionary<(int, int), AlignedTile> alignedTiles = new Dictionary<(int, int), AlignedTile>
            {
                {
                    (0, 0),
                    new AlignedTile
                    {
                        Id = topLeftTileId,
                        Position = (0, 0),
                        Orientation = topLeftTileOrientation
                    }
                }
            };

            // Determine position of other tiles starting from top left
            for (int i = 0; i < tilesPerSquareSide; i++)
            {
                for (int j = 0; j < tilesPerSquareSide; j++)
                {
                    // Top left is already determined
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    int nextTileId = 0;
                    string[] nextTile = null;

                    // If left tile is already determined
                    if (j > 0)
                    {
                        (nextTileId, nextTile) = GetNextTileAlignedByBorder(
                            alignedTiles[(i, j - 1)].Id,
                            GetTileBorder(alignedTiles[(i, j - 1)].Orientation, Border.Right),
                            Border.Left,
                            tilesSharedBorders,
                            tiles
                        );
                    }
                    // If top bar is already defined
                    else if (i > 0)
                    {
                        (nextTileId, nextTile) = GetNextTileAlignedByBorder(
                            alignedTiles[(i - 1, j)].Id,
                            GetTileBorder(alignedTiles[(i - 1, j)].Orientation, Border.Bottom),
                            Border.Top,
                            tilesSharedBorders,
                            tiles
                         );
                    }

                    if (nextTile == null)
                    {
                        return null;
                    }

                    alignedTiles.Add((i, j), new AlignedTile
                    {
                        Id = nextTileId,
                        Position = (i, j),
                        Orientation = nextTile
                    });
                }
            }

            return alignedTiles;
        }

        private (int nextTileId, string[] orientedNextTile) GetNextTileAlignedByBorder(
            int alignByTileId,
            string alignByBorder,
            Border checkBorder,
            Dictionary<int, List<(int, string)>> tilesSharedBorders,
            Dictionary<int, string[]> tiles
        )
        {
            List<(int, string)> tileSharedBorders = tilesSharedBorders[alignByTileId];
            foreach ((int tileId, string tileSharedBorder) in tileSharedBorders)
            {
                // This tile must be top oriented
                if (tileSharedBorder == alignByBorder || ReverseString(tileSharedBorder) == alignByBorder)
                {
                    string[] nextTile = tiles[tileId];
                    string[] nextTileFlipped;

                    while (GetTileBorder(nextTile, checkBorder) != alignByBorder)
                    {
                        nextTileFlipped = FlipVertically(nextTile);
                        if (GetTileBorder(nextTileFlipped, checkBorder) == alignByBorder)
                        {
                            nextTile = nextTileFlipped;
                            break;
                        }

                        nextTileFlipped = FlipHorizontally(nextTile);
                        if (GetTileBorder(nextTileFlipped, checkBorder) == alignByBorder)
                        {
                            nextTile = nextTileFlipped;
                            break;
                        }

                        nextTile = RotateTile(nextTile);
                    }

                    return (tileId, nextTile);
                }
            }

            return (0, null);
        }

        private List<string[]> GetImagePermutationsForAlignedTiles(Dictionary<(int, int), AlignedTile> alignedTiles)
        {
            List<string[]> images = new List<string[]>();

            string[,][] tilesWithRemovedBorders = GetTilesWithRemovedBorders(alignedTiles);
            string[] image = AssembleActualImage(tilesWithRemovedBorders);

            for (int i = 0; i < ROTATIONS; i++)
            {
                images.Add(image);
                images.Add(FlipHorizontally(image));
                images.Add(FlipVertically(image));

                image = RotateTile(image);
            }

            return images;
        }

        private string[,][] GetTilesWithRemovedBorders(Dictionary<(int, int), AlignedTile> alignedTiles)
        {
            int tilesPerSquareSide = (int)Math.Sqrt(alignedTiles.Count);
            string[,][] tilesWithRemovedBorders = new string[tilesPerSquareSide, tilesPerSquareSide][];

            foreach (KeyValuePair<(int, int), AlignedTile> alignedTile in alignedTiles)
            {
                (int i, int j) = alignedTile.Key;
                string[] tile = alignedTile.Value.Orientation;

                string[] tileWithRemovedBorders = new string[tile.Length - 2];

                // Top and bottom border is removed
                for (int k = 1; k < tile.Length - 1; k++)
                {
                    // Left and right border is removed
                    tileWithRemovedBorders[k - 1] = tile[k][1..^1];
                }

                tilesWithRemovedBorders[i, j] = tileWithRemovedBorders;
            }

            return tilesWithRemovedBorders;
        }

        private string[] AssembleActualImage(string[,][] tilesWithRemovedBorders)
        {
            string[] image = new string[tilesWithRemovedBorders.GetLength(0) * tilesWithRemovedBorders[0, 0].Length];

            int k = 0;
            for (int i = 0; i < tilesWithRemovedBorders.GetLength(0); i++)
            {
                StringBuilder[] sbRows = new StringBuilder[tilesWithRemovedBorders[0, 0].Length];

                for (int j = 0; j < tilesWithRemovedBorders.GetLength(1); j++)
                {
                    for (int h = 0; h < tilesWithRemovedBorders[i, j].Length; h++)
                    {
                        string row = tilesWithRemovedBorders[i, j][h];
                        if (sbRows[h] == null)
                        {
                            sbRows[h] = new StringBuilder();
                        }
                        sbRows[h] = sbRows[h].Append(row);
                    }
                }

                foreach (StringBuilder sb in sbRows)
                {
                    image[k] = sb.ToString();
                    k++;
                }
            }

            return image;
        }

        private int SearchSeaMonsters(string[] image)
        {
            /*
            Sea monster pattern:
                              #
            #    ##    ##    ###
             #  #  #  #  #  #
            */
            int seaMonsters = 0;

            for (int i = 0; i < image.Length; i++)
            {
                for (int j = 0; j < image[i].Length; j++)
                {
                    // Last sea monster pattern row
                    string row = image[i];
                    if (j + 15 < row.Length && row[j] == '#' && row[j + 3] == '#' && row[j + 6] == '#'
                        && row[j + 9] == '#' && row[j + 12] == '#' && row[j + 15] == '#')
                    {
                        // Middle sea monster pattern row
                        if (i - 1 >= 0)
                        {
                            row = image[i - 1];
                            if (j > 0 && j + 18 < row.Length && row[j - 1] == '#' && row[j + 4] == '#'
                                && row[j + 5] == '#' && row[j + 10] == '#' && row[j + 11] == '#'
                                && row[j + 16] == '#' && row[j + 17] == '#' && row[j + 18] == '#')
                            {
                                // First sea monster pattern row
                                if (i - 2 >= 0)
                                {
                                    row = image[i - 2];
                                    if (j + 17 < row.Length && row[j + 17] == '#')
                                    {
                                        seaMonsters++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return seaMonsters;
        }

        private string[] RotateTile(string[] tile)
        {
            int n = tile.Length;

            char[,] tileCharArray = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tileCharArray[i, j] = tile[i][j];
                }
            }

            string[] rotatedTile = new string[n];
            for (int i = 0; i < n; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < n; j++)
                {
                    sb.Append(tileCharArray[n - j - 1, i]);
                }
                rotatedTile[i] = sb.ToString();
            }

            return rotatedTile;
        }

        private string[] FlipHorizontally(string[] tile)
        {
            string[] flippedTile = new string[tile.Length];
            for (int i = 0; i < tile.Length; i++)
            {
                flippedTile[i] = ReverseString(tile[i]);
            }

            return flippedTile;
        }

        private string[] FlipVertically(string[] tile)
        {
            char[,] flippedTileCharArray = new char[tile.Length, tile.Length];
            int n = tile.Length;

            for (int i = 0; i < n / 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    flippedTileCharArray[i, j] = tile[n - 1 - i][j];
                    flippedTileCharArray[n - 1 - i, j] = tile[i][j];
                }
            }

            string[] flippedTile = new string[tile.Length];
            for (int i = 0; i < n; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < n; j++)
                {
                    sb.Append(flippedTileCharArray[i, j]);
                }

                flippedTile[i] = sb.ToString();
            }

            return flippedTile;
        }

        private string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        private int CountTotalHashes(string[] image)
        {
            int totalHashes = 0;
            foreach (string row in image)
            {
                totalHashes += row.Count(c => c == '#');
            }

            return totalHashes;
        }
    }
}
