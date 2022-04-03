using System;

namespace App.Tasks.Year2021.Day24
{
    public class InstructionsRepository
    {
        public string[] GetInstructions(string input)
        {
            string[] instructions = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return instructions;
        }
    }
}
