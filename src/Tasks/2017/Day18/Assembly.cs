using System.Collections.Generic;

namespace App.Tasks.Year2017.Day18
{
    public class Assembly
    {
        private const string REGISTER_P = "p";

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

        public int CountProgramOneTotalSentValuesWhenAssemblyCodeIsRanTwice(Dictionary<int, Instruction> instructions)
        {
            int program1SentValues = 0;

            int program0CurrentInstruction = 0;
            int program1CurrentInstruction = 0;
            Dictionary<string, long> program0Registers = InitializeRegisters(instructions, 0);
            Dictionary<string, long> program1Registers = InitializeRegisters(instructions, 1);
            Queue<int> program0Values = new Queue<int>();
            Queue<int> program1Values = new Queue<int>();

            bool? continueProgram1 = null;

            while (continueProgram1 == null || program0Values.Count > 0 || program1Values.Count > 0)
            {
                if (continueProgram1.HasValue == false || continueProgram1.Value == true)
                {
                    DoProgramsExhangeValues(
                        instructions,
                        ref program0CurrentInstruction,
                        program0Registers,
                        program1Values,
                        program0Values
                    );
                    continueProgram1 = false;
                }
                else
                {
                    DoProgramsExhangeValues(
                        instructions,
                        ref program1CurrentInstruction,
                        program1Registers,
                        program0Values,
                        program1Values
                    );
                    continueProgram1 = true;

                    program1SentValues += program0Values.Count;
                }
            }

            return program1SentValues;
        }

        private void DoProgramsExhangeValues(
            Dictionary<int, Instruction> instructions,
            ref int i,
            Dictionary<string, long> registers,
            Queue<int> send,
            Queue<int> receive
        )
        {
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
                        if (registers.ContainsKey(instruction.FirstArgument))
                        {
                            send.Enqueue((int)registers[instruction.FirstArgument]);
                        }
                        else
                        {
                            send.Enqueue(int.Parse(instruction.FirstArgument));
                        }
                        break;
                    case InstructionType.Recover:
                        if (receive.Count == 0)
                        {
                            return;
                        }

                        registers[instruction.FirstArgument] = receive.Dequeue();
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
                            if (registers.ContainsKey(instruction.SecondArgument))
                            {
                                i += (int)registers[instruction.SecondArgument];
                            }
                            else
                            {
                                i += int.Parse(instruction.SecondArgument);
                            }

                            continue;
                        }
                        break;
                }

                i++;
            }
        }

        private Dictionary<string, long> InitializeRegisters(
            Dictionary<int, Instruction> instructions,
            int registerP = 0
        )
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

            registers[REGISTER_P] = registerP;

            return registers;
        }
    }
}
