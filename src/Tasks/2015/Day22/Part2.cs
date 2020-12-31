/*
--- Part Two ---

On the next run through the game, you increase the difficulty to hard.

At the start of each player turn (before any other effects apply), you lose 1
hit point. If this brings you to or below 0 hit points, you lose.

With the same starting stats for you and the boss, what is the least amount of
mana you can spend and still win the fight?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day22
{
    public class Part2 : ITask<int>
    {
        private readonly EffectsRepository effectsRepository;

        private readonly BossStatsRepository bossStatsRepository;

        private readonly WizardSimulatorGame wizardSimulatorGame;

        public Part2()
        {
            effectsRepository = new EffectsRepository();
            bossStatsRepository = new BossStatsRepository();
            wizardSimulatorGame = new WizardSimulatorGame();
        }

        public int Solution(string input)
        {
            Dictionary<string, Effect> effects = effectsRepository.GetEffects();
            FighterStats bossStats = bossStatsRepository.GetBossStats(input);

            int leastAmountOfMana = wizardSimulatorGame.CalculateLeastAmountOfManaSpentWhenWinning(
                effects,
                bossStats,
                true
            );

            return leastAmountOfMana;
        }
    }
}
