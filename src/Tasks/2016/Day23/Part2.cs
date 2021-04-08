/*
--- Part Two ---

The safe doesn't open, but it does make several angry noises to express its
frustration.

You're quite sure your logic is working correctly, so the only other thing is...
you check the painting again. As it turns out, colored eggs are still eggs. Now
you count 12.

As you run the program with this new input, the prototype computer begins to
overheat. You wonder what's taking so long, and whether the lack of any
instruction more powerful than "add one" has anything to do with it. Don't
bunnies usually multiply?

Anyway, what value should actually be sent to the safe?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2016.Day23
{
    public class Part2 : ITask<int>
    {
        private const int REGISTER_A_START_VALUE = 12;

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
            int registerAValue = registers.GetRegisterAValue(instructions, REGISTER_A_START_VALUE);

            return registerAValue;
        }
    }
}
