using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day23
{
    public class InstructionsRepository
    {
        public Dictionary<int, Instruction> GetInstructions(string input)
        {
            Dictionary<int, Instruction> instructions = new Dictionary<int, Instruction>();

            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < instructionsString.Length; i++)
            {
                string instructionString = instructionsString[i];
                string[] instructionSplitted = instructionString.Split(' ');

                InstructionType instructionType;
                switch (instructionSplitted[0])
                {
                    case "set":
                        instructionType = InstructionType.Set;
                        break;
                    case "sub":
                        instructionType = InstructionType.Decrease;
                        break;
                    case "mul":
                        instructionType = InstructionType.Multiply;
                        break;
                    default:
                        instructionType = InstructionType.Jump;
                        break;
                }

                string firstArgument = instructionSplitted[1];
                string secondArgument = null;
                if (instructionSplitted.Length > 2)
                {
                    secondArgument = instructionSplitted[2];
                }

                Instruction instruction = new Instruction
                {
                    InstructionType = instructionType,
                    FirstArgument = firstArgument,
                    SecondArgument = secondArgument
                };

                instructions.Add(i, instruction);
            }

            return instructions;
        }
    }
}
