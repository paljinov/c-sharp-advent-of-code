/*
--- Part Two ---

Now that you're warmed up, it's time to play the real game.

A second compartment opens, this time labeled Dirac dice. Out of it falls a
single three-sided die.

As you experiment with the die, you feel a little strange. An informational
brochure in the compartment explains that this is a quantum die: when you roll
it, the universe splits into multiple copies, one copy for each possible outcome
of the die. In this case, rolling the die always splits the universe into three
copies: one where the outcome of the roll was 1, one where it was 2, and one
where it was 3.

The game is played the same as before, although to prevent things from getting
too far out of hand, the game now ends when either player's score reaches at
least 21.

Using the same starting positions as in the example above, player 1 wins in
444356092776315 universes, while player 2 merely wins in 341960390180808
universes.

Using your given starting positions, determine every possible outcome. Find the
player that wins in more universes; in how many universes does that player win?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2021.Day21
{
    public class Part2 : ITask<long>
    {
        private const int DIE_MAX_NUMBER = 3;

        private const int MINIMUM_WINNER_SCORE = 21;

        private readonly PlayersRepository playersRepository;

        private readonly DiracDice diracDice;

        public Part2()
        {
            playersRepository = new PlayersRepository();
            diracDice = new DiracDice();
        }

        public long Solution(string input)
        {
            Dictionary<int, int> playersStartingPositions = playersRepository.GetPlayersStartingPositions(input);
            long numberOfUniversesInWhichWinningPlayerWins = diracDice
                .CalculateNumberOfUniversesInWhichWinningPlayerWins(
                    playersStartingPositions, DIE_MAX_NUMBER, MINIMUM_WINNER_SCORE);

            return numberOfUniversesInWhichWinningPlayerWins;
        }
    }
}
