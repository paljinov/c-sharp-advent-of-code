/*
--- Part Two ---

The Elves in accounting are thankful for your help; one of them even offers you
a starfish coin they had left over from a past vacation. They offer you a second
one if you can find three numbers in your expense report that meet the same
criteria.

Using the above example again, the three entries that sum to 2020 are 979, 366,
and 675. Multiplying them together produces the answer, 241861950.

In your expense report, what is the product of the three entries that sum to
2020?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2020.Day1
{
    public class Part2 : ITask<int>
    {
        private readonly ExpenseReportRepository expenseReportRepository;

        public Part2()
        {
            expenseReportRepository = new ExpenseReportRepository();
        }

        public int Solution(string input)
        {
            int product = 0;

            List<int> entries = expenseReportRepository.GetEntries(input);

            for (int i = 0; i < entries.Count - 2; i++)
            {
                for (int j = i + 1; j < entries.Count - 1; j++)
                {
                    for (int k = i + 1; k < entries.Count; k++)
                    {
                        int sum = entries[i] + entries[j] + entries[k];
                        if (sum == 2020)
                        {
                            product = entries[i] * entries[j] * entries[k];
                            break;
                        }
                    }
                }
            }

            return product;
        }
    }
}
