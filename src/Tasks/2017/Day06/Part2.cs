/*
--- Part Two ---

Out of curiosity, the debugger would also like to know the size of the loop:
starting from a state that has already been seen, how many block redistribution
cycles must be performed before that same state is seen again?

In the example above, 2 4 1 2 is seen again after four cycles, and so the answer
in that example would be 4.

How many cycles are in the infinite loop that arises from the configuration in
your puzzle input?
*/

namespace App.Tasks.Year2017.Day6
{
    public class Part2 : ITask<int>
    {
        private readonly MemoryBanksRepository memoryBanksRepository;

        private readonly Redistribute redistribute;

        public Part2()
        {
            memoryBanksRepository = new MemoryBanksRepository();
            redistribute = new Redistribute();
        }

        public int Solution(string input)
        {
            int[] memoryBanksBlocks = memoryBanksRepository.GetBlocks(input);
            int redistributionCycles = redistribute.CountLoopSize(memoryBanksBlocks);

            return redistributionCycles;
        }
    }
}
