using System;

namespace App.Tasks.Year2021.Day24
{
    public class InstructionsRepository
    {
        public Instruction[] GetInstructions(string input)
        {
            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Instruction[] instructions = new Instruction[instructionsString.Length];

            for (int i = 0; i < instructionsString.Length; i++)
            {
                string instructionString = instructionsString[i];
                string[] instructionSplitted = instructionString.Split(' ');

                InstructionType instructionType = InstructionType.Input;
                switch (instructionSplitted[0])
                {
                    case "add":
                        instructionType = InstructionType.Add;
                        break;
                    case "mul":
                        instructionType = InstructionType.Multiply;
                        break;
                    case "div":
                        instructionType = InstructionType.Divide;
                        break;
                    case "mod":
                        instructionType = InstructionType.Modulo;
                        break;
                    case "eql":
                        instructionType = InstructionType.Equal;
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

                instructions[i] = instruction;
            }

            return instructions;
        }
    }
}
