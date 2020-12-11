using System;
using App.Tasks.Year2020.Day10;
using Xunit;

namespace Tests.Tasks.Year2020.Day10
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_FirstAdaptersListExample_OneAndThreeJoltDifferencesProductEquals()
        {
            string adapters = "16"
                + $"{Environment.NewLine}10"
                + $"{Environment.NewLine}15"
                + $"{Environment.NewLine}5"
                + $"{Environment.NewLine}1"
                + $"{Environment.NewLine}11"
                + $"{Environment.NewLine}7"
                + $"{Environment.NewLine}19"
                + $"{Environment.NewLine}6"
                + $"{Environment.NewLine}12"
                + $"{Environment.NewLine}4";

            Assert.Equal(35, task.Solution(adapters));
        }

        [Fact]
        public void Solution_SecondAdaptersListExample_OneAndThreeJoltDifferencesProductEquals()
        {
            string adapters = "28"
                + $"{Environment.NewLine}33"
                + $"{Environment.NewLine}18"
                + $"{Environment.NewLine}42"
                + $"{Environment.NewLine}31"
                + $"{Environment.NewLine}14"
                + $"{Environment.NewLine}46"
                + $"{Environment.NewLine}20"
                + $"{Environment.NewLine}48"
                + $"{Environment.NewLine}47"
                + $"{Environment.NewLine}24"
                + $"{Environment.NewLine}23"
                + $"{Environment.NewLine}49"
                + $"{Environment.NewLine}45"
                + $"{Environment.NewLine}19"
                + $"{Environment.NewLine}38"
                + $"{Environment.NewLine}39"
                + $"{Environment.NewLine}11"
                + $"{Environment.NewLine}1"
                + $"{Environment.NewLine}32"
                + $"{Environment.NewLine}25"
                + $"{Environment.NewLine}35"
                + $"{Environment.NewLine}8"
                + $"{Environment.NewLine}17"
                + $"{Environment.NewLine}7"
                + $"{Environment.NewLine}9"
                + $"{Environment.NewLine}4"
                + $"{Environment.NewLine}2"
                + $"{Environment.NewLine}34"
                + $"{Environment.NewLine}10"
                + $"{Environment.NewLine}3";

            Assert.Equal(220, task.Solution(adapters));
        }
    }
}
