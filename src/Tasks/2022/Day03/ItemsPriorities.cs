using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2022.Day3
{
    public class ItemsPriorities
    {
        private const int LOWERCASE_ITEM_SUBSTRACT = 96;

        private const int UPPERCASE_ITEM_SUBSTRACT = 38;

        public int CalculatePrioritiesSumForItemsThatAppearInBothCompartments(string[] rucksacksItems)
        {
            int prioritiesSum = 0;

            for (int i = 0; i < rucksacksItems.Length; i++)
            {
                int halfLength = rucksacksItems[i].Length / 2;

                string firstCompartment = rucksacksItems[i][..halfLength];
                string secondCompartment = rucksacksItems[i].Substring(halfLength, halfLength);

                IEnumerable<char> itemsThatAppearInBothCompartments = firstCompartment.Intersect(secondCompartment);
                prioritiesSum += CalculatePrioritiesSum(itemsThatAppearInBothCompartments);
            }

            return prioritiesSum;
        }

        public int CalculatePrioritiesSumForItemsThatCorrespondToTheBadgesOfEachThreeElfGroup(string[] rucksacksItems)
        {
            int prioritiesSum = 0;

            for (int i = 0; i < rucksacksItems.Length; i += 3)
            {
                char badgeItem = rucksacksItems[i]
                    .Intersect(rucksacksItems[i + 1])
                    .Intersect(rucksacksItems[i + 2])
                    .First();

                prioritiesSum += GetItemPriority(badgeItem);
            }

            return prioritiesSum;
        }

        private int CalculatePrioritiesSum(IEnumerable<char> items)
        {
            int prioritiesSum = 0;

            foreach (char item in items)
            {
                prioritiesSum += GetItemPriority(item);
            }

            return prioritiesSum;
        }

        private int GetItemPriority(char item)
        {
            int itemPriority;

            if (char.IsLower(item))
            {
                itemPriority = item - LOWERCASE_ITEM_SUBSTRACT;
            }
            else
            {
                itemPriority = item - UPPERCASE_ITEM_SUBSTRACT;
            }

            return itemPriority;
        }
    }
}
