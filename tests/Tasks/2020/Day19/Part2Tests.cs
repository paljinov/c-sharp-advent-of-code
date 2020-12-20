using System;
using App.Tasks.Year2020.Day19;
using Xunit;

namespace Tests.Tasks.Year2020.Day19
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_RulesAndReceivedMessagesExample_ReceivedMessagesThatFollowRulesEquals()
        {
            string rulesAndReceivedMessages = "42: 9 14 | 10 1"
                + $"{Environment.NewLine}9: 14 27 | 1 26"
                + $"{Environment.NewLine}10: 23 14 | 28 1"
                + $"{Environment.NewLine}1: \"a\""
                + $"{Environment.NewLine}11: 42 31"
                + $"{Environment.NewLine}5: 1 14 | 15 1"
                + $"{Environment.NewLine}19: 14 1 | 14 14"
                + $"{Environment.NewLine}12: 24 14 | 19 1"
                + $"{Environment.NewLine}16: 15 1 | 14 14"
                + $"{Environment.NewLine}31: 14 17 | 1 13"
                + $"{Environment.NewLine}6: 14 14 | 1 14"
                + $"{Environment.NewLine}2: 1 24 | 14 4"
                + $"{Environment.NewLine}0: 8 11"
                + $"{Environment.NewLine}13: 14 3 | 1 12"
                + $"{Environment.NewLine}15: 1 | 14"
                + $"{Environment.NewLine}17: 14 2 | 1 7"
                + $"{Environment.NewLine}23: 25 1 | 22 14"
                + $"{Environment.NewLine}28: 16 1"
                + $"{Environment.NewLine}4: 1 1"
                + $"{Environment.NewLine}20: 14 14 | 1 15"
                + $"{Environment.NewLine}3: 5 14 | 16 1"
                + $"{Environment.NewLine}27: 1 6 | 14 18"
                + $"{Environment.NewLine}14: \"b\""
                + $"{Environment.NewLine}21: 14 1 | 1 14"
                + $"{Environment.NewLine}25: 1 1 | 1 14"
                + $"{Environment.NewLine}22: 14 14"
                + $"{Environment.NewLine}8: 42"
                + $"{Environment.NewLine}26: 14 22 | 1 20"
                + $"{Environment.NewLine}18: 15 15"
                + $"{Environment.NewLine}7: 14 5 | 1 21"
                + $"{Environment.NewLine}24: 14 1"
                + Environment.NewLine
                + $"{Environment.NewLine}abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa"
                + $"{Environment.NewLine}bbabbbbaabaabba"
                + $"{Environment.NewLine}babbbbaabbbbbabbbbbbaabaaabaaa"
                + $"{Environment.NewLine}aaabbbbbbaaaabaababaabababbabaaabbababababaaa"
                + $"{Environment.NewLine}bbbbbbbaaaabbbbaaabbabaaa"
                + $"{Environment.NewLine}bbbababbbbaaaaaaaabbababaaababaabab"
                + $"{Environment.NewLine}ababaaaaaabaaab"
                + $"{Environment.NewLine}ababaaaaabbbaba"
                + $"{Environment.NewLine}baabbaaaabbaaaababbaababb"
                + $"{Environment.NewLine}abbbbabbbbaaaababbbbbbaaaababb"
                + $"{Environment.NewLine}aaaaabbaabaaaaababaa"
                + $"{Environment.NewLine}aaaabbaaaabbaaa"
                + $"{Environment.NewLine}aaaabbaabbaaaaaaabbbabbbaaabbaabaaa"
                + $"{Environment.NewLine}babaaabbbaaabaababbaabababaaab"
                + $"{Environment.NewLine}aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba";

            Assert.Equal(12, task.Solution(rulesAndReceivedMessages));
        }
    }
}
