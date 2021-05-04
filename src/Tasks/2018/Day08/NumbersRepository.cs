using System;
using System.Collections.Generic;

namespace App.Tasks.Year2018.Day8
{
    public class NumbersRepository
    {
        public Queue<int> GetNumbers(string input)
        {
            Queue<int> numbers = new Queue<int>();

            string[] numbersArray = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < numbersArray.Length; i++)
            {
                numbers.Enqueue(int.Parse(numbersArray[i]));
            }

            return numbers;
        }
    }
}
