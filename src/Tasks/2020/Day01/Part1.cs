/*
--- Day 1: Report Repair ---

After saving Christmas five years in a row, you've decided to take a vacation at
a nice resort on a tropical island. Surely, Christmas will go on without you.

The tropical island has its own currency and is entirely cash-only. The gold
coins used there have a little picture of a starfish; the locals just call them
stars. None of the currency exchanges seem to have heard of them, but somehow,
you'll need to find fifty of these coins by the time you arrive so you can pay
the deposit on your room.

To save your vacation, you need to get all fifty stars by December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day
in the Advent calendar; the second puzzle is unlocked when you complete the
first. Each puzzle grants one star. Good luck!

Before you leave, the Elves in accounting just need you to fix your expense
report (your puzzle input); apparently, something isn't quite adding up.

Specifically, they need you to find the two entries that sum to 2020 and then
multiply those two numbers together.

For example, suppose your expense report contained the following:

1721
979
366
299
675
1456

In this list, the two entries that sum to 2020 are 1721 and 299. Multiplying
them together produces 1721 * 299 = 514579, so the correct answer is 514579.

Of course, your expense report is much larger. Find the two entries that sum to
2020; what do you get if you multiply them together?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2020.Day1
{
    public class Part1 : ITask<int>
    {
        private readonly ExpenseReportRepository expenseReportRepository;

        public Part1()
        {
            expenseReportRepository = new ExpenseReportRepository();
        }

        public int Solution(string input)
        {
            int product = 0;

            List<int> entries = expenseReportRepository.GetEntries(input);

            for (int i = 0; i < entries.Count - 1; i++)
            {
                for (int j = i + 1; j < entries.Count; j++)
                {
                    int sum = entries[i] + entries[j];
                    if (sum == 2020)
                    {
                        product = entries[i] * entries[j];
                        break;
                    }
                }
            }

            return product;
        }
    }
}
