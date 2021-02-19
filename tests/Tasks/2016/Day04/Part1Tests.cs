using System;
using App.Tasks.Year2016.Day4;
using Xunit;

namespace Tests.Tasks.Year2016.Day4
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_PossibleRoomsExample_RealRoomsSectorIdsSumEquals()
        {
            string possibleRooms = "aaaaa-bbb-z-y-x-123[abxyz]"
                + $"{Environment.NewLine}a-b-c-d-e-f-g-h-987[abcde]"
                + $"{Environment.NewLine}not-a-real-room-404[oarel]"
                + $"{Environment.NewLine}totally-real-room-200[decoy]";

            Assert.Equal(1514, task.Solution(possibleRooms));
        }
    }
}
