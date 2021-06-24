/*
--- Part Two ---

You now have a complete Intcode computer.

Finally, you can lock on to the Ceres distress signal! You just need to boost
your sensors using the BOOST program.

The program runs in sensor boost mode by providing the input instruction the
value 2. Once run, it will boost the sensors automatically, but it might take a
few seconds to complete the operation on slower hardware. In sensor boost mode,
the program will output a single value: the coordinates of the distress signal.

Run the BOOST program in sensor boost mode. What are the coordinates of the
distress signal?
*/

namespace App.Tasks.Year2019.Day9
{
    public class Part2 : ITask<long>
    {
        private const int INPUT = 2;

        private readonly IntegersRepository integersRepository;

        private readonly Program program;

        public Part2()
        {
            integersRepository = new IntegersRepository();
            program = new Program();
        }

        public long Solution(string input)
        {
            long[] integers = integersRepository.GetIntegers(input);
            long boostKeycode = program.CalculateBoostKeycode(integers, INPUT);

            return boostKeycode;
        }
    }
}
