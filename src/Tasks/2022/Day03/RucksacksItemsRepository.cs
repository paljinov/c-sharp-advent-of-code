using System;

namespace App.Tasks.Year2022.Day3
{
    public class RucksacksItemsRepository
    {
        public string[] GetRucksacksItems(string input)
        {
            string[] rucksacksItems = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return rucksacksItems;
        }
    }
}
