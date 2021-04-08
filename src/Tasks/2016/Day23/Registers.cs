using System.Collections.Generic;

namespace App.Tasks.Year2016.Day23
{
    public class Registers
    {
        public int GetRegisterAValue(Dictionary<int, Instruction> instructions, int registerAStartValue)
        {
            Dictionary<string, int> registers = InitializeRegisters(registerAStartValue);

            int i = 0;
            while (i < instructions.Count)
            {
                Instruction instruction = instructions[i];
                switch (instruction.InstructionType)
                {
                    case InstructionType.Copy:
                        if (registers.ContainsKey(instruction.FirstArgument))
                        {
                            registers[instruction.SecondArgument] = registers[instruction.FirstArgument];
                        }
                        else
                        {
                            registers[instruction.SecondArgument] = int.Parse(instruction.FirstArgument);
                        }
                        break;
                    case InstructionType.Increase:
                        registers[instruction.FirstArgument]++;
                        break;
                    case InstructionType.Decrease:
                        registers[instruction.FirstArgument]--;
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

                        // If first argument is not zero
                        if (jump)
                        {
                            if (registers.ContainsKey(instruction.SecondArgument))
                            {
                                i += registers[instruction.SecondArgument];
                            }
                            else
                            {
                                i += int.Parse(instruction.SecondArgument);
                            }

                            continue;
                        }
                        break;
                    case InstructionType.Toggle:
                        int nextInstruction = i + registers[instruction.FirstArgument];
                        // If an attempt is made to toggle an instruction outside the program, nothing happens
                        if (nextInstruction < instructions.Count)
                        {
                            // One-argument instructions
                            if (string.IsNullOrEmpty(instructions[nextInstruction].SecondArgument))
                            {
                                // Increase becomes decrease
                                if (instructions[nextInstruction].InstructionType == InstructionType.Increase)
                                {
                                    instructions[nextInstruction].InstructionType = InstructionType.Decrease;
                                }
                                // All other one-argument instructions become increase
                                else
                                {
                                    instructions[nextInstruction].InstructionType = InstructionType.Increase;
                                }
                            }
                            // Two-argument instructions
                            else
                            {
                                // Jump becomes copy
                                if (instructions[nextInstruction].InstructionType == InstructionType.Jump)
                                {
                                    instructions[nextInstruction].InstructionType = InstructionType.Copy;
                                    // If toggling produces an invalid instruction skip it
                                    if (int.TryParse(instructions[nextInstruction].SecondArgument, out _))
                                    {
                                        instructions.Remove(nextInstruction);
                                    }
                                }
                                // All other two-instructions become jump
                                else
                                {
                                    instructions[nextInstruction].InstructionType = InstructionType.Jump;
                                }
                            }
                        }
                        break;
                }

                i++;
            }

            return registers["a"];
        }

        private Dictionary<string, int> InitializeRegisters(int registerAStartValue)
        {
            return new Dictionary<string, int>
            {
                { "a", registerAStartValue },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 }
            };
        }
    }
}
