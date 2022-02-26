using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day21
{
    public class Program
    {
        private const int INSTRUCTION_POINTER_START = 0;
        private const int MOVE_INCREMENT = 1;
        private const int REGISTER_ZERO = 0;
        private const int REGISTER_FIVE = 5;
        private const int TOTAL_REGISTERS = 6;
        private const int VALUES_SEEN_TIMES = 5;


        public int CalculateLowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithFewestInstructionsExecuted(
            int instructionPointer,
            Instruction[] instructions
        )
        {
            int lowestNonNegativeRegisterZeroValue = int.MaxValue;
            int fewestInstructionsExecuted = int.MaxValue;

            int registerZeroStartValue = 0;
            while (lowestNonNegativeRegisterZeroValue == int.MaxValue)
            {
                int instructionsExecuted = CalculateInstructionsExecutedWhenTheBackgroundProcessHalts(
                    instructionPointer, instructions, fewestInstructionsExecuted, registerZeroStartValue);

                if (instructionsExecuted < fewestInstructionsExecuted)
                {
                    fewestInstructionsExecuted = instructionsExecuted;
                    lowestNonNegativeRegisterZeroValue = registerZeroStartValue;
                    break;
                }

                registerZeroStartValue++;
            }

            return lowestNonNegativeRegisterZeroValue;
        }


        public int CalculateLowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithMostInstructionsExecuted(
            int instructionPointer,
            Instruction[] instructions
        )
        {
            return 0;
        }

        public int CalculateInstructionsExecutedWhenTheBackgroundProcessHalts(
            int instructionPointer,
            Instruction[] instructions,
            int fewestInstructionsExecuted,
            int registerZeroStartValue
        )
        {
            Dictionary<int, int> valueSeenTimes = new Dictionary<int, int>();

            int[] registers = new int[TOTAL_REGISTERS];
            registers[REGISTER_ZERO] = registerZeroStartValue;

            // Instruction pointer is bound to a register
            int boundRegister = instructionPointer;
            // The instruction pointer starts at 0
            instructionPointer = INSTRUCTION_POINTER_START;

            int instructionsCount = 0;
            // If the instruction pointer ever causes the device to attempt to load an instruction outside
            // the instructions defined in the program, the program instead immediately halts
            while (instructionPointer < instructions.Length && instructionsCount < fewestInstructionsExecuted)
            {
                // Program indirectly access the instruction pointer itself
                Instruction instruction = instructions[instructionPointer];
                // Instruction pointer value is written to bound register just before each instruction is execute
                registers[boundRegister] = instructionPointer;

                switch (instruction.InstructionType)
                {
                    case InstructionType.AddRegister:
                        AddRegister(registers, instruction);
                        break;
                    case InstructionType.AddImmediate:
                        AddImmediate(registers, instruction);
                        break;
                    case InstructionType.MultiplyRegister:
                        MultiplyRegister(registers, instruction);
                        break;
                    case InstructionType.MultiplyImmediate:
                        MultiplyImmediate(registers, instruction);
                        break;
                    case InstructionType.BitwiseAndRegister:
                        BitwiseAndRegister(registers, instruction);
                        break;
                    case InstructionType.BitwiseAndImmediate:
                        BitwiseAndImmediate(registers, instruction);
                        break;
                    case InstructionType.BitwiseOrRegister:
                        BitwiseOrRegister(registers, instruction);
                        break;
                    case InstructionType.BitwiseOrImmediate:
                        BitwiseOrImmediate(registers, instruction);
                        break;
                    case InstructionType.SetRegister:
                        SetRegister(registers, instruction);
                        break;
                    case InstructionType.SetImmediate:
                        SetImmediate(registers, instruction);
                        break;
                    case InstructionType.GreaterThanImmediateRegister:
                        GreaterThanImmediateRegister(registers, instruction);
                        break;
                    case InstructionType.GreaterThanRegisterImmediate:
                        GreaterThanRegisterImmediate(registers, instruction);
                        break;
                    case InstructionType.GreaterThanRegisterRegister:
                        GreaterThanRegisterRegister(registers, instruction);
                        break;
                    case InstructionType.EqualImmediateRegister:
                        EqualImmediateRegister(registers, instruction);
                        break;
                    case InstructionType.EqualRegisterImmediate:
                        EqualRegisterImmediate(registers, instruction);
                        break;
                    case InstructionType.EqualRegisterRegister:
                        EqualRegisterRegister(registers, instruction);
                        break;
                }

                // The value of bound register is written back to the instruction pointer
                // immediately after each instruction finishes execution
                instructionPointer = registers[boundRegister];
                // Move to the next instruction by adding one to the instruction pointer
                instructionPointer += MOVE_INCREMENT;

                instructionsCount++;

                // If register five is modified
                if (instruction.OutputC == REGISTER_FIVE)
                {
                    // Check for cycle
                    if (valueSeenTimes.ContainsKey(registers[instruction.OutputC]))
                    {
                        if (valueSeenTimes[registers[instruction.OutputC]] < VALUES_SEEN_TIMES)
                        {
                            valueSeenTimes[registers[instruction.OutputC]]++;
                        }
                        else
                        {
                            return fewestInstructionsExecuted;
                        }
                    }
                    // If value is not seen yet
                    else
                    {
                        valueSeenTimes[registers[instruction.OutputC]] = 1;
                    }
                }
            }

            return instructionsCount;
        }

        private void AddRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] + registers[instruction.InputB];
        }

        private void AddImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] + instruction.InputB;
        }

        private void MultiplyRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] * registers[instruction.InputB];
        }

        private void MultiplyImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] * instruction.InputB;
        }

        private void BitwiseAndRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] & registers[instruction.InputB];
        }

        private void BitwiseAndImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] & instruction.InputB;
        }

        private void BitwiseOrRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] | registers[instruction.InputB];
        }

        private void BitwiseOrImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] | instruction.InputB;
        }

        private void SetRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA];
        }

        private void SetImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = instruction.InputA;
        }

        private void GreaterThanImmediateRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = instruction.InputA > registers[instruction.InputB] ? 1 : 0;
        }

        private void GreaterThanRegisterImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] > instruction.InputB ? 1 : 0;
        }

        private void GreaterThanRegisterRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] > registers[instruction.InputB] ? 1 : 0;
        }

        private void EqualImmediateRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = instruction.InputA == registers[instruction.InputB] ? 1 : 0;
        }

        private void EqualRegisterImmediate(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] == instruction.InputB ? 1 : 0;
        }

        private void EqualRegisterRegister(int[] registers, Instruction instruction)
        {
            registers[instruction.OutputC] = registers[instruction.InputA] == registers[instruction.InputB] ? 1 : 0;
        }
    }
}
