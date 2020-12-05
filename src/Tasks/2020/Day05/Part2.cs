/*
--- Part Two ---

Ding! The "fasten seat belt" signs have turned on. Time to find your seat.

It's a completely full flight, so your seat should be the only missing boarding
pass in your list. However, there's a catch: some of the seats at the very front
and back of the plane don't exist on this aircraft, so they'll be missing from
your list as well.

Your seat wasn't at the very front or back, though; the seats with IDs +1 and -1
from yours will be in your list.

What is the ID of your seat?
*/

using System.Linq;

namespace App.Tasks.Year2020.Day5
{
    public class Part2 : ITask<int>
    {
        private readonly SeatsRepository seatsRepository;

        private readonly Seat seat;

        public Part2()
        {
            seatsRepository = new SeatsRepository();
            seat = new Seat();
        }

        public int Solution(string input)
        {
            string[] seats = seatsRepository.GetSeats(input);
            int[] seatsIds = seat.GetSeatsIds(seats);

            int highestSeatId = seatsIds.Max();
            int lowestSeatId = seatsIds.Min();

            int seatId = highestSeatId;
            for (int i = lowestSeatId + 1; i < highestSeatId; i++)
            {
                // Seat is between the seats with IDs +1 and -1 from yours
                if (!seatsIds.Contains(i) && seatsIds.Contains(i - 1) && seatsIds.Contains(i + 1))
                {
                    seatId = i;
                    break;
                }
            }

            return seatId;
        }
    }
}
