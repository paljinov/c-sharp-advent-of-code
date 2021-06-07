using System.Collections.Generic;

namespace App.Tasks.Year2015.Day22
{
    public class WizardSimulatorGame
    {
        private readonly int playerHitPoints = 50;

        private readonly int playerManaPoints = 500;

        public int CalculateLeastAmountOfManaSpentWhenWinning(
            Dictionary<string, Effect> effects,
            FighterStats bossStats,
            bool hardDifficulty
        )
        {
            FighterStats playerStats = new FighterStats
            {
                HitPoints = playerHitPoints,
                Damage = 0,
                ManaPoints = playerManaPoints
            };

            int leastAmountOfManaSpentWhenWinning = int.MaxValue;

            FightPermutations(
                (FighterStats)playerStats.Clone(),
                (FighterStats)bossStats.Clone(),
                effects,
                new Dictionary<string, Effect>(),
                true,
                0,
                hardDifficulty,
                ref leastAmountOfManaSpentWhenWinning
            );

            return leastAmountOfManaSpentWhenWinning;
        }

        private void FightPermutations(
            FighterStats playerStats,
            FighterStats bossStats,
            Dictionary<string, Effect> effects,
            Dictionary<string, Effect> activeEffects,
            bool playerTurn,
            int manaSpent,
            bool hardDifficulty,
            ref int leastAmountOfManaSpentWhenWinning
        )
        {
            // If playing on hard difficulty
            if (hardDifficulty)
            {
                playerStats.HitPoints -= 1;
                if (playerStats.HitPoints <= 0)
                {
                    return;
                }
            }

            int playerArmor = 0;
            foreach (KeyValuePair<string, Effect> activeEffect in activeEffects)
            {
                if (activeEffect.Value.Damage > 0)
                {
                    bossStats.HitPoints -= activeEffect.Value.Damage;
                    // If player won
                    if (bossStats.HitPoints <= 0)
                    {
                        leastAmountOfManaSpentWhenWinning = manaSpent;
                        return;
                    }
                }

                if (activeEffect.Value.Armor > 0)
                {
                    playerArmor = activeEffect.Value.Armor;
                }

                if (activeEffect.Value.Heal > 0)
                {
                    playerStats.HitPoints += activeEffect.Value.Heal;
                }

                if (activeEffect.Value.ManaRecharge > 0)
                {
                    playerStats.ManaPoints += activeEffect.Value.ManaRecharge;
                }

                activeEffect.Value.Turns--;
                // If effect expired
                if (activeEffect.Value.Turns == 0)
                {
                    activeEffects.Remove(activeEffect.Key);
                }
            }

            // Player turn
            if (playerTurn)
            {
                foreach (KeyValuePair<string, Effect> effect in effects)
                {
                    // Already active effect cannot be casted
                    if (!activeEffects.ContainsKey(effect.Key))
                    {
                        // If player can afford to cast spell
                        if (playerStats.ManaPoints - effect.Value.ManaCost >= 0)
                        {
                            // If mana spent is less than least amount of mana spent when winning
                            if (manaSpent + effect.Value.ManaCost <= leastAmountOfManaSpentWhenWinning)
                            {
                                Dictionary<string, Effect> newActiveEffects = CloneEffects(activeEffects);
                                newActiveEffects.Add(effect.Key, (Effect)effect.Value.Clone());

                                FighterStats newPlayerStats = (FighterStats)playerStats.Clone();
                                newPlayerStats.ManaPoints -= effect.Value.ManaCost;

                                FightPermutations(
                                    newPlayerStats,
                                    (FighterStats)bossStats.Clone(),
                                    effects,
                                    newActiveEffects,
                                    false,
                                    manaSpent + effect.Value.ManaCost,
                                    hardDifficulty,
                                    ref leastAmountOfManaSpentWhenWinning
                                );
                            }
                        }
                    }
                }
            }
            // Boss turn
            else
            {
                playerStats.HitPoints -= bossStats.Damage - playerArmor;
                // If boss won
                if (playerStats.HitPoints <= 0)
                {
                    return;
                }

                FightPermutations(
                    (FighterStats)playerStats.Clone(),
                    (FighterStats)bossStats.Clone(),
                    effects,
                    new Dictionary<string, Effect>(activeEffects),
                    true,
                    manaSpent,
                    hardDifficulty,
                    ref leastAmountOfManaSpentWhenWinning
                );
            }
        }

        private Dictionary<string, Effect> CloneEffects(Dictionary<string, Effect> effects)
        {
            Dictionary<string, Effect> clonedEffects = new Dictionary<string, Effect>();
            foreach (KeyValuePair<string, Effect> effect in effects)
            {
                clonedEffects.Add(effect.Key, (Effect)effect.Value.Clone());
            }

            return clonedEffects;
        }
    }
}
