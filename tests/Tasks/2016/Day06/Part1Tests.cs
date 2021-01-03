using System;
using App.Tasks.Year2016.Day6;
using Xunit;

namespace Tests.Tasks.Year2016.Day6
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_MessagesExample_ErrorCorrectedMessageForMostCommonLetterEquals()
        {
            string messages = "eedadn"
                + $"{Environment.NewLine}drvtee"
                + $"{Environment.NewLine}eandsr"
                + $"{Environment.NewLine}raavrd"
                + $"{Environment.NewLine}atevrs"
                + $"{Environment.NewLine}tsrnev"
                + $"{Environment.NewLine}sdttsa"
                + $"{Environment.NewLine}rasrtv"
                + $"{Environment.NewLine}nssdts"
                + $"{Environment.NewLine}ntnada"
                + $"{Environment.NewLine}svetve"
                + $"{Environment.NewLine}tesnvt"
                + $"{Environment.NewLine}vntsnd"
                + $"{Environment.NewLine}vrdear"
                + $"{Environment.NewLine}dvrsen"
                + $"{Environment.NewLine}enarar";

            Assert.Equal("easter", task.Solution(messages));
        }
    }
}
