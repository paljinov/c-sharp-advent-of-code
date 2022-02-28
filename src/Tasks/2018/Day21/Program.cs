namespace App.Tasks.Year2018.Day21
{
    public class Program
    {
        private const int INSTRUCTION_POINTER_START = 0;
        private const int MOVE_INCREMENT = 1;
        private const int TOTAL_REGISTERS = 6;
        private const int REGISTER_FOUR = 4;
        private const int INSTRUCTION_ON_WHICH_REGISTER_ZERO_IS_USED = 28;

        public int CalculateLowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithFewestInstructionsExecuted(
            int instructionPointer,
            Instruction[] instructions
        )
        {
            int lowestNonNegativeRegisterZeroValue =
                DoCalculateLowestNonNegativeRegisterZeroValue(instructionPointer, instructions);

            return lowestNonNegativeRegisterZeroValue;
        }


        public int CalculateLowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithMostInstructionsExecuted(
            int instructionPointer,
            Instruction[] instructions
        )
        {
            return 0;
        }

        public int DoCalculateLowestNonNegativeRegisterZeroValue(int instructionPointer, Instruction[] instructions)
        {
            int lowestNonNegativeRegisterZeroValue = -1;

            int[] registers = new int[TOTAL_REGISTERS];

            // Instruction pointer is bound to a register
            int boundRegister = instructionPointer;
            // The instruction pointer starts at 0
            instructionPointer = INSTRUCTION_POINTER_START;

            int instructionsCount = 0;
            // If the instruction pointer ever causes the device to attempt to load an instruction outside
            // the instructions defined in the program, the program instead immediately halts
            while (lowestNonNegativeRegisterZeroValue == -1 && instructionPointer < instructions.Length)
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

                if (instructionPointer == INSTRUCTION_ON_WHICH_REGISTER_ZERO_IS_USED)
                {
                    lowestNonNegativeRegisterZeroValue = registers[REGISTER_FOUR];
                }
            }

            return lowestNonNegativeRegisterZeroValue;
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
