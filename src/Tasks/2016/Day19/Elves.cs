using System.Collections.Generic;

namespace App.Tasks.Year2016.Day19
{
    public class Elves
    {
        public int FindElfWhichGetsAllThePresents(int totalElves)
        {
            int elvesWithPresents = totalElves;

            Dictionary<int, int> elvesPresents = new Dictionary<int, int>();
            for (int elf = 1; elf <= totalElves; elf++)
            {
                elvesPresents.Add(elf, 1);
            }

            int currentElf = 0;
            // While only one elf has all the presents
            while (elvesWithPresents > 1)
            {
                currentElf++;
                if (currentElf > totalElves)
                {
                    currentElf = 1;
                }

                // If current elf has presents
                if (elvesPresents[currentElf] > 0)
                {
                    // Find next elf which has presents
                    int nextElf = currentElf;
                    while (nextElf == currentElf || elvesPresents[nextElf] == 0)
                    {
                        nextElf++;
                        if (nextElf > totalElves)
                        {
                            nextElf = 1;
                        }
                    }

                    // Add presents to current elf
                    elvesPresents[currentElf] += elvesPresents[nextElf];

                    // Remove presents from next elf
                    elvesPresents[nextElf] = 0;
                    elvesWithPresents--;
                }
            }

            return currentElf;
        }
    }
}
