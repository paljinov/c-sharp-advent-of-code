/*
--- Part Two ---

The unknown benefactor is very thankful for releasi-- er, helping little Jane
Marie with her computer. Definitely not to distract you, what is the value in
register b after the program is finished executing if register a starts as 1
instead?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day23
{
    public class Part2 : ITask<int>
    {
        private const int REGISTER_A_STARTS_AT = 1;

        private readonly InstructionsRepository instructionsRepository;

        private readonly Register register;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            register = new Register();
        }

        public int Solution(string input)
        {
            Dictionary<int, Instruction> instructions = instructionsRepository.GetInstructions(input);

            int registerBValue = register.GetRegisterBValue(instructions, REGISTER_A_STARTS_AT);

            return registerBValue;
        }
    }
}
