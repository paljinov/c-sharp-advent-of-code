/*
--- Part Two ---

Amused by the speed of your answer, the Elves are curious:

What would the new winning Elf's score be if the number of the last marble were
100 times larger?
*/

namespace App.Tasks.Year2018.Day9
{
    public class Part2 : ITask<long>
    {
        private const int LAST_MARBLE_TIMES_LARGER = 100;

        private readonly MarblesGameRepository marblesGameRepository;

        private readonly MarblesGame marblesGame;

        public Part2()
        {
            marblesGameRepository = new MarblesGameRepository();
            marblesGame = new MarblesGame();
        }

        public long Solution(string input)
        {
            int players = marblesGameRepository.GetPlayers(input);
            int lastMarble = marblesGameRepository.GetLastMarble(input) * LAST_MARBLE_TIMES_LARGER;

            long winningElfScore = marblesGame.CalculateWinningElfScore(players, lastMarble);

            return winningElfScore;
        }
    }
}
