/*
--- Part Two ---

The Elves decide they don't want to visit an infinite number of houses. Instead,
each Elf will stop after delivering presents to 50 houses. To make up for it,
they decide to deliver presents equal to eleven times their number at each
house.

With these changes, what is the new lowest house number of the house to get at
least as many presents as the number in your puzzle input?
*/

namespace App.Tasks.Year2015.Day20
{
    public class Part2 : ITask<int>
    {
        private const int PRESENTS_PER_ELF = 11;

        private readonly PresentsRepository presentsRepository;
        private readonly HouseNumber houseNumber;

        public Part2()
        {
            presentsRepository = new PresentsRepository();
            houseNumber = new HouseNumber();
        }

        public int Solution(string input)
        {
            int atLeastPresents = presentsRepository.GetLeastPresents(input);
            int lowestHouseNumber =
                houseNumber.CalculateLowestHouseNumberWhichGetsAtLeastPresentsWhenElfStopsAtFiftyHouses(
                    atLeastPresents, PRESENTS_PER_ELF);

            return lowestHouseNumber;
        }

    }
}
