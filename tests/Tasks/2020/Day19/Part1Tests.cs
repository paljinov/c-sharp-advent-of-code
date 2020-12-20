using System;
using App.Tasks.Year2020.Day19;
using Xunit;

namespace Tests.Tasks.Year2020.Day19
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_RulesAndReceivedMessagesExample_ReceivedMessagesThatFollowRulesEquals()
        {
            string rulesAndReceivedMessages = "0: 4 1 5"
                + $"{Environment.NewLine}1: 2 3 | 3 2"
                + $"{Environment.NewLine}2: 4 4 | 5 5"
                + $"{Environment.NewLine}3: 4 5 | 5 4"
                + $"{Environment.NewLine}4: \"a\""
                + $"{Environment.NewLine}5: \"b\""
                + Environment.NewLine
                + $"{Environment.NewLine}ababbb"
                + $"{Environment.NewLine}bababa"
                + $"{Environment.NewLine}abbbab"
                + $"{Environment.NewLine}aaabbb"
                + $"{Environment.NewLine}aaaabbb";

            Assert.Equal(2, task.Solution(rulesAndReceivedMessages));
        }
    }
}
