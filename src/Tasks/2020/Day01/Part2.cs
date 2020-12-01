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

        private readonly EntriesProduct entriesProduct;

        public Part2()
        {
            expenseReportRepository = new ExpenseReportRepository();
            entriesProduct = new EntriesProduct();
        }

        public int Solution(string input)
        {
            List<int> entries = expenseReportRepository.GetEntries(input);
            int product = entriesProduct.FindProductOfEntriesWhichSumTo(entries, 3);

            return product;
        }
    }
}
