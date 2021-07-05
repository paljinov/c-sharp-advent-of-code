/*
--- Part Two ---

The game didn't run because you didn't put in any quarters. Unfortunately, you
did not bring any quarters. Memory address 0 represents the number of quarters
that have been inserted; set it to 2 to play for free.

The arcade cabinet has a joystick that can move left and right. The software
reads the position of the joystick with input instructions:

- If the joystick is in the neutral position, provide 0.
- If the joystick is tilted to the left, provide -1.
- If the joystick is tilted to the right, provide 1.

The arcade cabinet also has a segment display capable of showing a single number
that represents the player's current score. When three output instructions
specify X=-1, Y=0, the third output instruction is not a tile; the value instead
specifies the new score to show in the segment display. For example, a sequence
of output values like -1,0,12345 would show 12345 as the player's current score.

Beat the game by breaking all the blocks. What is your score after the last
block is broken?
*/

namespace App.Tasks.Year2019.Day13
{
    public class Part2 : ITask<int>
    {
        private const int MEMORY_ADDRESS_ZERO_QUARTERS = 2;

        private readonly IntegersRepository integersRepository;

        private readonly Program program;

        public Part2()
        {
            integersRepository = new IntegersRepository();
            program = new Program();
        }

        public int Solution(string input)
        {
            long[] integers = integersRepository.GetIntegers(input);
            integers[0] = MEMORY_ADDRESS_ZERO_QUARTERS;

            int scoreAfterTheLastBlockIsBroken = program.CalculateScoreAfterTheLastBlockIsBroken(integers);

            return scoreAfterTheLastBlockIsBroken;
        }
    }
}
