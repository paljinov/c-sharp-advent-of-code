/*
--- Part Two ---

After getting the first capsule (it contained a star! what great fortune!), the
machine detects your success and begins to rearrange itself.

When it's done, the discs are back in their original configuration as if it were
time=0 again, but a new disc with 11 positions and starting at position 0 has
appeared exactly one second below the previously-bottom disc.

With this new disc, and counting again starting from time=0 with the
configuration in your puzzle input, what is the first time you can press the
button to get another capsule?
*/

using System.Linq;

namespace App.Tasks.Year2016.Day15
{
    public class Part2 : ITask<int>
    {
        private const int ADDITIONAL_DISC_TOTAL_POSITIONS = 11;

        private const int ADDITIONAL_DISC_INITIAL_POSITION = 0;


        private readonly DiscsRepository discsRepository;

        private readonly Capsule capsule;

        public Part2()
        {
            discsRepository = new DiscsRepository();
            capsule = new Capsule();
        }

        public int Solution(string input)
        {
            Disc[] discs = discsRepository.GetDiscs(input);
            Disc[] additionalDisc = new Disc[] {
                new Disc
                {
                    Number = discs[^1].Number + 1,
                    TotalPositions = ADDITIONAL_DISC_TOTAL_POSITIONS,
                    InitialPosition = ADDITIONAL_DISC_INITIAL_POSITION
                }
            };

            Disc[] combinedDiscs = discs.Concat(additionalDisc).ToArray();

            int firstTimeYouCanPressButtonAndGetCapsule =
                capsule.CalculateFirstTimeYouCanPressButtonAndGetCapsule(combinedDiscs);

            return firstTimeYouCanPressButtonAndGetCapsule;
        }
    }
}
