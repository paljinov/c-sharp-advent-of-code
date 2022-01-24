/*
--- Part Two ---

As the submarine starts booting up things like the Retro Encabulator, you
realize that maybe you don't need all these submarine features after all.

What is the smallest model number accepted by MONAD?
*/

namespace App.Tasks.Year2021.Day24
{
    public class Part2 : ITask<long>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly ArithmeticLogicUnit arithmeticLogicUnit;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            arithmeticLogicUnit = new ArithmeticLogicUnit();
        }

        public long Solution(string input)
        {
            Instruction[] instructions = instructionsRepository.GetInstructions(input);
            long smallestModelNumberAcceptedByMonad =
                arithmeticLogicUnit.CalculateSmallestModelNumberAcceptedByMonad(instructions);

            return smallestModelNumberAcceptedByMonad;
        }
    }
}
