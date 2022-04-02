using System;
using App.Tasks.Year2021.Day23;
using Xunit;

namespace Tests.Tasks.Year2021.Day23
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_AmphipodsBurrowExample_LeastEnergyRequiredToOrganizeTheAmphipodsEquals()
        {
            string amphipodsBurrow = "#############"
                + $"{Environment.NewLine}#...........#"
                + $"{Environment.NewLine}###B#C#B#D###"
                + $"{Environment.NewLine}  #D#C#B#A#  "
                + $"{Environment.NewLine}  #D#B#A#C#  "
                + $"{Environment.NewLine}  #A#D#C#A#  "
                + $"{Environment.NewLine}  #########  ";

            Assert.Equal(44169, task.Solution(amphipodsBurrow));
        }
    }
}
