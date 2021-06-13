using System.Collections.Generic;

namespace App.Tasks.Year2019.Day7
{
    public class Permutations
    {
        public List<List<int>> GetPermutations(int from, int to)
        {
            int[] integers = new int[to - from + 1];

            int i = 0;
            for (int j = from; j <= to; j++)
            {
                integers[i] = j;
                i++;
            }

            List<List<int>> permutations = new List<List<int>>();
            return DoPermute(integers, 0, integers.Length - 1, permutations);
        }

        private List<List<int>> DoPermute(int[] integers, int start, int end, List<List<int>> permutations)
        {
            if (start == end)
            {
                permutations.Add(new List<int>(integers));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref integers[start], ref integers[i]);
                    DoPermute(integers, start + 1, end, permutations);
                    Swap(ref integers[start], ref integers[i]);
                }
            }

            return permutations;
        }

        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
