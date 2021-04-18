/*
--- Part Two ---

Now, it's time to fix the problem.

The debug mode switch is wired directly to register a. You flip the switch,
which makes register a now start at 1 when the program is executed.

Immediately, the coprocessor begins to overheat. Whoever wrote this program
obviously didn't choose a very efficient implementation. You'll need to optimize
the program if it has any hope of completing before Santa needs that printer
working.

The coprocessor's ultimate goal is to determine the final value left in register
h once the program completes. Technically, if it had that... it wouldn't even
need to run the program.

After setting register a to 1, if the program were to run to completion, what
value would be left in register h?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day23
{
    public class Part2 : ITask<int>
    {
        private const int REGISTER_A_START_VALUE = 1;

        private readonly InstructionsRepository instructionsRepository;

        private readonly Coprocessor coprocessor;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            coprocessor = new Coprocessor();
        }

        public int Solution(string input)
        {
            Dictionary<int, Instruction> instructions = instructionsRepository.GetInstructions(input);
            int registerHValue =
                coprocessor.CalculateRegisterHValueAfterProgramCompletion(instructions, REGISTER_A_START_VALUE);

            return registerHValue;
        }
    }
}
