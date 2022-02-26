/*
--- Part Two ---

In order to determine the timing window for your underflow exploit, you also
need an upper bound:

What is the lowest non-negative integer value for register 0 that causes the
program to halt after executing the most instructions? (The program must
actually halt; running forever does not count as halting.)
*/

namespace App.Tasks.Year2018.Day21
{
    public class Part2 : ITask<int>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly Program program;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            program = new Program();
        }

        public int Solution(string input)
        {
            int instructionPointer = instructionsRepository.GetInstructionPointer(input);
            Instruction[] instructions = instructionsRepository.GetInstructions(input);

            int lowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithMostInstructionsExecuted = program.
                CalculateLowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithMostInstructionsExecuted(
                    instructionPointer, instructions);

            return lowestNonNegativeRegisterZeroValueWhichCausesTheProgramToHaltWithMostInstructionsExecuted;
        }
    }
}
