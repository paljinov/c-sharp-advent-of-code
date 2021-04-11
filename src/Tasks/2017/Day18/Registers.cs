using System.Collections.Generic;

namespace App.Tasks.Year2017.Day18
{
    public class Registers
    {
        public int FindFirstRecoveredFrequency(Dictionary<int, Instruction> instructions)
        {
            int lastPlayedSound = 0;

            Dictionary<string, long> registers = InitializeRegisters(instructions);

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
                    case InstructionType.Increase:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] += registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] += int.Parse(instruction.SecondArgument);
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
                        break;
                    case InstructionType.Remainder:
                        if (registers.ContainsKey(instruction.SecondArgument))
                        {
                            registers[instruction.FirstArgument] %= registers[instruction.SecondArgument];
                        }
                        else
                        {
                            registers[instruction.FirstArgument] %= int.Parse(instruction.SecondArgument);
                        }
                        break;
                    case InstructionType.PlaySound:
                        lastPlayedSound = (int)registers[instruction.FirstArgument];
                        break;
                    case InstructionType.Recover:
                        if (registers[instruction.FirstArgument] != 0)
                        {
                            return lastPlayedSound;
                        }
                        break;
                    case InstructionType.Jump:
                        bool jump = false;
                        if (registers.ContainsKey(instruction.FirstArgument))
                        {
                            if (registers[instruction.FirstArgument] > 0)
                            {
                                jump = true;
                            }
                        }
                        else
                        {
                            if (int.Parse(instruction.FirstArgument) > 0)
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

            return lastPlayedSound;
        }

        private Dictionary<string, long> InitializeRegisters(Dictionary<int, Instruction> instructions)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            foreach (KeyValuePair<int, Instruction> instruction in instructions)
            {
                if (!int.TryParse(instruction.Value.FirstArgument, out _))
                {
                    registers[instruction.Value.FirstArgument] = 0;
                }

                if (instruction.Value.SecondArgument != null && !long.TryParse(instruction.Value.SecondArgument, out _))
                {
                    registers[instruction.Value.SecondArgument] = 0;
                }
            }

            return registers;
        }
    }
}
