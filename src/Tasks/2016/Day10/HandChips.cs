using System.Collections.Generic;

namespace App.Tasks.Year2016.Day10
{
    public class HandChips
    {
        private const int UNDEFINED_BOT = -1;

        private const int UNDEFINED_CHIP_VALUE = -1;

        public int BotNumberWhichComparesChips(
            List<IBotInstruction> botsInstructions,
            int lowerValueChip,
            int higherValueChip
        )
        {
            Dictionary<int, Bot> bots = new Dictionary<int, Bot>();
            Dictionary<int, int> output = new Dictionary<int, int>();

            while (botsInstructions.Count > 0)
            {
                foreach (IBotInstruction botInstruction in new List<IBotInstruction>(botsInstructions))
                {
                    if (botInstruction is TakeChipInstruction takeChipInstruction)
                    {
                        TakeChipInstruction(bots, takeChipInstruction);
                        botsInstructions.Remove(botInstruction);

                        Bot bot = bots[takeChipInstruction.BotNumber];
                        if (IsBotWhichComparesChips(bot, lowerValueChip, higherValueChip))
                        {
                            return takeChipInstruction.BotNumber;
                        }
                    }
                    else if (botInstruction is GiveChipsInstruction giveChipsInstruction)
                    {
                        // Each bot only proceeds when it has two microchips
                        if (bots.ContainsKey(giveChipsInstruction.BotNumber)
                            && bots[giveChipsInstruction.BotNumber].LowerValueChip != UNDEFINED_CHIP_VALUE)
                        {
                            Bot bot = bots[giveChipsInstruction.BotNumber];
                            if (IsBotWhichComparesChips(bot, lowerValueChip, higherValueChip))
                            {
                                return giveChipsInstruction.BotNumber;
                            }

                            GiveChipsInstruction(bots, output, giveChipsInstruction);
                            botsInstructions.Remove(botInstruction);
                        }
                    }
                }
            }

            return UNDEFINED_BOT;
        }

        private void TakeChipInstruction(Dictionary<int, Bot> bots, TakeChipInstruction takeChipInstruction)
        {
            GiveChipToBot(bots, takeChipInstruction.BotNumber, takeChipInstruction.ChipValue);
        }

        private void GiveChipsInstruction(
            Dictionary<int, Bot> bots,
            Dictionary<int, int> output,
            GiveChipsInstruction giveChipsInstruction
        )
        {
            Bot bot = bots[giveChipsInstruction.BotNumber];

            if (giveChipsInstruction.LowerValueChipToOutput)
            {
                output[giveChipsInstruction.LowerValueChipTo] = bot.LowerValueChip;
            }
            else
            {
                GiveChipToBot(bots, giveChipsInstruction.LowerValueChipTo, bot.LowerValueChip);
            }

            if (giveChipsInstruction.HigherValueChipToOutput)
            {
                output[giveChipsInstruction.HigherValueChipTo] = bot.HigherValueChip;
            }
            else
            {
                GiveChipToBot(bots, giveChipsInstruction.HigherValueChipTo, bot.HigherValueChip);
            }

            bots.Remove(giveChipsInstruction.BotNumber);
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

        private bool IsBotWhichComparesChips(Bot bot, int lowerValueChip, int higherValueChip)
        {
            if (bot.LowerValueChip == lowerValueChip && bot.HigherValueChip == higherValueChip)
            {
                return true;
            }

            return false;
        }
    }
}
