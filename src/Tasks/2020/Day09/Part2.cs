/*
--- Part Two ---

The final step in breaking the XMAS encryption relies on the invalid number you
just found: you must find a contiguous set of at least two numbers in your list
which sum to the invalid number from step 1.

Again consider the above example:

35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576

In this list, adding up all of the numbers from 15 through 40 produces the
invalid number from step 1, 127. (Of course, the contiguous set of numbers in
your actual list might be much longer.)

To find the encryption weakness, add together the smallest and largest number in
this contiguous range; in this example, these are 15 and 47, producing 62.

What is the encryption weakness in your XMAS-encrypted list of numbers?
*/

using System.Linq;

namespace App.Tasks.Year2020.Day9
{
    public class Part2 : ITask<long>
    {
        private readonly NumbersRepository numbersRepository;

        private readonly NumbersFinder numbersFinder;

        public Part2()
        {
            numbersRepository = new NumbersRepository();
            numbersFinder = new NumbersFinder();
        }

        public long Solution(string input)
        {
            long[] numbers = numbersRepository.GetNumbers(input);
            long firstNumberNotFollowingRule = numbersFinder.FindFirstNumberWhichIsNotSumOfTwoPreambleNumbers(numbers);

            long[] contiguousSet = numbersFinder.FindContiguousSetOfAtLeastTwoNumbersWhichSumToInvalidNumber(
                numbers,
                firstNumberNotFollowingRule
            );

            long encryptionWeakness = contiguousSet.Min() + contiguousSet.Max();
            return encryptionWeakness;
        }
    }
}
