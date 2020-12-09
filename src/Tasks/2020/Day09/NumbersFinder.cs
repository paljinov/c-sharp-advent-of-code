namespace App.Tasks.Year2020.Day9
{
    public class NumbersFinder
    {
        private readonly int preamble = 25;

        public long FindFirstNumberWhichIsNotSumOfTwoPreambleNumbers(long[] numbers)
        {
            long firstNumberNotFollowingRule = 0;

            for (int i = preamble; i < numbers.Length; i++)
            {
                if (!IsNumberFollowingRule(numbers, numbers[i], i))
                {
                    firstNumberNotFollowingRule = numbers[i];
                    break;
                }
            }

            return firstNumberNotFollowingRule;
        }

        public long[] FindContiguousSetOfAtLeastTwoNumbersWhichSumToInvalidNumber(
            long[] numbers,
            long firstNumberNotFollowingRule
        )
        {
            int i = 0;
            int setStartIndex = 0;
            int setEndIndex = 0;
            long sum = 0;
            while (i < numbers.Length)
            {
                sum += numbers[i];

                if (sum == firstNumberNotFollowingRule)
                {
                    setEndIndex = i;
                    break;
                }
                else if (sum > firstNumberNotFollowingRule)
                {
                    setStartIndex += 1;
                    i = setStartIndex;
                    sum = 0;
                }

                i++;
            }

            long[] contiguousSet = new long[setEndIndex - setStartIndex];
            i = 0;
            for (int j = setStartIndex; j < setEndIndex; j++)
            {
                contiguousSet[i] = numbers[j];
                i++;
            }

            return contiguousSet;
        }

        private bool IsNumberFollowingRule(long[] numbers, long number, int index)
        {
            for (int i = index - preamble; i < index - 1; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    if (numbers[i] + numbers[j] == number)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
