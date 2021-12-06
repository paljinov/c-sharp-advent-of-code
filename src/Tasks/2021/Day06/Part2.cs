/*
--- Part Two ---

Suppose the lanternfish live forever and have unlimited food and space. Would
they take over the entire ocean?

After 256 days in the example above, there would be a total of 26984457539
lanternfish!

How many lanternfish would there be after 256 days?
*/

namespace App.Tasks.Year2021.Day6
{
    public class Part2 : ITask<long>
    {
        private const int TOTAL_DAYS = 256;

        private readonly LanternfishInternalTimersRepository lanternfishInternalTimersRepository;

        private readonly LanternfishSimulation lanternfishSimulation;

        public Part2()
        {
            lanternfishInternalTimersRepository = new LanternfishInternalTimersRepository();
            lanternfishSimulation = new LanternfishSimulation();
        }

        public long Solution(string input)
        {
            int[] lanternfishInternalTimers = lanternfishInternalTimersRepository.GetLanternfishInternalTimers(input);
            long totalLanternfish =
                lanternfishSimulation.CountLanternfishAfterGivenDays(lanternfishInternalTimers, TOTAL_DAYS);

            return totalLanternfish;
        }
    }
}
