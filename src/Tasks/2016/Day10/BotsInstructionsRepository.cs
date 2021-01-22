using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day10
{
    public class BotsInstructionsRepository
    {
        private const string OUTPUT = "output";

        public List<IBotInstruction> GetBotsInstructions(string input)
        {
            List<IBotInstruction> botsInstructions = new List<IBotInstruction>();

            string[] botsInstructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Regex takeChipInstructionRegex = new Regex(@"^value (\d+) goes to bot (\d+)$");
            Regex giveChipsInstructionRegex =
                new Regex(@"^bot (\d+) gives low to (output|bot) (\d+) and high to (output|bot) (\d+)$");

            foreach (string botsInstructionString in botsInstructionsString)
            {
                Match takeChipInstructionMatch = takeChipInstructionRegex.Match(botsInstructionString);
                if (takeChipInstructionMatch.Success)
                {
                    GroupCollection takeChipInstructionGroups = takeChipInstructionMatch.Groups;
                    botsInstructions.Add(new TakeChipInstruction
                    {
                        ChipValue = int.Parse(takeChipInstructionGroups[1].Value),
                        BotNumber = int.Parse(takeChipInstructionGroups[2].Value)
                    });
                }
                else
                {
                    Match giveChipsInstructionMatch = giveChipsInstructionRegex.Match(botsInstructionString);
                    GroupCollection giveChipsInstructionGroups = giveChipsInstructionMatch.Groups;
                    botsInstructions.Add(new GiveChipsInstruction
                    {
                        BotNumber = int.Parse(giveChipsInstructionGroups[1].Value),
                        LowerValueChipToOutput = giveChipsInstructionGroups[2].Value == OUTPUT,
                        LowerValueChipTo = int.Parse(giveChipsInstructionGroups[3].Value),
                        HigherValueChipToOutput = giveChipsInstructionGroups[4].Value == OUTPUT,
                        HigherValueChipTo = int.Parse(giveChipsInstructionGroups[5].Value),
                    });
                }
            }

            return botsInstructions;
        }
    }
}
