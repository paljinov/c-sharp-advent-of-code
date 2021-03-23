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
            foreach (string knotHash in knotHashes)
            {
                string binary = ConvertHexToBinary(knotHash);
                foreach (char c in binary)
                {
                    if (c == '1')
                    {
                        usedSquares++;
                    }
                }
            }

            return usedSquares;
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
    }
}
