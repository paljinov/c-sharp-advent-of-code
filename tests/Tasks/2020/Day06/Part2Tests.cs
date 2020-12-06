using System;
using App.Tasks.Year2020.Day6;
using Xunit;

namespace Tests.Tasks.Year2020.Day6
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PersonsYesAnswersExample_EveryoneAnsweredYesEquals()
        {
            string personsYesAnswers = "abc"
                + $"{Environment.NewLine}"
                + $"{Environment.NewLine}a"
                + $"{Environment.NewLine}b"
                + $"{Environment.NewLine}c"
                + $"{Environment.NewLine}"
                + $"{Environment.NewLine}ab"
                + $"{Environment.NewLine}ac"
                + $"{Environment.NewLine}"
                + $"{Environment.NewLine}a"
                + $"{Environment.NewLine}a"
                + $"{Environment.NewLine}a"
                + $"{Environment.NewLine}a"
                + $"{Environment.NewLine}"
                + $"{Environment.NewLine}b";

            Assert.Equal(6, task.Solution(personsYesAnswers));
        }
    }
}
