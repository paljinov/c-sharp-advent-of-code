using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day1
{
    public class EntriesProduct
    {
        private const int SUM = 2020;

        public int FindProductOfEntriesWhichSumTo(List<int> entries, int entriesCount)
        {
            List<int> sumEntries = new List<int>();
            FindEntriesWhichSumTo(entries, entriesCount, 0, sumEntries);

            int product = 1;
            foreach (int sumEntry in sumEntries)
            {
                product *= sumEntry;
            }

            return product;
        }

        /// <summary>
        /// Find exact number of entries which give required sum.
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="entriesCount"></param>
        /// <param name="start"></param>
        /// <param name="sumEntries"></param>
        /// <returns>True if exact number of entries which give required sum is found, false otherwise.</returns>
        private bool FindEntriesWhichSumTo(List<int> entries, int entriesCount, int start, List<int> sumEntries)
        {
            entriesCount--;

            for (int i = start; i < entries.Count - entriesCount; i++)
            {
                sumEntries.Add(entries[i]);
                // If more entries needs to be added
                if (entriesCount > 0)
                {
                    if (FindEntriesWhichSumTo(entries, entriesCount, i + 1, sumEntries))
                    {
                        return true;
                    }
                }
                // If proper number of entries are added and sum is correct
                else if (sumEntries.Sum() == SUM)
                {
                    return true;
                }

                sumEntries.RemoveAt(sumEntries.Count - 1);
            }

            return false;
        }
    }
}
