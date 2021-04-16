using System;
using App.Tasks.Year2017.Day20;
using Xunit;

namespace Tests.Tasks.Year2017.Day20
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_ParticlesExample_LeftParticlesCountAfterAllCollisionsAreResolvedEquals()
        {
            string particles = "p=<-6,0,0>, v=<3,0,0>, a=<0,0,0>"
                + $"{Environment.NewLine}p=<-4,0,0>, v=<2,0,0>, a=<0,0,0>"
                + $"{Environment.NewLine}p=<-2,0,0>, v=<1,0,0>, a=<0,0,0>"
                + $"{Environment.NewLine}p=<3,0,0>, v=<-1,0,0>, a=<0,0,0>";

            Assert.Equal(1, task.Solution(particles));
        }
    }
}
