using System;
using System.Collections;
using System.Collections.Generic;
using App.Tasks.Year2021.Day22;
using Xunit;

namespace Tests.Tasks.Year2021.Day22
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Theory]
        [ClassData(typeof(RebootSteps_TurnedOnCubesCount_TestData))]
        public void Solution_RebootStepsExample_TurnedOnCubesInRegionCountEquals(string rebootSteps, int turnedOnCubes)
        {
            Assert.Equal(turnedOnCubes, task.Solution(rebootSteps));
        }

        public class RebootSteps_TurnedOnCubesCount_TestData
            : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "on x=10..12,y=10..12,z=10..12"
                    + $"{Environment.NewLine}on x=11..13,y=11..13,z=11..13"
                    + $"{Environment.NewLine}off x=9..11,y=9..11,z=9..11"
                    + $"{Environment.NewLine}on x=10..10,y=10..10,z=10..10",
                    39
                };

                yield return new object[] {
                    "on x=-20..26,y=-36..17,z=-47..7"
                    + $"{Environment.NewLine}on x=-20..33,y=-21..23,z=-26..28"
                    + $"{Environment.NewLine}on x=-22..28,y=-29..23,z=-38..16"
                    + $"{Environment.NewLine}on x=-46..7,y=-6..46,z=-50..-1"
                    + $"{Environment.NewLine}on x=-49..1,y=-3..46,z=-24..28"
                    + $"{Environment.NewLine}on x=2..47,y=-22..22,z=-23..27"
                    + $"{Environment.NewLine}on x=-27..23,y=-28..26,z=-21..29"
                    + $"{Environment.NewLine}on x=-39..5,y=-6..47,z=-3..44"
                    + $"{Environment.NewLine}on x=-30..21,y=-8..43,z=-13..34"
                    + $"{Environment.NewLine}on x=-22..26,y=-27..20,z=-29..19"
                    + $"{Environment.NewLine}off x=-48..-32,y=26..41,z=-47..-37"
                    + $"{Environment.NewLine}on x=-12..35,y=6..50,z=-50..-2"
                    + $"{Environment.NewLine}off x=-48..-32,y=-32..-16,z=-15..-5"
                    + $"{Environment.NewLine}on x=-18..26,y=-33..15,z=-7..46"
                    + $"{Environment.NewLine}off x=-40..-22,y=-38..-28,z=23..41"
                    + $"{Environment.NewLine}on x=-16..35,y=-41..10,z=-47..6"
                    + $"{Environment.NewLine}off x=-32..-23,y=11..30,z=-14..3"
                    + $"{Environment.NewLine}on x=-49..-5,y=-3..45,z=-29..18"
                    + $"{Environment.NewLine}off x=18..30,y=-20..-8,z=-3..13"
                    + $"{Environment.NewLine}on x=-41..9,y=-7..43,z=-33..15"
                    + $"{Environment.NewLine}on x=-54112..-39298,y=-85059..-49293,z=-27449..7877"
                    + $"{Environment.NewLine}on x=967..23432,y=45373..81175,z=27513..53682",
                    590784
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
