using System;
using App.Tasks.Year2015.Day16;
using Xunit;

namespace Tests.Tasks.Year2015.Day16
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_SuesExample_AuntSueNumberEquals()
        {
            string sues = "Sue 1: goldfish: 3, akitas: 2, perfumes: 6"
                + $"{Environment.NewLine}Sue 2: cats: 7, trees: 0, vizslas: 1"
                + $"{Environment.NewLine}Sue 3: perfumes: 7, cars: 7, akitas: 7"
                + $"{Environment.NewLine}Sue 4: goldfish: 0, vizslas: 0, samoyeds: 2"
                + $"{Environment.NewLine}Sue 5: vizslas: 2, children: 2, cats: 3";

            Assert.Equal(4, task.Solution(sues));
        }
    }
}
