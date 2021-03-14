using System;
using System.Collections.Generic;
using System.Text;

namespace App.Tasks.Year2017.Day10
{
    public class Process
    {
        private readonly int circularListLength = 256;

        public int CalculateFirstTwoCircularListNumbersProduct(List<int> lengthsSequence)
        {
            Dictionary<int, int> circularList = DoProcess(lengthsSequence, 1);

            int product = circularList[0] * circularList[1];

            return product;
        }

        public string CalculateKnotHash(List<int> lengthsSequence, int totalRounds, int reduceSparseHashTimes)
        {
            Dictionary<int, int> sparseHash = DoProcess(lengthsSequence, totalRounds);

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

        private Dictionary<int, int> DoProcess(List<int> lengthsSequence, int totalRounds)
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
            for (int i = 0; i < circularListLength; i++)
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
    }
}
