using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day23
{
    public class Coprocessor
    {
        private const string REGISTER_A = "a";

        private const string REGISTER_B = "b";

        private const string REGISTER_C = "c";

        private const string REGISTER_E = "e";

        private const string REGISTER_H = "h";

        public int CountMultiplicationInstructionInvocations(Dictionary<int, Instruction> instructions)
        {
            int multiplicationInstructionInvocations = 0;

            Dictionary<string, int> registers = InitializeRegisters(instructions);

            int i = 0;
            while (i < instructions.Count)
            {
                Instruction instruction = instructions[i];
                switch (instruction.InstructionType)
                {
                    case InstructionType.Set:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] = registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] = int.Parse(instruction.SecondArgument);
                        }
                        break;
                    case InstructionType.Decrease:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] -= registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] -= int.Parse(instruction.SecondArgument);
                        }
                        break;
                    case InstructionType.Multiply:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] *= registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] *= int.Parse(instruction.SecondArgument);
                        }

                        multiplicationInstructionInvocations++;
                        break;
                    case InstructionType.Jump:
                        bool jump = false;
                        if (registers.ContainsKey(instruction.FirstArgument))
                        {
                            if (registers[instruction.FirstArgument] != 0)
                            {
                                jump = true;
                            }
                        }
                        else
                        {
                            if (int.Parse(instruction.FirstArgument) != 0)
                            {
                                jump = true;
                            }
                        }

                        if (jump)
                        {
                            i += int.Parse(instruction.SecondArgument);
                            continue;
                        }
                        break;
                }

                i++;
            }

            return multiplicationInstructionInvocations;
        }

        public int CalculateRegisterHValueAfterProgramCompletion(
            Dictionary<int, Instruction> instructions,
            int registerAStartValue
        )
        {
            int multiplicationInstructionInvocations = 0;
            Dictionary<string, int> registers = InitializeRegisters(instructions, registerAStartValue);

            int i = 0;
            // Register "e" represents inner loop
            while (registers[REGISTER_E] == 0)
            {
                Instruction instruction = instructions[i];
                switch (instruction.InstructionType)
                {
                    case InstructionType.Set:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] = registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] = int.Parse(instruction.SecondArgument);
                        }
                        break;
                    case InstructionType.Decrease:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] -= registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] -= int.Parse(instruction.SecondArgument);
                        }
                        break;
                    case InstructionType.Multiply:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] *= registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] *= int.Parse(instruction.SecondArgument);
                        }

                        multiplicationInstructionInvocations++;
                        break;
                    case InstructionType.Jump:
                        bool jump = false;
                        if (registers.ContainsKey(instruction.FirstArgument))
                        {
                            if (registers[instruction.FirstArgument] != 0)
                            {
                                jump = true;
                            }
                        }
                        else
                        {
                            if (int.Parse(instruction.FirstArgument) != 0)
                            {
                                jump = true;
                            }
                        }

                        if (jump)
                        {
                            i += int.Parse(instruction.SecondArgument);
                            continue;
                        }
                        break;
                }

                i++;
            }

            for (int b = registers[REGISTER_B]; b < registers[REGISTER_C] + 1; b += 17)
            {
                bool isPrimeNumber = true;
                for (int d = 2; d < (int)Math.Sqrt(b); d++)
                {
                    if (b % d == 0)
                    {
                        isPrimeNumber = false;
                        break;
                    }
                }

                if (!isPrimeNumber)
                {
                    registers[REGISTER_H] += 1;
                }
            }

            return registers[REGISTER_H];
        }

        private Dictionary<string, int> InitializeRegisters(
            Dictionary<int, Instruction> instructions,
            int registerAStartValue = 0
        )
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();

            foreach (KeyValuePair<int, Instruction> instruction in instructions)
            {
                if (!int.TryParse(instruction.Value.FirstArgument, out _))
                {
                    registers[instruction.Value.FirstArgument] = 0;
                }

                if (instruction.Value.SecondArgument != null && !int.TryParse(instruction.Value.SecondArgument, out _))
                {
                    registers[instruction.Value.SecondArgument] = 0;
                }
            }

            registers[REGISTER_A] = registerAStartValue;

            return registers;
        }
    }
}
