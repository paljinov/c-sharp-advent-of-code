using System;
using System.Reflection;
using App.Tasks.Year2015.Day14;
using Xunit;

namespace Tests.Tasks.Year2015.Day14
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();

            ReindeersFlightData reindeersFlightData = new ReindeersFlightData();
            reindeersFlightData.GetType()
                .GetField("flightDurationLimit", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(reindeersFlightData, 1000);

            task.GetType()
                .GetField("reindeersFlightData", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, reindeersFlightData);
        }

        [Fact]
        public void Solution_ReindeersExample_WinningReindeerPointsAfterOneThousandSecondsEquals()
        {
            string reindeers =
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds."
                + $"{Environment.NewLine}Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";

            Assert.Equal(689, task.Solution(reindeers));
        }
    }
}
