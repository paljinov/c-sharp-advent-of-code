using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day7
{
    public class InstructionsRepository
    {
        public HashSet<(char, char)> GetInstructions(string input)
        {
            HashSet<(char, char)> instructions = new HashSet<(char, char)>();

            Regex instructionRegex =
                new Regex(@"^Step\s([A-Z])\smust\sbe\sfinished\sbefore\sstep\s([A-Z])\scan\sbegin.$");
            string[] instructionsArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string instructionString in instructionsArray)
            {
                Match instructionMatch = instructionRegex.Match(instructionString);
                GroupCollection instructionGroups = instructionMatch.Groups;

                instructions.Add((instructionGroups[1].Value[0], instructionGroups[2].Value[0]));
            }

            return instructions;
        }
    }
}
