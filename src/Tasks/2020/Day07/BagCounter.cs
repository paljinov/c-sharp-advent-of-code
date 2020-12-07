using System.Collections.Generic;

namespace App.Tasks.Year2020.Day7
{
    public class BagCounter
    {
        private const string SHINY_GOLD = "shiny gold";

        public int CountBagsWhichContainAtLeastOneShinyGoldBag(Dictionary<string, Dictionary<string, int>> bags)
        {
            int bagsWhichContainAtLeastOneShinyGoldBag = 0;

            foreach (KeyValuePair<string, Dictionary<string, int>> bag in bags)
            {
                if (IsBagContainingProvidedBag(bags, SHINY_GOLD, bag.Key))
                {
                    bagsWhichContainAtLeastOneShinyGoldBag++;
                }
            }

            return bagsWhichContainAtLeastOneShinyGoldBag;
        }

        public int CountBagsRequiredInsideSingleShinyGoldBag(Dictionary<string, Dictionary<string, int>> bags)
        {
            int countBagsRequiredInsideSingleShinyGoldBag = 0;
            CountBagsRequiredInsideBag(bags, SHINY_GOLD, 1, ref countBagsRequiredInsideSingleShinyGoldBag);

            return countBagsRequiredInsideSingleShinyGoldBag;
        }

        private bool IsBagContainingProvidedBag(
            Dictionary<string, Dictionary<string, int>> bags,
            string targetBagColor,
            string currentBagColor
        )
        {
            Dictionary<string, int> containedBags = bags[currentBagColor];
            foreach (KeyValuePair<string, int> containedBag in containedBags)
            {
                if (containedBag.Key == targetBagColor)
                {
                    return true;
                }
                else if (IsBagContainingProvidedBag(bags, targetBagColor, containedBag.Key))
                {
                    return true;
                }
            }

            return false;
        }

        private void CountBagsRequiredInsideBag(
            Dictionary<string, Dictionary<string, int>> bags,
            string currentBagColor,
            int currentBagQuantity,
            ref int totalContainedBags
        )
        {
            Dictionary<string, int> containedBags = bags[currentBagColor];
            foreach (KeyValuePair<string, int> containedBag in containedBags)
            {
                int containedBagQuantity = currentBagQuantity * containedBag.Value;
                totalContainedBags += containedBagQuantity;

                CountBagsRequiredInsideBag(bags, containedBag.Key, containedBagQuantity, ref totalContainedBags);
            }
        }
    }
}
