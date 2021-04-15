using System;
using App.Tasks.Year2017.Day20;
using Xunit;

namespace Tests.Tasks.Year2017.Day20
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_ParticlesExample_ParticleWhichStaysClosestToOriginPositionInTheLongTermEquals()
        {
            string particles = "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>"
                + $"{Environment.NewLine}p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>";

            Assert.Equal(0, task.Solution(particles));
        }
    }
}
