using System;
using App.Tasks.Year2015.Day16;
using Xunit;

namespace Tests.Tasks.Year2015.Day16
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_SuesExample_AuntSueNumberEquals()
        {
            string sues = "Sue 1: trees: 2, cars: 3, vizslas: 8"
                + $"{Environment.NewLine}Sue 2: trees: 10, children: 9, cats: 1"
                + $"{Environment.NewLine}Sue 3: pomeranians: 3, perfumes: 1, vizslas: 0"
                + $"{Environment.NewLine}Sue 4: vizslas: 0, perfumes: 6, trees: 0"
                + $"{Environment.NewLine}Sue 5: vizslas: 7, pomeranians: 1, akitas: 10";

            Assert.Equal(3, task.Solution(sues));
        }
    }
}
