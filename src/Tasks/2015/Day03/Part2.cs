/*
--- Part Two ---

The next year, to speed up the process, Santa creates a robot version of
himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the
same starting house), then take turns moving based on instructions from the elf,
who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

- ^v delivers presents to 3 houses, because Santa goes north, and then
  Robo-Santa goes south.
- ^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back
  where they started.
- ^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction
  and Robo-Santa going the other.
*/

namespace App.Tasks.Year2015.Day3
{
    public class Part2 : ITask<int>
    {
        private readonly InstructionsRepository instructionsRepository;

        private readonly Houses houses;

        public Part2()
        {
            instructionsRepository = new InstructionsRepository();
            houses = new Houses();
        }
        public int Solution(string input)
        {
            CardinalDirection[] instructions = instructionsRepository.GetInstructions(input);

            int housesThatReceiveAtLeastOnePresent =
                houses.CountHousesThatReceiveAtLeastOnePresentWithRoboSanta(instructions);

            return housesThatReceiveAtLeastOnePresent;
        }
    }
}
