using System.Collections.Generic;

namespace App.Tasks.Year2016.Day12
{
    public class Registers
    {
        private readonly Dictionary<string, int> registers = new Dictionary<string, int>
        {
            { "a", 0 },
            { "b", 0 },
            { "c", 0 },
            { "d", 0 }
        };

        public int GetRegisterAValue(Dictionary<int, Instruction> instructions)
        {
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
                        if (jump)
                        {
                            i += int.Parse(instruction.SecondArgument);
                            // When jumping iterator is not increased
                            i--;
                        }
                        break;
                }

                i++;
            }

            return registers["a"];
        }

        public int GetRegisterAValueWhenRegisterCIsInitializedToOne(Dictionary<int, Instruction> instructions)
        {
            registers["c"] = 1;
            return GetRegisterAValue(instructions);
        }
    }
}
