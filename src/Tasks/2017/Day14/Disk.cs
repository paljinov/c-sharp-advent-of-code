using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2017.Day14
{
    public class Disk
    {
        private const int TOTAL_KNOT_HASHES = 128;

        private const int REDUCE_SPARSE_HASH_TIMES = 16;

        private const int TOTAL_ROUNDS = 64;

        private const int CIRCULAR_LIST_LENGTH = 256;

        private const string LENGTHS_SEQUENCE_SUFFIX = "17, 31, 73, 47, 23";


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
                string hashInput = $"{key}-{number}";
                List<int> lengthsSequence = GetAsciiCodesLengthsSequence(hashInput);
                lengthsSequence = lengthsSequence.Concat(GetLengthsSequence(LENGTHS_SEQUENCE_SUFFIX)).ToList();

                string knotHash = CalculateKnotHash(lengthsSequence, TOTAL_ROUNDS, REDUCE_SPARSE_HASH_TIMES);
                knotHashes.Add(knotHash);
            }

            return knotHashes;
        }

        private string CalculateKnotHash(List<int> lengthsSequence, int totalRounds, int reduceSparseHashTimes)
        {
            Dictionary<int, int> sparseHash = CalculateSparseHash(lengthsSequence, totalRounds);

            List<int> denseHash = new List<int>();
            for (int i = 0; i < sparseHash.Count; i += reduceSparseHashTimes)
            {
                int result = sparseHash[i];
                for (int j = i + 1; j < i + reduceSparseHashTimes; j++)
                {
                    result ^= sparseHash[j];
                }

                denseHash.Add(result);
            }

            StringBuilder knotHash = new StringBuilder();
            foreach (int number in denseHash)
            {
                knotHash.Append(number.ToString("x2"));
            }

            return knotHash.ToString();
        }

        private Dictionary<int, int> CalculateSparseHash(List<int> lengthsSequence, int totalRounds)
        {
            Dictionary<int, int> circularList = InitializeCircularList();
            int position = 0;
            int skipSize = 0;

            for (int i = 0; i < totalRounds; i++)
            {
                foreach (int length in lengthsSequence)
                {
                    // Stack if LIFO data structure so it will assure reverse sublist order
                    Stack<int> sublist = GetSublist(circularList, length, position);
                    UpdateCircularList(circularList, sublist, position);

                    // Update position and skip size
                    position = (position + length + skipSize) % circularList.Count;
                    skipSize++;
                }
            }

            return circularList;
        }

        private Dictionary<int, int> InitializeCircularList()
        {
            Dictionary<int, int> circularList = new Dictionary<int, int>();
            for (int i = 0; i < CIRCULAR_LIST_LENGTH; i++)
            {
                circularList.Add(i, i);
            }

            return circularList;
        }

        private Stack<int> GetSublist(Dictionary<int, int> circularList, int length, int position)
        {
            Stack<int> sublist = new Stack<int>();
            while (sublist.Count < length)
            {
                if (position >= circularList.Count)
                {
                    position = 0;
                }

                sublist.Push(circularList[position]);
                position++;
            }

            return sublist;
        }

        private void UpdateCircularList(Dictionary<int, int> circularList, Stack<int> sublist, int position)
        {
            while (sublist.Count > 0)
            {
                if (position >= circularList.Count)
                {
                    position = 0;
                }

                circularList[position] = sublist.Pop();
                position++;
            }
        }

        private List<int> GetLengthsSequence(string input)
        {
            List<int> lengthsSequence = new List<int>();

            string[] lengthsSequenceString = input.Split(',');
            foreach (string length in lengthsSequenceString)
            {
                lengthsSequence.Add(int.Parse(length));
            }

            return lengthsSequence;
        }

        private List<int> GetAsciiCodesLengthsSequence(string input)
        {
            List<int> lengthsSequence = new List<int>();
            foreach (char c in input)
            {
                lengthsSequence.Add((int)c);
            }

            return lengthsSequence;
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
