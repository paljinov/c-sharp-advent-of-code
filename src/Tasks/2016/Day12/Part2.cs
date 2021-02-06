/*
--- Part Two ---

As you head down the fire escape to the monorail, you notice it didn't start;
register c needs to be initialized to the position of the ignition key.

If you instead initialize register c to be 1, what value is now left in register
a?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2016.Day12
{
    public class Part2 : ITask<int>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly Registers registers;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            registers = new Registers();
        }

        public int Solution(string input)
        {
            Dictionary<int, Instruction> instructions = instructionsRepository.GetInstructions(input);
            int registerAValue = registers.GetRegisterAValueWhenRegisterCIsInitializedToOne(instructions);

            return registerAValue;
        }
    }
}
