using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day8
{
    public class InstructionsRepository
    {
        private const string OPERATION_ACCUMULATOR = "acc";

        private const string OPERATION_JUMP = "jmp";

        public List<Instruction> GetInstructions(string input)
        {
            List<Instruction> instructions = new List<Instruction>();

            string[] instructionsString = input.Split(Environment.NewLine);
            Regex instructionRegex = new Regex(@"^(\w+)\s([+\-]\d+)$");

            foreach (string instructionString in instructionsString)
            {
                Match instructionMatch = instructionRegex.Match(instructionString);
                GroupCollection instructionGroups = instructionMatch.Groups;

                Operation operation = Operation.NoOperation;
                switch (instructionGroups[1].Value)
                {
                    case OPERATION_ACCUMULATOR:
                        operation = Operation.Accumulator;
                        break;
                    case OPERATION_JUMP:
                        operation = Operation.Jump;
                        break;
                }

                int argument = int.Parse(instructionGroups[2].Value);

                instructions.Add(new Instruction
                {
                    Operation = operation,
                    Argument = argument
                });
            }

            return instructions;
        }
    }
}
