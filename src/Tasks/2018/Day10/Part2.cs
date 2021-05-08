/*
--- Part Two ---

Good thing you didn't have to wait, because that would have taken a long time -
much longer than the 3 seconds in the example above.

Impressed by your sub-hour communication capabilities, the Elves are curious:
exactly how many seconds would they have needed to wait for that message to
appear?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2018.Day10
{
    public class Part2 : ITask<int>
    {
        private readonly PointsRepository pointsRepository;

        private readonly Message message;

        public Part2()
        {
            pointsRepository = new PointsRepository();
            message = new Message();
        }

        public int Solution(string input)
        {
            List<Point> points = pointsRepository.GetPoints(input);
            (_, int secondsNeededForMessageToAppear) = message.FindMessageWhichAppearsInTheSky(points);

            return secondsNeededForMessageToAppear;
        }
    }
}
