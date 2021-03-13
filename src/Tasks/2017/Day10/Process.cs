using System.Collections.Generic;

namespace App.Tasks.Year2017.Day10
{
    public class Process
    {
        private readonly int circularListLength = 256;

        public int CalculateFirstTwoCircularListNumbersProduct(List<int> lengthsSequence)
        {
            Dictionary<int, int> circularList = InitializeCircularList();
            int position = 0;
            int skipSize = 0;

            foreach (int length in lengthsSequence)
            {
                // Stack if LIFO data structure so it will assure reverse sublist order
                Stack<int> sublist = GetSublist(circularList, length, position);
                UpdateCircularList(circularList, sublist, position);

                // Update position and skip size
                position = (position + length + skipSize) % circularList.Count;
                skipSize++;
            }

            int product = circularList[0] * circularList[1];

            return product;
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
