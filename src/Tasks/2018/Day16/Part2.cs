/*
--- Part Two ---

Using the samples you collected, work out the number of each opcode and execute
the test program (the second section of your puzzle input).

What value is contained in register 0 after executing the test program?
*/

namespace App.Tasks.Year2018.Day16
{
    public class Part2 : ITask<int>
    {
        private readonly SamplesAndTestProgramRepository samplesAndTestProgramRepository;

        private readonly Device device;

        public Part2()
        {
            samplesAndTestProgramRepository = new SamplesAndTestProgramRepository();
            device = new Device();
        }

        public int Solution(string input)
        {
            Sample[] samples = samplesAndTestProgramRepository.GetSamples(input);
            int[][] testProgram = samplesAndTestProgramRepository.GetTestProgram(input);

            int registerZeroValue = device.CalculateRegisterZeroValueAfterExecutingTestProgram(samples, testProgram);

            return registerZeroValue;
        }
    }
}
