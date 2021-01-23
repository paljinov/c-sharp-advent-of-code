/*
--- Part Two ---

What do you get if you multiply together the values of one chip in each of
outputs 0, 1, and 2?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2016.Day10
{
    public class Part2 : ITask<int>
    {
        private readonly BotsInstructionsRepository botsInstructionsRepository;

        private readonly HandChips handChips;

        public Part2()
        {
            botsInstructionsRepository = new BotsInstructionsRepository();
            handChips = new HandChips();
        }

        public int Solution(string input)
        {
            List<IBotInstruction> botsInstructions = botsInstructionsRepository.GetBotsInstructions(input);
            int botNumberWhichComparesChips = handChips.FirstThreeOutputsProduct(botsInstructions);

            return botNumberWhichComparesChips;
        }
    }
}
