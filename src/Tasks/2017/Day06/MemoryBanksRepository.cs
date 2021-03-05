using System;

namespace App.Tasks.Year2017.Day6
{
    public class MemoryBanksRepository
    {
        public int[] GetBlocks(string input)
        {
            string[] blocksString = input.Split('\t', StringSplitOptions.RemoveEmptyEntries);

            int[] blocks = new int[blocksString.Length];

            for (int i = 0; i < blocksString.Length; i++)
            {
                blocks[i] = int.Parse(blocksString[i]);
            }

            return blocks;
        }
    }
}
