using System;
using App.Tasks.Year2022.Day3;
using Xunit;

namespace Tests.Tasks.Year2022.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_RucksacksItemsExample_PrioritiesSumForItemsThatCorrespondToTheBadgesOfEachThreeElfGroupEquals()
        {
            string prioritiesSum = "vJrwpWtwJgWrhcsFMMfFFhFp"
                + $"{Environment.NewLine}jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL"
                + $"{Environment.NewLine}PmmdzqPrVvPwwTWBwg"
                + $"{Environment.NewLine}wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn"
                + $"{Environment.NewLine}ttgJtRGJQctTZtZT"
                + $"{Environment.NewLine}CrZsJsPPZsGzwwsLwLmpwMDw";

            Assert.Equal(70, task.Solution(prioritiesSum));
        }
    }
}
