using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day14
{
    public class Disk
    {
        private const int TOTAL_KNOT_HASHES = 128;

        private readonly KnotHash knotHash;

        public Disk()
        {
            knotHash = new KnotHash();
        }

        public int CalculateUsedSquares(string key)
        {
            int usedSquares = 0;

            List<string> knotHashes = CalculateKnotHashes(key);
            bool[,] binaryGrid = CalculateBinaryGrid(knotHashes);

            for (int i = 0; i < binaryGrid.GetLength(0); i++)
            {
                for (int j = 0; j < binaryGrid.GetLength(1); j++)
                {
                    if (binaryGrid[i, j])
                    {
                        usedSquares++;
                    }
                }
            }

            return usedSquares;
        }

        public int CountRegions(string key)
        {
            List<string> knotHashes = CalculateKnotHashes(key);
            bool[,] binaryGrid = CalculateBinaryGrid(knotHashes);

            bool[,] occupiedSquaresByRegions = new bool[binaryGrid.GetLength(0), binaryGrid.GetLength(1)];
            int regions = 0;

            for (int i = 0; i < binaryGrid.GetLength(0); i++)
            {
                for (int j = 0; j < binaryGrid.GetLength(1); j++)
                {
                    // If square is used and isn't occupied by other region
                    if (binaryGrid[i, j] && !occupiedSquaresByRegions[i, j])
                    {
                        FloodFill(i, j, binaryGrid, occupiedSquaresByRegions);
                        regions++;
                    }
                }
            }

            return regions;
        }

        private bool[,] CalculateBinaryGrid(List<string> knotHashes)
        {
            bool[,] binaryGrid = new bool[knotHashes.Count, knotHashes.First().Length * 4];

            for (int i = 0; i < knotHashes.Count; i++)
            {
                string binary = ConvertHexToBinary(knotHashes[i]);
                for (int j = 0; j < binary.Length; j++)
                {
                    if (binary[j] == '1')
                    {
                        binaryGrid[i, j] = true;
                    }
                }
            }

            return binaryGrid;
        }

        private List<string> CalculateKnotHashes(string key)
        {
            List<string> knotHashes = new List<string>();

            for (int number = 0; number < TOTAL_KNOT_HASHES; number++)
            {
                string knotHash = this.knotHash.CalculateKnotHashForKeyAndNumber(key, number);
                knotHashes.Add(knotHash);
            }

            return knotHashes;
        }

        private string ConvertHexToBinary(string hex)
        {
            string binary = string.Join(
                string.Empty,
                hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
            );

            return binary;
        }

        private void FloodFill(int i, int j, bool[,] binaryGrid, bool[,] occupiedSquaresByRegions)
        {
            // If square is used and isn't occupied by other region
            if (binaryGrid[i, j] && !occupiedSquaresByRegions[i, j])
            {
                occupiedSquaresByRegions[i, j] = true;

                // Go left
                if (i > 0)
                {
                    FloodFill(i - 1, j, binaryGrid, occupiedSquaresByRegions);
                }

                // Go right
                if (i < occupiedSquaresByRegions.GetLength(0) - 1)
                {
                    FloodFill(i + 1, j, binaryGrid, occupiedSquaresByRegions);
                }

                // Go up
                if (j > 0)
                {
                    FloodFill(i, j - 1, binaryGrid, occupiedSquaresByRegions);
                }

                // Go down
                if (j < occupiedSquaresByRegions.GetLength(1) - 1)
                {
                    FloodFill(i, j + 1, binaryGrid, occupiedSquaresByRegions);
                }
            }
        }
    }
}
