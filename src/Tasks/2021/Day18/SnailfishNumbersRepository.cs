using System;

namespace App.Tasks.Year2021.Day18
{
    public class SnailfishNumbersRepository
    {
        public string[] GetSnailfishNumbers(string input)
        {
            string[] snailfishNumbers = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return snailfishNumbers;
        }
    }
}
