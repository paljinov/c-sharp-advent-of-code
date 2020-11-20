using System;

namespace App.Tasks.Year2016.Day2
{
    public class InstructionsRepository
    {
        public string[] ParseInput(string input)
        {
            string[] instructions = input.Split(Environment.NewLine);

            return instructions;
        }
    }
}
