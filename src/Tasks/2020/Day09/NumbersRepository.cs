using System;

namespace App.Tasks.Year2020.Day9
{
    public class NumbersRepository
    {
        public long[] GetNumbers(string input)
        {
            string[] numbersRulesString = input.Split(Environment.NewLine);

            long[] numbers = new long[numbersRulesString.Length];

            for (int i = 0; i < numbersRulesString.Length; i++)
            {
                numbers[i] = long.Parse(numbersRulesString[i]);
            }

            return numbers;
        }
    }
}
