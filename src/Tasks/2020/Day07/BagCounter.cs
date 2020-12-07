using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day7
{
    public class BagCounter
    {
        private const string SHINY_GOLD = "shiny gold";

        public int CountBagsWhichContainShinyGoldBag(Dictionary<string, Dictionary<string, int>> bags)
        {
            int shinyBags = 0;

            foreach (KeyValuePair<string, Dictionary<string, int>> bag in bags)
            {
                if (ShinyBagCounter(bags, bag.Key))
                {
                    shinyBags++;
                }
            }

            return shinyBags;
        }

        public int CountShinyGoldBagContains(Dictionary<string, Dictionary<string, int>> bags)
        {
            int shinyGoldBagContains = 0;

            CountShinyGoldBagContainsCounter(bags, SHINY_GOLD, 1, ref shinyGoldBagContains);

            return shinyGoldBagContains;
        }

        private bool ShinyBagCounter(
            Dictionary<string, Dictionary<string, int>> bags,
            string currentBagType
        )
        {
            Dictionary<string, int> containedBags = bags[currentBagType];
            foreach (KeyValuePair<string, int> containedBag in containedBags)
            {
                if (containedBag.Key == SHINY_GOLD)
                {
                    return true;
                }
                else if (ShinyBagCounter(bags, containedBag.Key))
                {
                    return true;
                }
            }

            return false;
        }

        private void CountShinyGoldBagContainsCounter(
            Dictionary<string, Dictionary<string, int>> bags,
            string currentBagType,
            int currentBagTypeCount,
            ref int contains
        )
        {
            Dictionary<string, int> containedBags = bags[currentBagType];
            foreach (KeyValuePair<string, int> containedBag in containedBags)
            {
                int bagCount = currentBagTypeCount * containedBag.Value;
                contains += bagCount;

                CountShinyGoldBagContainsCounter(bags, containedBag.Key, bagCount, ref contains);
            }
        }
    }
}
