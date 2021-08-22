using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2019.Day14;
using Xunit;

namespace Tests.Tasks.Year2019.Day14
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Theory]
        [ClassData(typeof(Reactions_MaximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre_TestData))]
        public void Solution_ReactionsExample_MaximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOreEquals(
            string reactions,
            long maximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre
        )
        {
            Assert.Equal(maximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre, task.Solution(reactions));
        }

        public class Reactions_MaximumAmountOfFuelThatCanBeProducedWithGivenAmountOfOre_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "10 ORE => 10 A"
                    + $"{Environment.NewLine}1 ORE => 1 B"
                    + $"{Environment.NewLine}7 A, 1 B => 1 C"
                    + $"{Environment.NewLine}7 A, 1 C => 1 D"
                    + $"{Environment.NewLine}7 A, 1 D => 1 E"
                    + $"{Environment.NewLine}7 A, 1 E => 1 FUEL",
                    34482758620
                };

                yield return new object[] {
                    "9 ORE => 2 A"
                    + $"{Environment.NewLine}8 ORE => 3 B"
                    + $"{Environment.NewLine}7 ORE => 5 C"
                    + $"{Environment.NewLine}3 A, 4 B => 1 AB"
                    + $"{Environment.NewLine}5 B, 7 C => 1 BC"
                    + $"{Environment.NewLine}4 C, 1 A => 1 CA"
                    + $"{Environment.NewLine}2 AB, 3 BC, 4 CA => 1 FUEL",
                    6323777403
                };

                yield return new object[] {
                    "157 ORE => 5 NZVS"
                    + $"{Environment.NewLine}165 ORE => 6 DCFZ"
                    + $"{Environment.NewLine}44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL"
                    + $"{Environment.NewLine}12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ"
                    + $"{Environment.NewLine}179 ORE => 7 PSHF"
                    + $"{Environment.NewLine}177 ORE => 5 HKGWZ"
                    + $"{Environment.NewLine}7 DCFZ, 7 PSHF => 2 XJWVT"
                    + $"{Environment.NewLine}165 ORE => 2 GPVTF"
                    + $"{Environment.NewLine}3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT",
                    82892753
                };

                yield return new object[] {
                    "2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG"
                    + $"{Environment.NewLine}17 NVRVD, 3 JNWZP => 8 VPVL"
                    + $"{Environment.NewLine}53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL"
                    + $"{Environment.NewLine}22 VJHF, 37 MNCFX => 5 FWMGM"
                    + $"{Environment.NewLine}139 ORE => 4 NVRVD"
                    + $"{Environment.NewLine}144 ORE => 7 JNWZP"
                    + $"{Environment.NewLine}5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC"
                    + $"{Environment.NewLine}5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV"
                    + $"{Environment.NewLine}145 ORE => 6 MNCFX"
                    + $"{Environment.NewLine}1 NVRVD => 8 CXFTF"
                    + $"{Environment.NewLine}1 VJHF, 6 MNCFX => 4 RFSQX"
                    + $"{Environment.NewLine}176 ORE => 6 VJHF",
                    5586022
                };

                // yield return new object[] {
                //     "171 ORE => 8 CNZTR"
                //     + $"{Environment.NewLine}7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL"
                //     + $"{Environment.NewLine}114 ORE => 4 BHXH"
                //     + $"{Environment.NewLine}14 VRPVC => 6 BMBT"
                //     + $"{Environment.NewLine}6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL"
                //     + $"{Environment.NewLine}6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT"
                //     + $"{Environment.NewLine}15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW"
                //     + $"{Environment.NewLine}13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW"
                //     + $"{Environment.NewLine}5 BMBT => 4 WPTQ"
                //     + $"{Environment.NewLine}189 ORE => 9 KTJDG"
                //     + $"{Environment.NewLine}1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP"
                //     + $"{Environment.NewLine}12 VRPVC, 27 CNZTR => 2 XDBXC"
                //     + $"{Environment.NewLine}15 KTJDG, 12 BHXH => 5 XCVML"
                //     + $"{Environment.NewLine}3 BHXH, 2 VRPVC => 7 MZWV"
                //     + $"{Environment.NewLine}121 ORE => 7 VRPVC"
                //     + $"{Environment.NewLine}7 XCVML => 6 RJRHP"
                //     + $"{Environment.NewLine}5 BHXH, 4 VRPVC => 5 LTCX",
                //     460664
                // };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
