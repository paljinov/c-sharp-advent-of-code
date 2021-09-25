/*
--- Part Two ---

After a while, you realize your shuffling skill won't improve much more with
merely a single deck of cards. You ask every 3D printer on the ship to make you
some more cards while you check on the ship repairs. While reviewing the work
the droids have finished so far, you think you see Halley's Comet fly past!

When you get back, you discover that the 3D printers have combined their power
to create for you a single, giant, brand new, factory order deck of
119315717514047 space cards.

Finally, a deck of cards worthy of shuffling!

You decide to apply your complete shuffle process (your puzzle input) to the
deck 101741582076661 times in a row.

You'll need to be careful, though - one wrong move with this many cards and you
might overflow your entire ship!

After shuffling your new, giant, factory order deck that many times, what number
is on the card that ends up in position 2020?
*/

namespace App.Tasks.Year2019.Day22
{
    public class Part2 : ITask<int>
    {
        private const long SHUFFLE_PROCESS_REPETITIONS = 101741582076661;

        private readonly long totalCards = 119315717514047;

        private readonly int wantedCardPosition = 2020;

        private readonly ShuffleTechniquesRepository shuffleTechniquesRepository;

        private readonly ShuffleCardDeck shuffleCardDeck;

        public Part2()
        {
            shuffleTechniquesRepository = new ShuffleTechniquesRepository();
            shuffleCardDeck = new ShuffleCardDeck();
        }

        public int Solution(string input)
        {
            IShuffleTechnique[] shuffleTechniques = shuffleTechniquesRepository.GetShuffleTechniques(input);
            int cardNumberAfterShufflingDeck = shuffleCardDeck.CalculateCardNumberAfterShufflingDeck(
                shuffleTechniques, totalCards, wantedCardPosition, SHUFFLE_PROCESS_REPETITIONS);

            return cardNumberAfterShufflingDeck;
        }
    }
}
