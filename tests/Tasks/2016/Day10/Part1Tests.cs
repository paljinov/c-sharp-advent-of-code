using System;
using System.Reflection;
using App.Tasks.Year2016.Day10;
using Xunit;

namespace Tests.Tasks.Year2016.Day10
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            task.GetType()
                .GetField("lowerValueChip", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 2);

            task.GetType()
                .GetField("higherValueChip", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, 5);
        }

        [Fact]
        public void Solution_BotsInstructionsExample_BotNumberWhichComparesChipsTwoAndFiveEquals()
        {
            string botsInstructions = "value 5 goes to bot 2"
                + $"{Environment.NewLine}bot 2 gives low to bot 1 and high to bot 0"
                + $"{Environment.NewLine}value 3 goes to bot 1"
                + $"{Environment.NewLine}bot 1 gives low to output 1 and high to bot 0"
                + $"{Environment.NewLine}bot 0 gives low to output 2 and high to output 0"
                + $"{Environment.NewLine}value 2 goes to bot 2";

            Assert.Equal(2, task.Solution(botsInstructions));
        }
    }
}
