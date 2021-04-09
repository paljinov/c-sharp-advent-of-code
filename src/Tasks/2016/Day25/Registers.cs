using System.Collections.Generic;

namespace App.Tasks.Year2016.Day25
{
    public class Registers
    {
        // After repetitions limit is hit it is considered that clock signal repeats forever
        private const int CLOCK_SIGNAL_REPETITIONS_LIMIT = 1000;

        public int CalculateLowestRegisterAInitialPositiveIntegerThatCausesClockSignal(
            Dictionary<int, Instruction> instructions
        )
        {
            int registerAStartValue = 0;
            bool found = false;
            while (!found)
            {
                registerAStartValue++;
                found = GetRegisterAValue(instructions, registerAStartValue);
            }

            return registerAStartValue;
        }

        private bool GetRegisterAValue(Dictionary<int, Instruction> instructions, int registerAStartValue)
        {
            Dictionary<string, int> registers = InitializeRegisters(registerAStartValue);
            int nextClockSignal = 0;
            int clockSignalRepetitions = 0;

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
                            continue;
                        }
                        break;
                    case InstructionType.Transmit:
                        int actualClockSignal;
                        if (registers.ContainsKey(instruction.FirstArgument))
                        {
                            actualClockSignal = registers[instruction.FirstArgument];
                        }
                        else
                        {
                            actualClockSignal = int.Parse(instruction.FirstArgument);
                        }

                        if (nextClockSignal != actualClockSignal)
                        {
                            return false;
                        }

                        clockSignalRepetitions++;
                        nextClockSignal = nextClockSignal == 0 ? 1 : 0;

                        // If clock signal repeats forever
                        if (clockSignalRepetitions >= CLOCK_SIGNAL_REPETITIONS_LIMIT)
                        {
                            return true;
                        }
                        break;
                }

                i++;
            }

            return true;
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
