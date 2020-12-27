using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2020.Day20
{
    public class AssembleImage
    {
        private const int TOP_BORDER = 0;

        private const int RIGHT_BORDER = 90;

        private const int BOTTOM_BORDER = 180;

        private const int LEFT_BORDER = 270;

        private const int SEA_MONSTER_HASHES = 15;

        public long CalculateCornerTilesIdsProduct(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, Dictionary<int, string>> tilesBorders = GetTilesBorders(tiles);
            Dictionary<int, List<SharedBorder>> tilesSharedBorders = GetTilesSharedBorders(tilesBorders);
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
            Dictionary<int, Dictionary<int, string>> tilesBorders = GetTilesBorders(tiles);
            Dictionary<int, List<SharedBorder>> tilesSharedBorders = GetTilesSharedBorders(tilesBorders);

            int hashSignsWhichAreNotPartOfSeaMonster = 0;

            // Get all possible tiles positions and orientations
            List<string[,][]> possibleTilesPositionsAndOrientations =
                GetPossibleTilesPositionsAndOrientations(tilesSharedBorders, tiles);

            // Iterating possible corners
            foreach (string[,][] positionedTiles in possibleTilesPositionsAndOrientations)
            {
                string[,][] tilesWithRemovedBorders = GetTilesWithRemovedBorders(positionedTiles);
                string[] image = AssembleActualImage(tilesWithRemovedBorders);

                for (int i = 0; i < 4; i++)
                {
                    List<string[]> images = new List<string[]>();

                    images.Add(image);
                    images.Add(FlipHorizontally(image));
                    images.Add(FlipVertically(image));

                    image = RotateTile(image);

                    foreach (string[] img in images)
                    {
                        int seaMonsters = SearchSeaMonsters(img);
                        if (seaMonsters > 0)
                        {
                            int totalHashes = CountTotalHashes(img);
                            hashSignsWhichAreNotPartOfSeaMonster = totalHashes - seaMonsters * SEA_MONSTER_HASHES;
                            break;
                        }
                    }
                }
            }

            return hashSignsWhichAreNotPartOfSeaMonster;
        }

        private Dictionary<int, Dictionary<int, string>> GetTilesBorders(Dictionary<int, string[]> tiles)
        {
            Dictionary<int, Dictionary<int, string>> tilesBorders = new Dictionary<int, Dictionary<int, string>>();

            foreach (KeyValuePair<int, string[]> tile in tiles)
            {
                Dictionary<int, string> tileBorders = new Dictionary<int, string>();

                tileBorders[TOP_BORDER] = tile.Value[0];
                tileBorders[BOTTOM_BORDER] = tile.Value[^1];

                StringBuilder left = new StringBuilder();
                StringBuilder right = new StringBuilder();
                foreach (string row in tile.Value)
                {
                    left.Append(row[0]);
                    right.Append(row[^1]);
                }

                tileBorders[LEFT_BORDER] = left.ToString();
                tileBorders[RIGHT_BORDER] = right.ToString();

                tilesBorders.Add(tile.Key, tileBorders);
            }

            return tilesBorders;
        }

        private Dictionary<int, List<SharedBorder>> GetTilesSharedBorders(
            Dictionary<int, Dictionary<int, string>> tilesBorders
        )
        {
            Dictionary<int, List<SharedBorder>> tilesSharedBorders =
                new Dictionary<int, List<SharedBorder>>();

            foreach (KeyValuePair<int, Dictionary<int, string>> tileBorders in tilesBorders)
            {
                List<SharedBorder> sharedBorders = new List<SharedBorder>();

                foreach (KeyValuePair<int, string> tileBorder in tileBorders.Value)
                {
                    SharedBorder sharedBorder = GetSharedBorder(tileBorders.Key, tileBorder.Key, tileBorder.Value, tilesBorders);
                    if (sharedBorder != null)
                    {
                        sharedBorders.Add(sharedBorder);
                    }
                }

                tilesSharedBorders.Add(tileBorders.Key, sharedBorders);
            }

            return tilesSharedBorders;
        }

        private SharedBorder GetSharedBorder(
            int tileId,
            int tileSide,
            string tileBorderString,
            Dictionary<int, Dictionary<int, string>> tilesBorders
        )
        {
            SharedBorder sharedBorder = new SharedBorder
            {
                FirstTileId = tileId,
                FirstBorderSide = tileSide
            };

            string tileBorderStringReverse = ReverseString(tileBorderString);

            foreach (KeyValuePair<int, Dictionary<int, string>> tileBorders in tilesBorders)
            {
                if (tileId != tileBorders.Key)
                {
                    foreach (KeyValuePair<int, string> tileBorder in tileBorders.Value)
                    {
                        sharedBorder.SecondTileId = tileBorders.Key;
                        sharedBorder.SecondBorderSide = tileBorder.Key;

                        if (tileBorderString.Equals(tileBorder.Value))
                        {
                            sharedBorder.FirstBorder = tileBorderString;
                            sharedBorder.FirstIsReverse = false;
                            sharedBorder.SecondBorder = tileBorder.Value;
                            sharedBorder.SecondIsReverse = false;
                            return sharedBorder;
                        }
                        else if (tileBorderString.Equals(ReverseString(tileBorder.Value)))
                        {
                            sharedBorder.FirstBorder = tileBorderString;
                            sharedBorder.FirstIsReverse = false;
                            sharedBorder.SecondBorder = ReverseString(tileBorder.Value);
                            sharedBorder.SecondIsReverse = true;
                            return sharedBorder;
                        }
                        else if (tileBorderStringReverse.Equals(tileBorder.Value))
                        {
                            sharedBorder.FirstBorder = tileBorderStringReverse;
                            sharedBorder.FirstIsReverse = true;
                            sharedBorder.SecondBorder = tileBorder.Value;
                            sharedBorder.SecondIsReverse = false;
                            return sharedBorder;
                        }
                        else if (tileBorderStringReverse.Equals(ReverseString(tileBorder.Value)))
                        {
                            sharedBorder.FirstBorder = tileBorderStringReverse;
                            sharedBorder.FirstIsReverse = true;
                            sharedBorder.SecondBorder = ReverseString(tileBorder.Value);
                            sharedBorder.SecondIsReverse = true;
                            return sharedBorder;
                        }
                    }
                }
            }

            return null;
        }

        private int[] GetCorners(Dictionary<int, List<SharedBorder>> tilesSharedBorders)
        {
            int[] corners = new int[4];

            int i = 0;
            foreach (KeyValuePair<int, List<SharedBorder>> tileSharedBorders in tilesSharedBorders)
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

        private List<string[,][]> GetPossibleTilesPositionsAndOrientations(
            Dictionary<int, List<SharedBorder>> tilesSharedBorders,
            Dictionary<int, string[]> tiles)
        {
            List<string[,][]> possibleTilesPositionsAndOrientations = new List<string[,][]>();

            // Each of these corners can be top left tile
            int[] corners = GetCorners(tilesSharedBorders);

            // Iterating possible corners
            foreach (int cornerTileId in corners)
            {
                // Each corner can be oriented in 2 possible ways
                foreach (SharedBorder sharedBorder in tilesSharedBorders[cornerTileId])
                {
                    List<string[,][]> positionedAndOrientedTilesForTopLeftTile =
                        GetPositionedAndOrientedTilesForTopLeftTile(
                            tilesSharedBorders,
                            tiles,
                            sharedBorder.FirstTileId
                        );

                    possibleTilesPositionsAndOrientations =
                        possibleTilesPositionsAndOrientations.Concat(positionedAndOrientedTilesForTopLeftTile).ToList();
                }
            }

            return possibleTilesPositionsAndOrientations;
        }

        private List<string[,][]> GetPositionedAndOrientedTilesForTopLeftTile(
            Dictionary<int, List<SharedBorder>> tilesSharedBorders,
            Dictionary<int, string[]> tiles,
            int cornerTileId
        )
        {
            List<string[,][]> possibleTilesPositionsAndOrientations = new List<string[,][]>();


            string[] topLeftTile = tiles[cornerTileId];

            List<string[]> possibleTopLeftOrientations = new List<string[]>();
            for (int t = 0; t < 4; t++)
            {
                possibleTopLeftOrientations.Add(topLeftTile);
                possibleTopLeftOrientations.Add(FlipHorizontally(topLeftTile));
                possibleTopLeftOrientations.Add(FlipVertically(topLeftTile));

                topLeftTile = RotateTile(topLeftTile);
            }

            foreach (string[] positionedTopLeftTile in possibleTopLeftOrientations)
            {
                string[,][] positionedTiles = GetPositionedTilesForTopLeftOrientation(
                    tilesSharedBorders,
                    tiles,
                    cornerTileId,
                    positionedTopLeftTile
                );

                bool allValid = true;
                for (int i = 0; i < positionedTiles.GetLength(0); i++)
                {
                    for (int j = 0; j < positionedTiles.GetLength(1); j++)
                    {
                        if (positionedTiles[i, j] == null)
                        {
                            allValid = false;
                        }
                    }
                }

                if (allValid)
                {
                    possibleTilesPositionsAndOrientations.Add(positionedTiles);
                }
            }

            return possibleTilesPositionsAndOrientations;
        }

        private string[,][] GetPositionedTilesForTopLeftOrientation(
            Dictionary<int, List<SharedBorder>> tilesSharedBorders,
            Dictionary<int, string[]> tiles,
            int topLeftTileId,
            string[] positionedTopLeftTile
        )
        {
            int tilesPerSquareSide = (int)Math.Sqrt(tilesSharedBorders.Count);

            int[,] tilesPositions = new int[tilesPerSquareSide, tilesPerSquareSide];
            tilesPositions[0, 0] = topLeftTileId;

            string[] positionedTile = positionedTopLeftTile;

            string[,][] positionedTiles;
            positionedTiles = new string[tilesPerSquareSide, tilesPerSquareSide][];
            positionedTiles[0, 0] = positionedTopLeftTile;

            // Determine position of other tiles starting from top left
            for (int i = 0; i < tilesPerSquareSide; i++)
            {
                bool invalidCombination = false;

                for (int j = 0; j < tilesPerSquareSide; j++)
                {
                    // Top left is already determined
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    // If left tile is already determined
                    if (j > 0)
                    {
                        int leftTileId = tilesPositions[i, j - 1];
                        string rightBorder = GetBorder(positionedTiles[i, j - 1], RIGHT_BORDER);

                        List<SharedBorder> leftTileSharedBorders = tilesSharedBorders[leftTileId];
                        foreach (SharedBorder leftTileSharedBorder in leftTileSharedBorders)
                        {
                            if (leftTileSharedBorder.SecondBorder == rightBorder
                                || ReverseString(leftTileSharedBorder.SecondBorder) == rightBorder)
                            {
                                tilesPositions[i, j] = leftTileSharedBorder.SecondTileId;
                                positionedTile = tiles[tilesPositions[i, j]];
                                while (GetBorder(positionedTile, LEFT_BORDER) != rightBorder)
                                {
                                    if (GetBorder(FlipVertically(positionedTile), LEFT_BORDER) == rightBorder)
                                    {
                                        positionedTile = FlipVertically(positionedTile);
                                        break;
                                    }
                                    else if (GetBorder(FlipHorizontally(positionedTile), LEFT_BORDER) == rightBorder)
                                    {
                                        positionedTile = FlipHorizontally(positionedTile);
                                        break;
                                    }
                                    else
                                    {
                                        positionedTile = RotateTile(positionedTile);
                                    }
                                }
                            }
                        }

                        positionedTiles[i, j] = positionedTile;
                    }
                    // If top bar is already defined
                    else if (i > 0)
                    {
                        int topTileId = tilesPositions[i - 1, j];
                        string bottomBorder = GetBorder(positionedTiles[i - 1, j], BOTTOM_BORDER);

                        List<SharedBorder> topTileSharedBorders = tilesSharedBorders[topTileId];
                        foreach (SharedBorder topTileSharedBorder in topTileSharedBorders)
                        {
                            // This tile must be top oriented
                            if (topTileSharedBorder.SecondBorder == bottomBorder)
                            {
                                tilesPositions[i, j] = topTileSharedBorder.SecondTileId;
                                positionedTile = tiles[tilesPositions[i, j]];
                                while (GetBorder(positionedTile, TOP_BORDER) != bottomBorder)
                                {
                                    if (GetBorder(FlipVertically(positionedTile), TOP_BORDER) == bottomBorder)
                                    {
                                        positionedTile = FlipVertically(positionedTile);
                                        break;
                                    }
                                    else if (GetBorder(FlipHorizontally(positionedTile), TOP_BORDER) == bottomBorder)
                                    {
                                        positionedTile = FlipHorizontally(positionedTile);
                                        break;
                                    }
                                    else
                                    {
                                        positionedTile = RotateTile(positionedTile);
                                    }
                                }
                            }
                        }

                        positionedTiles[i, j] = positionedTile;
                    }

                    if (tilesPositions[i, j] == 0)
                    {
                        invalidCombination = true;
                        break;
                    }
                }

                if (invalidCombination)
                {
                    break;
                }
            }


            return positionedTiles;
        }

        private string[] RotateTile(string[] tile)
        {
            int n = tile.Length;

            char[,] tile2d = new char[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tile2d[i, j] = tile[i][j];
                }
            }

            string[] rotatedTile = new string[n];

            for (int i = 0; i < n; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < n; j++)
                {
                    sb.Append(tile2d[n - j - 1, i]);
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

            for (int i = 0; i < tile.Length / 2; i++)
            {
                int n = tile[i].Length;
                for (int j = 0; j < n; j++)
                {
                    flippedTileCharArray[i, j] = tile[n - 1 - i][j];
                    flippedTileCharArray[n - 1 - i, j] = tile[i][j];
                }
            }

            string[] flippedTile = new string[tile.Length];
            for (int i = 0; i < flippedTileCharArray.GetLength(0); i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < flippedTileCharArray.GetLength(1); j++)
                {
                    sb.Append(flippedTileCharArray[i, j]);
                }

                flippedTile[i] = sb.ToString();
            }

            return flippedTile;
        }

        private string[,][] GetTilesWithRemovedBorders(string[,][] positionedTiles)
        {
            string[,][] tilesWithRemovedBorders =
                new string[positionedTiles.GetLength(0), positionedTiles.GetLength(1)][];

            for (int i = 0; i < positionedTiles.GetLength(0); i++)
            {
                for (int j = 0; j < positionedTiles.GetLength(1); j++)
                {
                    string[] tileWithRemovedBorders = new string[positionedTiles[i, j].Length - 2];

                    // Top and bottom border is removed
                    for (int k = 1; k < positionedTiles[i, j].Length - 1; k++)
                    {
                        // Left and right border is removed
                        string tileRow = positionedTiles[i, j][k];
                        tileWithRemovedBorders[k - 1] = tileRow[1..^1];
                    }

                    tilesWithRemovedBorders[i, j] = tileWithRemovedBorders;
                }
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
            // Sea monster pattern
            //                   #
            // #    ##    ##    ###
            //  #  #  #  #  #  #

            int seaMonsters = 0;

            for (int i = 0; i < image.Length; i++)
            {
                string row = image[i];
                for (int j = 0; j < row.Length; j++)
                {
                    if (j + 15 < row.Length && row[j] == '#' && row[j + 3] == '#' && row[j + 6] == '#'
                        && row[j + 9] == '#' && row[j + 12] == '#' && row[j + 15] == '#')
                    {
                        if (i - 1 >= 0)
                        {
                            row = image[i - 1];
                            if (j > 0 && j + 18 < row.Length && row[j - 1] == '#' && row[j + 4] == '#'
                                && row[j + 5] == '#' && row[j + 10] == '#' && row[j + 11] == '#'
                                && row[j + 16] == '#' && row[j + 17] == '#' && row[j + 18] == '#')
                            {
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

        private string GetBorder(string[] tile, int borderOrientation)
        {
            StringBuilder sb = new StringBuilder();

            switch (borderOrientation)
            {
                case TOP_BORDER:
                    return tile[0];
                case RIGHT_BORDER:
                    sb = new StringBuilder();
                    foreach (string row in tile)
                    {
                        sb.Append(row[^1]);
                    }
                    return sb.ToString();
                case BOTTOM_BORDER:
                    return tile[^1];
                case LEFT_BORDER:
                    sb = new StringBuilder();
                    foreach (string row in tile)
                    {
                        sb.Append(row[0]);
                    }
                    return sb.ToString();
            }

            return null;
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
