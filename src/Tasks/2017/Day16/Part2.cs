/*
--- Part Two ---

Now that you're starting to get a feel for the dance moves, you turn your
attention to the dance as a whole.

Keeping the positions they ended up in from their previous dance, the programs
perform it again and again: including the first dance, a total of one billion
(1000000000) times.

In the example above, their second dance would begin with the order baedc, and
use the same dance moves:

- s1, a spin of size 1: cbaed.
- x3/4, swapping the last two programs: cbade.
- pe/b, swapping programs e and b: ceadb.

In what order are the programs standing after their billion dances?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2017.Day16
{
    public class Part2 : ITask<string>
    {
        private const int TOTAL_DANCES = 1000000000;

        private readonly DanceMovesRepository danceMovesRepository;

        private readonly Dance dance;

        public Part2()
        {
            danceMovesRepository = new DanceMovesRepository();
            dance = new Dance();
        }

        public string Solution(string input)
        {
            List<IDanceMove> danceMoves = danceMovesRepository.GetDanceMoves(input);
            string programsOrderAfterDance = dance.GetProgramsOrderAfterDance(danceMoves, TOTAL_DANCES);

            return programsOrderAfterDance;
        }
    }
}
