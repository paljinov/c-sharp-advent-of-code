using System.Collections.Generic;

namespace App.Tasks.Year2016.Day10
{
    public class HandChips
    {
        private const int UNDEFINED_BOT = -1;

        private const int UNDEFINED_CHIP_VALUE = -1;

        public int NumberOfBotWhichComparesChips(
            List<IBotInstruction> botsInstructions,
            int lowerValueChip,
            int higherValueChip
        )
        {
            int numberOfBotWhichComparesChips = UNDEFINED_BOT;

            (Dictionary<int, Bot> bots, _) = HandChipsByInstructions(botsInstructions);

            foreach (KeyValuePair<int, Bot> bot in bots)
            {
                if (bot.Value.LowerValueChip == lowerValueChip && bot.Value.HigherValueChip == higherValueChip)
                {
                    numberOfBotWhichComparesChips = bot.Key;
                    break;
                }
            }

            return numberOfBotWhichComparesChips;
        }

        public int FirstThreeOutputsProduct(List<IBotInstruction> botsInstructions)
        {
            (_, Dictionary<int, int> outputs) = HandChipsByInstructions(botsInstructions);

            int product = 1;
            for (int i = 0; i < 3; i++)
            {
                product *= outputs[i];
            }

            return product;
        }

        private (Dictionary<int, Bot>, Dictionary<int, int>) HandChipsByInstructions(
            List<IBotInstruction> botsInstructions
        )
        {
            Dictionary<int, Bot> bots = new Dictionary<int, Bot>();
            Dictionary<int, int> outputs = new Dictionary<int, int>();

            while (botsInstructions.Count > 0)
            {
                foreach (IBotInstruction botInstruction in new List<IBotInstruction>(botsInstructions))
                {
                    if (botInstruction is TakeChipInstruction takeChipInstruction)
                    {
                        TakeChipInstruction(bots, takeChipInstruction);
                        botsInstructions.Remove(botInstruction);
                    }
                    else if (botInstruction is GiveChipsInstruction giveChipsInstruction)
                    {
                        // Each bot only proceeds when it has two microchips
                        if (bots.ContainsKey(giveChipsInstruction.BotNumber)
                            && bots[giveChipsInstruction.BotNumber].LowerValueChip != UNDEFINED_CHIP_VALUE)
                        {
                            GiveChipsInstruction(bots, outputs, giveChipsInstruction);
                            botsInstructions.Remove(botInstruction);
                        }
                    }
                }
            }

            return (bots, outputs);
        }

        private void TakeChipInstruction(Dictionary<int, Bot> bots, TakeChipInstruction takeChipInstruction)
        {
            GiveChipToBot(bots, takeChipInstruction.BotNumber, takeChipInstruction.ChipValue);
        }

        private void GiveChipsInstruction(
            Dictionary<int, Bot> bots,
            Dictionary<int, int> outputs,
            GiveChipsInstruction giveChipsInstruction
        )
        {
            Bot bot = bots[giveChipsInstruction.BotNumber];

            if (giveChipsInstruction.LowerValueChipToOutput)
            {
                outputs[giveChipsInstruction.LowerValueChipTo] = bot.LowerValueChip;
            }
            else
            {
                GiveChipToBot(bots, giveChipsInstruction.LowerValueChipTo, bot.LowerValueChip);
            }

            if (giveChipsInstruction.HigherValueChipToOutput)
            {
                outputs[giveChipsInstruction.HigherValueChipTo] = bot.HigherValueChip;
            }
            else
            {
                GiveChipToBot(bots, giveChipsInstruction.HigherValueChipTo, bot.HigherValueChip);
            }
        }

        private void GiveChipToBot(Dictionary<int, Bot> bots, int botNumber, int chipValue)
        {
            if (!bots.ContainsKey(botNumber))
            {
                bots[botNumber] = new Bot
                {
                    LowerValueChip = UNDEFINED_CHIP_VALUE,
                    HigherValueChip = chipValue
                };
            }
            else
            {
                if (bots[botNumber].HigherValueChip > chipValue)
                {
                    bots[botNumber].LowerValueChip = chipValue;
                }
                else
                {
                    bots[botNumber].LowerValueChip = bots[botNumber].HigherValueChip;
                    bots[botNumber].HigherValueChip = chipValue;
                }
            }
        }
    }
}
