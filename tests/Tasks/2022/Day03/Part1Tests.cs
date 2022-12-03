using System;
using App.Tasks.Year2022.Day3;
using Xunit;

namespace Tests.Tasks.Year2022.Day3
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_RucksacksItemsExample_PrioritiesSumForItemsThatAppearInBothCompartmentsEquals()
        {
            string prioritiesSum = "vJrwpWtwJgWrhcsFMMfFFhFp"
                + $"{Environment.NewLine}jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"
                + $"{Environment.NewLine}PmmdzqPrVvPwwTWBwg"
                + $"{Environment.NewLine}wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"
                + $"{Environment.NewLine}ttgJtRGJQctTZtZT"
                + $"{Environment.NewLine}CrZsJsPPZsGzwwsLwLmpwMDw";

            Assert.Equal(157, task.Solution(prioritiesSum));
        }
    }
}
