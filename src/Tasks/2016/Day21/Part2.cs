/*
--- Part Two ---

You scrambled the password correctly, but you discover that you can't actually
modify the password file on the system. You'll need to un-scramble one of the
existing passwords by reversing the scrambling process.

What is the un-scrambled version of the scrambled password fbgdceah?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2016.Day21
{
    public class Part2 : ITask<string>
    {
        private readonly string scrambledPassword = "fbgdceah";

        private readonly OperationsRepository operationsRepository;

        private readonly Scrambler scrambler;

        public Part2()
        {
            operationsRepository = new OperationsRepository();
            scrambler = new Scrambler();
        }

        public string Solution(string input)
        {
            List<Operation> operations = operationsRepository.GetOperations(input);
            string unscrambledPassword = scrambler.UnscramblePassword(operations, scrambledPassword);

            return unscrambledPassword;
        }
    }
}
