using System;

namespace App.Tasks.Year2019.Day7
{
    public class IntegersRepository
    {
        public int[] GetIntegers(string input)
        {
            string[] integersString = input.Split(',');

            int[] integers = new int[integersString.Length];
            for (int i = 0; i < integersString.Length; i++)
            {
                integers[i] = int.Parse(integersString[i]);
            }

            return integers;
        }
    }
}
