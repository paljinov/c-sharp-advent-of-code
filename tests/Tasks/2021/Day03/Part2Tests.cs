using System;
using App.Tasks.Year2021.Day3;
using Xunit;

namespace Tests.Tasks.Year2021.Day3
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_DiagnosticReportExample_SubmarineLifeSupportRatingEquals()
        {
            string diagnosticReport = "00100"
                + $"{Environment.NewLine}11110"
                + $"{Environment.NewLine}10110"
                + $"{Environment.NewLine}10111"
                + $"{Environment.NewLine}10101"
                + $"{Environment.NewLine}01111"
                + $"{Environment.NewLine}00111"
                + $"{Environment.NewLine}11100"
                + $"{Environment.NewLine}10000"
                + $"{Environment.NewLine}11001"
                + $"{Environment.NewLine}00010"
                + $"{Environment.NewLine}01010";

            Assert.Equal(230, task.Solution(diagnosticReport));
        }
    }
}
