/*
--- Part Two ---

To be safe, the CPU also needs to know the highest value held in any register
during this process so that it can decide how much memory to allocate to these
operations. For example, in the above instructions, the highest value ever held
was 10 (in register c after the third instruction was evaluated).
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day8
{
    public class Part2 : ITask<int>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly Compute compute;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            compute = new Compute();
        }

        public int Solution(string input)
        {
            List<Instruction> instructions = instructionsRepository.GetInstructions(input);
            int highestValueHeldInAnyRegisterDuringComputationProcess =
                compute.FindHighestValueHeldInAnyRegisterDuringComputationProcess(instructions);

            return highestValueHeldInAnyRegisterDuringComputationProcess;
        }
    }
}
