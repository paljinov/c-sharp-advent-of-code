using System;
using App.Tasks.Year2018.Day10;
using Xunit;

namespace Tests.Tasks.Year2018.Day10
{
    public class Part2Tests
    {
        private readonly Part2 task;

        public Part2Tests()
        {
            task = new Part2();
        }

        [Fact]
        public void Solution_PointsExample_SecondsNeededForMessageToAppearEquals()
        {
            string points = "position=< 9,  1> velocity=< 0,  2>"
                + $"{Environment.NewLine}position=< 7,  0> velocity=<-1,  0>"
                + $"{Environment.NewLine}position=< 3, -2> velocity=<-1,  1>"
                + $"{Environment.NewLine}position=< 6, 10> velocity=<-2, -1>"
                + $"{Environment.NewLine}position=< 2, -4> velocity=< 2,  2>"
                + $"{Environment.NewLine}position=<-6, 10> velocity=< 2, -2>"
                + $"{Environment.NewLine}position=< 1,  8> velocity=< 1, -1>"
                + $"{Environment.NewLine}position=< 1,  7> velocity=< 1,  0>"
                + $"{Environment.NewLine}position=<-3, 11> velocity=< 1, -2>"
                + $"{Environment.NewLine}position=< 7,  6> velocity=<-1, -1>"
                + $"{Environment.NewLine}position=<-2,  3> velocity=< 1,  0>"
                + $"{Environment.NewLine}position=<-4,  3> velocity=< 2,  0>"
                + $"{Environment.NewLine}position=<10, -3> velocity=<-1,  1>"
                + $"{Environment.NewLine}position=< 5, 11> velocity=< 1, -2>"
                + $"{Environment.NewLine}position=< 4,  7> velocity=< 0, -1>"
                + $"{Environment.NewLine}position=< 8, -2> velocity=< 0,  1>"
                + $"{Environment.NewLine}position=<15,  0> velocity=<-2,  0>"
                + $"{Environment.NewLine}position=< 1,  6> velocity=< 1,  0>"
                + $"{Environment.NewLine}position=< 8,  9> velocity=< 0, -1>"
                + $"{Environment.NewLine}position=< 3,  3> velocity=<-1,  1>"
                + $"{Environment.NewLine}position=< 0,  5> velocity=< 0, -1>"
                + $"{Environment.NewLine}position=<-2,  2> velocity=< 2,  0>"
                + $"{Environment.NewLine}position=< 5, -2> velocity=< 1,  2>"
                + $"{Environment.NewLine}position=< 1,  4> velocity=< 2,  1>"
                + $"{Environment.NewLine}position=<-2,  7> velocity=< 2, -2>"
                + $"{Environment.NewLine}position=< 3,  6> velocity=<-1, -1>"
                + $"{Environment.NewLine}position=< 5,  0> velocity=< 1,  0>"
                + $"{Environment.NewLine}position=<-6,  0> velocity=< 2,  0>"
                + $"{Environment.NewLine}position=< 5,  9> velocity=< 1, -2>"
                + $"{Environment.NewLine}position=<14,  7> velocity=<-2,  0>"
                + $"{Environment.NewLine}position=<-3,  6> velocity=< 2, -1>";

            Assert.Equal(3, task.Solution(points));
        }
    }
}
