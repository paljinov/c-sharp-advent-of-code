/*
--- Part Two ---

Impressed, the Elves issue you a challenge: determine the 30000000th number
spoken. For example, given the same starting numbers as above:

- Given 0,3,6, the 30000000th number spoken is 175594.
- Given 1,3,2, the 30000000th number spoken is 2578.
- Given 2,1,3, the 30000000th number spoken is 3544142.
- Given 1,2,3, the 30000000th number spoken is 261214.
- Given 2,3,1, the 30000000th number spoken is 6895259.
- Given 3,2,1, the 30000000th number spoken is 18.
- Given 3,1,2, the 30000000th number spoken is 362.

Given your starting numbers, what will be the 30000000th number spoken?
*/

namespace App.Tasks.Year2020.Day15
{
    public class Part2 : ITask<int>
    {
        private const int SPOKEN_AT_POSITION = 30000000;

        private readonly StartingNumbersRepository startingNumbersRepository;

        private readonly SpokenNumber spokenNumber;

        public Part2()
        {
            startingNumbersRepository = new StartingNumbersRepository();
            spokenNumber = new SpokenNumber();
        }

        public int Solution(string input)
        {
            int[] startingNumbers = startingNumbersRepository.GetStartingNumbers(input);
            int number = spokenNumber.FindNumberSpokenAtPosition(startingNumbers, SPOKEN_AT_POSITION);

            return number;
        }
    }
}
