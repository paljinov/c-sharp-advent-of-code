/*
--- Part Two ---

Turns out the shopkeeper is working with the boss, and can persuade you to buy
whatever items he wants. The other rules still apply, and he still only has one
of each item.

What is the most amount of gold you can spend and still lose the fight?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day21
{
    public class Part2 : ITask<int>
    {
        private readonly ShopItemRepository shopItemRepository;

        private readonly BossStatsRepository bossStatsRepository;

        private readonly Fight fight;

        public Part2()
        {
            shopItemRepository = new ShopItemRepository();
            bossStatsRepository = new BossStatsRepository();
            fight = new Fight();
        }

        public int Solution(string input)
        {
            Dictionary<string, Item> weapons = shopItemRepository.GetWeapons();
            Dictionary<string, Item> armors = shopItemRepository.GetArmors();
            Dictionary<string, Item> rings = shopItemRepository.GetRings();
            FighterStats bossStats = bossStatsRepository.GetBossStats(input);

            int mostAmountOfGoldSpent = fight.CalculateMostAmountOfGoldSpentWhenLosing(
                weapons,
                armors,
                rings,
                bossStats
            );

            return mostAmountOfGoldSpent;
        }
    }
}
