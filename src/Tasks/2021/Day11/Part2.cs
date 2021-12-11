/*
--- Part Two ---

It seems like the individual flashes aren't bright enough to navigate. However,
you might have a better option: the flashes seem to be synchronizing!

In the example above, the first time all octopuses flash simultaneously is step
195:

After step 193:
5877777777
8877777777
7777777777
7777777777
7777777777
7777777777
7777777777
7777777777
7777777777
7777777777

After step 194:
6988888888
9988888888
8888888888
8888888888
8888888888
8888888888
8888888888
8888888888
8888888888
8888888888

After step 195:
0000000000
0000000000
0000000000
0000000000
0000000000
0000000000
0000000000
0000000000
0000000000
0000000000

If you can calculate the exact moments when the octopuses will all flash
simultaneously, you should be able to navigate through the cavern. What is the
first step during which all octopuses flash?
*/

namespace App.Tasks.Year2021.Day11
{
    public class Part2 : ITask<int>
    {
        private readonly OctopusesEnergyLevelsRepository octopusesEnergyLevelsRepository;

        private readonly OctopusesFlashes octopusesFlashes;

        public Part2()
        {
            octopusesEnergyLevelsRepository = new OctopusesEnergyLevelsRepository();
            octopusesFlashes = new OctopusesFlashes();
        }

        public int Solution(string input)
        {
            int[,] octopusesEnergyLevels = octopusesEnergyLevelsRepository.GetOctopusesEnergyLevels(input);
            int firstStepDuringWhichAllOctopusesFlash =
                octopusesFlashes.FindFirstStepDuringWhichAllOctopusesFlash(octopusesEnergyLevels);

            return firstStepDuringWhichAllOctopusesFlash;
        }
    }
}
