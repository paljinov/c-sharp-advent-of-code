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

            ReindeersFlyingRace reindeersFlyingRace = new ReindeersFlyingRace();
            reindeersFlyingRace.GetType()
                .GetField("flightDurationLimit", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(reindeersFlyingRace, 1000);

            task.GetType()
                .GetField("reindeersFlyingRace", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, reindeersFlyingRace);
        }

        [Fact]
        public void Solution_ReindeersDescriptionsExample_WinningReindeerPointsAfterOneThousandSecondsEquals()
        {
            string reindeersDescriptions =
                "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds."
                + $"{Environment.NewLine}Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";

            Assert.Equal(689, task.Solution(reindeersDescriptions));
        }
    }
}
