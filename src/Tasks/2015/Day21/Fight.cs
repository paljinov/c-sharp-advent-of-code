using System;
using System.Collections.Generic;

namespace App.Tasks.Year2015.Day21
{
    public class Fight
    {
        private const int PlayerHitPoints = 100;

        public int CalculateLeastAmountOfGoldSpentWhenWinning(
            Dictionary<string, Item> weapons,
            Dictionary<string, Item> armors,
            Dictionary<string, Item> rings,
            FighterStats bossStats
        )
        {
            int leastAmountOfGoldSpent = int.MaxValue;

            List<(int, FighterStats)> playerStatsPermutations = GetPlayerStatsPermutations(weapons, armors, rings);
            foreach ((int cost, FighterStats playerStats) in playerStatsPermutations)
            {
                bool isPlayerWinning = IsPlayerWinning(playerStats, new FighterStats
                {
                    HitPoints = bossStats.HitPoints,
                    Damage = bossStats.Damage,
                    Armor = bossStats.Armor
                });

                // If player is winning battle
                if (isPlayerWinning)
                {
                    leastAmountOfGoldSpent = Math.Min(cost, leastAmountOfGoldSpent);
                }
            }

            return leastAmountOfGoldSpent;
        }

        public int CalculateMostAmountOfGoldSpentWhenLosing(
            Dictionary<string, Item> weapons,
            Dictionary<string, Item> armors,
            Dictionary<string, Item> rings,
            FighterStats bossStats
        )
        {
            int mostAmountOfGoldSpent = 0;

            List<(int, FighterStats)> playerStatsPermutations = GetPlayerStatsPermutations(weapons, armors, rings);
            foreach ((int cost, FighterStats playerStats) in playerStatsPermutations)
            {
                bool isPlayerWinning = IsPlayerWinning(playerStats, new FighterStats
                {
                    HitPoints = bossStats.HitPoints,
                    Damage = bossStats.Damage,
                    Armor = bossStats.Armor
                });

                // If player is losing battle
                if (!isPlayerWinning)
                {
                    mostAmountOfGoldSpent = Math.Max(cost, mostAmountOfGoldSpent);
                }
            }

            return mostAmountOfGoldSpent;
        }

        private List<(int, FighterStats)> GetPlayerStatsPermutations(
            Dictionary<string, Item> weapons,
            Dictionary<string, Item> armors,
            Dictionary<string, Item> rings
        )
        {
            List<(int, FighterStats)> playerStatsPermutations = new List<(int, FighterStats)>();

            foreach (KeyValuePair<string, Item> weapon in weapons)
            {
                foreach (KeyValuePair<string, Item> armor in armors)
                {
                    foreach (KeyValuePair<string, Item> leftHandRing in rings)
                    {
                        foreach (KeyValuePair<string, Item> rightHandRing in rings)
                        {
                            // The shop only has one kind of ring, and you can buy 0-2 rings
                            if ((leftHandRing.Key != rightHandRing.Key)
                                || (leftHandRing.Key == "None" && rightHandRing.Key == "None"))
                            {
                                int cost = weapon.Value.Cost + armor.Value.Cost
                                    + leftHandRing.Value.Cost + rightHandRing.Value.Cost;

                                FighterStats player = new FighterStats
                                {
                                    HitPoints = PlayerHitPoints,
                                    Damage = weapon.Value.Damage
                                        + leftHandRing.Value.Damage + rightHandRing.Value.Damage,
                                    Armor = armor.Value.Armor
                                        + leftHandRing.Value.Armor + rightHandRing.Value.Armor
                                };

                                playerStatsPermutations.Add((cost, player));
                            }
                        }
                    }
                }
            }

            return playerStatsPermutations;
        }

        private bool IsPlayerWinning(FighterStats playerStats, FighterStats bossStats)
        {
            // Player is attacking first
            bool playerAttacks = true;

            while (playerStats.HitPoints > 0 && bossStats.HitPoints > 0)
            {
                // Player attacks
                if (playerAttacks)
                {
                    bossStats.HitPoints -= Math.Max(1, playerStats.Damage - bossStats.Armor);
                }
                // Boss attacks
                else
                {
                    playerStats.HitPoints -= Math.Max(1, bossStats.Damage - playerStats.Armor);
                }

                playerAttacks = !playerAttacks;
            }

            if (playerStats.HitPoints > 0)
            {
                return true;
            }

            return false;
        }
    }
}
