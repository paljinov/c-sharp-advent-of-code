using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2021.Day2
{
    public class InstructionsRepository
    {
        private const string DOWN = "down";

        private const string UP = "up";

        public Instruction[] GetInstructions(string input)
        {
            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Instruction[] instructions = new Instruction[instructionsString.Length];

            Regex instructionRegex = new Regex(@"^(\w+)\s(\d+)$");

            for (int i = 0; i < instructionsString.Length; i++)
            {
                Match instructionMatch = instructionRegex.Match(instructionsString[i]);
                GroupCollection instructionGroups = instructionMatch.Groups;

                Command command = Command.Forward;
                switch (instructionGroups[1].Value)
                {
                    case DOWN:
                        command = Command.Down;
                        break;
                    case UP:
                        command = Command.Up;
                        break;
                }

                Instruction instruction = new Instruction
                {
                    Command = command,
                    Value = int.Parse(instructionGroups[2].Value)
                };

                instructions[i] = instruction;
            }

            return instructions;
        }
    }
}
