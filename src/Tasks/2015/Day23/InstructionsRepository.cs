using System;
using System.Collections.Generic;

namespace App.Tasks.Year2015.Day23
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
                char? register = null;
                int? offset = null;

                switch (instructionSplitted[0])
                {
                    case "tpl":
                        instructionType = InstructionType.TripleCurrentValue;
                        register = instructionSplitted[1][0];
                        break;
                    case "inc":
                        instructionType = InstructionType.Increment;
                        register = instructionSplitted[1][0];
                        break;
                    case "jmp":
                        instructionType = InstructionType.JumpOffset;
                        offset = int.Parse(instructionSplitted[1]);
                        break;
                    case "jie":
                        instructionType = InstructionType.JumpOffsetIfEven;
                        register = instructionSplitted[1][0];
                        offset = int.Parse(instructionSplitted[2]);
                        break;
                    case "jio":
                        instructionType = InstructionType.JumpOffsetIfOne;
                        register = instructionSplitted[1][0];
                        offset = int.Parse(instructionSplitted[2]);
                        break;
                    default:
                        instructionType = InstructionType.HalfCurrentValue;
                        register = instructionSplitted[1][0];
                        break;
                }

                Instruction instruction = new Instruction
                {
                    InstructionType = instructionType,
                    Register = register,
                    Offset = offset
                };

                instructions.Add(i, instruction);
            }

            return instructions;
        }
    }
}
