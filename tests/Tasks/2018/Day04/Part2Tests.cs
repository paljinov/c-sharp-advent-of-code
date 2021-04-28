using System;
using App.Tasks.Year2018.Day4;
using Xunit;

namespace Tests.Tasks.Year2018.Day4
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_GuardsRecordsExample_GuardIdAndAsleepMinuteProductForSecondStrategyEquals()
        {
            string guardsRecords = "[1518-11-01 00:00] Guard #10 begins shift"
                + $"{Environment.NewLine}[1518-11-01 00:05] falls asleep"
                + $"{Environment.NewLine}[1518-11-01 00:25] wakes up"
                + $"{Environment.NewLine}[1518-11-01 00:30] falls asleep"
                + $"{Environment.NewLine}[1518-11-01 00:55] wakes up"
                + $"{Environment.NewLine}[1518-11-01 23:58] Guard #99 begins shift"
                + $"{Environment.NewLine}[1518-11-02 00:40] falls asleep"
                + $"{Environment.NewLine}[1518-11-02 00:50] wakes up"
                + $"{Environment.NewLine}[1518-11-03 00:05] Guard #10 begins shift"
                + $"{Environment.NewLine}[1518-11-03 00:24] falls asleep"
                + $"{Environment.NewLine}[1518-11-03 00:29] wakes up"
                + $"{Environment.NewLine}[1518-11-04 00:02] Guard #99 begins shift"
                + $"{Environment.NewLine}[1518-11-04 00:36] falls asleep"
                + $"{Environment.NewLine}[1518-11-04 00:46] wakes up"
                + $"{Environment.NewLine}[1518-11-05 00:03] Guard #99 begins shift"
                + $"{Environment.NewLine}[1518-11-05 00:45] falls asleep"
                + $"{Environment.NewLine}[1518-11-05 00:55] wakes up";

            Assert.Equal(4455, task.Solution(guardsRecords));
        }
    }
}
