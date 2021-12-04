using System;
using App.Tasks.Year2021.Day4;
using Xunit;

namespace Tests.Tasks.Year2021.Day4
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_DrawnNumbersAndBoardsExample_WinningBoardFinalScoreEquals()
        {
            string guardsRecords = "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1"
                + Environment.NewLine
                + $"{Environment.NewLine}22 13 17 11  0"
                + $"{Environment.NewLine} 8  2 23  4 24"
                + $"{Environment.NewLine}21  9 14 16  7"
                + $"{Environment.NewLine} 6 10  3 18  5"
                + $"{Environment.NewLine} 1 12 20 15 19"
                + Environment.NewLine
                + $"{Environment.NewLine} 3 15  0  2 22"
                + $"{Environment.NewLine} 9 18 13 17  5"
                + $"{Environment.NewLine}19  8  7 25 23"
                + $"{Environment.NewLine}20 11 10 24  4"
                + $"{Environment.NewLine}14 21 16 12  6"
                + Environment.NewLine
                + $"{Environment.NewLine}14 21 17 24  4"
                + $"{Environment.NewLine}10 16 15  9 19"
                + $"{Environment.NewLine}18  8 23 26 20"
                + $"{Environment.NewLine}22 11 13  6  5"
                + $"{Environment.NewLine} 2  0 12  3  7";

            Assert.Equal(4512, task.Solution(guardsRecords));
        }
    }
}
