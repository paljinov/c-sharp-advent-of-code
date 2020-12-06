using System;
using App.Tasks.Year2020.Day6;
using Xunit;

namespace Tests.Tasks.Year2020.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PersonsYesAnswersExample_AnyoneAnsweredYesEquals()
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

            Assert.Equal(11, task.Solution(personsYesAnswers));
        }
    }
}
