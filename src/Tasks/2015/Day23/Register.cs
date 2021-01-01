using System.Collections.Generic;

namespace App.Tasks.Year2015.Day23
{
    public class Register
    {
        private const char REGISTER_A = 'a';

        private const char REGISTER_B = 'b';

        private const int REGISTER_B_STARTS_AT = 0;

        public int GetRegisterBValue(Dictionary<int, Instruction> instructions, int registerAStartsAt)
        {
            Dictionary<char, int> registers = new Dictionary<char, int>
            {
                { REGISTER_A, registerAStartsAt },
                { REGISTER_B, REGISTER_B_STARTS_AT }
            };

            int i = 0;
            while (i < instructions.Count)
            {
                Instruction instruction = instructions[i];

                switch (instruction.InstructionType)
                {
                    case InstructionType.HalfCurrentValue:
                        registers[instruction.Register.Value] /= 2;
                        break;
                    case InstructionType.TripleCurrentValue:
                        registers[instruction.Register.Value] *= 3;
                        break;
                    case InstructionType.Increment:
                        registers[instruction.Register.Value] += 1;
                        break;
                    case InstructionType.JumpOffset:
                        i += instruction.Offset.Value;
                        continue;
                    case InstructionType.JumpOffsetIfEven:
                        if (registers[instruction.Register.Value] % 2 == 0)
                        {
                            i += instruction.Offset.Value;
                            continue;
                        }
                        break;
                    case InstructionType.JumpOffsetIfOne:
                        if (registers[instruction.Register.Value] == 1)
                        {
                            i += instruction.Offset.Value;
                            continue;
                        }
                        break;
                }

                i++;
            }

            return registers[REGISTER_B];
        }
    }
}
