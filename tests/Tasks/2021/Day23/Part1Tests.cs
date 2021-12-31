using System;
using App.Tasks.Year2021.Day23;
using Xunit;

namespace Tests.Tasks.Year2021.Day23
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_AmphipodsBurrowExample_LeastEnergyRequiredToOrganizeTheAmphipodsEquals()
        {
            string amphipodsBurrow = "#############"
                + $"{Environment.NewLine}#...........#"
                + $"{Environment.NewLine}###B#C#B#D###"
                + $"{Environment.NewLine}  #A#D#C#A#  "
                + $"{Environment.NewLine}  #########  ";

            Assert.Equal(12521, task.Solution(amphipodsBurrow));
        }
    }
}
