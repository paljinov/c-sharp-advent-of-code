using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day21
{
    public class InstructionsRepository
    {
        public int GetInstructionPointer(string input)
        {
            string instructionPointerString =
                input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)[0].Trim();

            Regex instructionPointerRegex = new Regex(@"^#ip\s(\d+)$");
            Match instructionPointerMatch = instructionPointerRegex.Match(instructionPointerString);

            int instructionPointer = int.Parse(instructionPointerMatch.Groups[1].Value);

            return instructionPointer;
        }

        public Instruction[] GetInstructions(string input)
        {
            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Instruction[] instructions = new Instruction[instructionsString.Length - 1];

            Regex instructionRegex = new Regex(@"^(\w+)\s(\d+)\s(\d+)\s(\d+)$");

            for (int i = 1; i < instructionsString.Length; i++)
            {
                string instructionString = instructionsString[i].Trim();
                Match instructionMatch = instructionRegex.Match(instructionString);
                GroupCollection instructionGroups = instructionMatch.Groups;

                InstructionType instructionType;
                switch (instructionGroups[1].Value)
                {
                    case "addr":
                        instructionType = InstructionType.AddRegister;
                        break;
                    case "addi":
                        instructionType = InstructionType.AddImmediate;
                        break;

                    case "mulr":
                        instructionType = InstructionType.MultiplyRegister;
                        break;
                    case "muli":
                        instructionType = InstructionType.MultiplyImmediate;
                        break;
                    case "banr":
                        instructionType = InstructionType.BitwiseAndRegister;
                        break;
                    case "bani":
                        instructionType = InstructionType.BitwiseAndImmediate;
                        break;
                    case "borr":
                        instructionType = InstructionType.BitwiseOrRegister;
                        break;
                    case "bori":
                        instructionType = InstructionType.BitwiseOrImmediate;
                        break;
                    case "setr":
                        instructionType = InstructionType.SetRegister;
                        break;
                    case "seti":
                        instructionType = InstructionType.SetImmediate;
                        break;
                    case "gtir":
                        instructionType = InstructionType.GreaterThanImmediateRegister;
                        break;
                    case "gtri":
                        instructionType = InstructionType.GreaterThanRegisterImmediate;
                        break;
                    case "gtrr":
                        instructionType = InstructionType.GreaterThanRegisterRegister;
                        break;
                    case "eqir":
                        instructionType = InstructionType.EqualImmediateRegister;
                        break;
                    case "eqri":
                        instructionType = InstructionType.EqualRegisterImmediate;
                        break;
                    case "eqrr":
                    default:
                        instructionType = InstructionType.EqualRegisterRegister;
                        break;
                }

                Instruction instruction = new Instruction
                {
                    InstructionType = instructionType,
                    InputA = int.Parse(instructionGroups[2].Value),
                    InputB = int.Parse(instructionGroups[3].Value),
                    OutputC = int.Parse(instructionGroups[4].Value)
                };

                instructions[i - 1] = instruction;
            }

            return instructions;
        }
    }
}
