using System;
using App.Tasks.Year2020.Day7;
using Xunit;

namespace Tests.Tasks.Year2020.Day7
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_FirstBagsRulesExample_BagsRequiredInsideSingleShinyGoldBagEquals()
        {
            string bagsRules = "light red bags contain 1 bright white bag, 2 muted yellow bags."
                + $"{Environment.NewLine}dark orange bags contain 3 bright white bags, 4 muted yellow bags."
                + $"{Environment.NewLine}bright white bags contain 1 shiny gold bag."
                + $"{Environment.NewLine}muted yellow bags contain 2 shiny gold bags, 9 faded blue bags."
                + $"{Environment.NewLine}shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags."
                + $"{Environment.NewLine}dark olive bags contain 3 faded blue bags, 4 dotted black bags."
                + $"{Environment.NewLine}vibrant plum bags contain 5 faded blue bags, 6 dotted black bags."
                + $"{Environment.NewLine}faded blue bags contain no other bags."
                + $"{Environment.NewLine}dotted black bags contain no other bags.";

            Assert.Equal(32, task.Solution(bagsRules));
        }

        [Fact]
        public void Solution_SecondBagsRulesExample_BagsRequiredInsideSingleShinyGoldBagEquals()
        {
            string bagsRules = "shiny gold bags contain 2 dark red bags."
                + $"{Environment.NewLine}dark red bags contain 2 dark orange bags."
                + $"{Environment.NewLine}dark orange bags contain 2 dark yellow bags."
                + $"{Environment.NewLine}dark yellow bags contain 2 dark green bags."
                + $"{Environment.NewLine}dark green bags contain 2 dark blue bags."
                + $"{Environment.NewLine}dark blue bags contain 2 dark violet bags."
                + $"{Environment.NewLine}dark violet bags contain no other bags.";

            Assert.Equal(126, task.Solution(bagsRules));
        }
    }
}
