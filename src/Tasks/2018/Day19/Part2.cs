/*
--- Part Two ---

A new background process immediately spins up in its place. It appears
identical, but on closer inspection, you notice that this time, register 0
started with the value 1.

What value is left in register 0 when this new background process halts?
*/

namespace App.Tasks.Year2018.Day19
{
    public class Part2 : ITask<int>
    {
        private const int REGISTER_ZERO_START_VALUE = 1;

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

            int registerZeroValueWhenTheBackgroundProcessHalts =
                program.CalculateRegisterZeroValueWhenTheBackgroundProcessHalts(
                    instructionPointer, instructions, REGISTER_ZERO_START_VALUE);

            return registerZeroValueWhenTheBackgroundProcessHalts;
        }
    }
}
