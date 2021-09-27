using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace App.Tasks.Year2019.Day22
{
    public class ShuffleCardDeck
    {
        public int CalculateCardPositionAfterShufflingDeck(
            IShuffleTechnique[] shuffleTechniques,
            int totalCards,
            int wantedCard
        )
        {
            Dictionary<long, long> cards = InitializeCards(totalCards);

            foreach (IShuffleTechnique shuffleTechnique in shuffleTechniques)
            {
                if (shuffleTechnique is DealIntoNewStack)
                {
                    cards = DealIntoNewStack(cards);
                }
                else if (shuffleTechnique is CutCards cutCards)
                {
                    cards = CutCards(cards, cutCards);
                }
                else
                {
                    cards = DealWithIncrement(cards, (DealWithIncrement)shuffleTechnique);
                }
            }

            int wantedCardPosition = (int)cards.FirstOrDefault(c => c.Value == wantedCard).Key;

            return wantedCardPosition;
        }

        public long CalculateCardNumberAfterShufflingDeck(
            IShuffleTechnique[] shuffleTechniques,
            long totalCards,
            int wantedCardPosition,
            long shuffleProcessRepetitions
        )
        {
            BigInteger start = 0;
            BigInteger step = 1;

            (start, step) = Shuffle(start, step, totalCards, shuffleTechniques);
            (start, step) = RepeatShuffleProcess(start, step, totalCards, shuffleProcessRepetitions);

            long cardNumberAfterShufflingDeck = (long)((start + step * wantedCardPosition) % totalCards);

            return cardNumberAfterShufflingDeck;
        }

        private Dictionary<long, long> InitializeCards(long totalCards)
        {
            Dictionary<long, long> cards = new Dictionary<long, long>();

            for (int i = 0; i < totalCards; i++)
            {
                cards[i] = i;
            }

            return cards;
        }

        private Dictionary<long, long> DealIntoNewStack(Dictionary<long, long> cards)
        {
            Dictionary<long, long> newStack = new Dictionary<long, long>();

            for (int i = 0; i < cards.Count; i++)
            {
                newStack[i] = cards[cards.Count - 1 - i];
            }

            return newStack;
        }

        private Dictionary<long, long> CutCards(Dictionary<long, long> cards, CutCards cutCards)
        {
            Dictionary<long, long> newStack = new Dictionary<long, long>();
            List<long> newStackList;

            if (cutCards.Cut > 0)
            {
                newStackList = cards.Values.TakeLast(cards.Count - cutCards.Cut)
                    .Concat(cards.Values.Take(cutCards.Cut)).ToList();
            }
            else
            {
                newStackList = cards.Values.TakeLast(Math.Abs(cutCards.Cut))
                    .Concat(cards.Values.Take(cards.Count - Math.Abs(cutCards.Cut))).ToList();
            }

            for (int i = 0; i < newStackList.Count; i++)
            {
                newStack[i] = newStackList[i];
            }

            return newStack;
        }

        private Dictionary<long, long> DealWithIncrement(
            Dictionary<long, long> cards,
            DealWithIncrement dealWithIncrement)
        {
            Dictionary<long, long> newStack = new Dictionary<long, long>();

            int newPosition = 0;
            int oldPosition = 0;
            while (oldPosition < cards.Count)
            {
                newStack[newPosition] = cards[oldPosition];

                oldPosition++;

                newPosition += dealWithIncrement.Increment;
                if (newPosition >= cards.Count)
                {
                    newPosition -= cards.Count;
                }
            }

            newStack = newStack.OrderBy(c => c.Key).ToDictionary(c => c.Key, c => c.Value);

            return newStack;
        }

        private (BigInteger, BigInteger) Shuffle(
            BigInteger start,
            BigInteger step,
            long totalCards,
            IShuffleTechnique[] shuffleTechniques
        )
        {
            for (long i = 0; i < shuffleTechniques.Length; i++)
            {
                IShuffleTechnique shuffleTechnique = shuffleTechniques[i];

                if (shuffleTechnique is DealIntoNewStack)
                {
                    start = (start - step) % totalCards;
                    step = -step % totalCards;
                }
                else if (shuffleTechnique is CutCards cutCards)
                {
                    long cut = cutCards.Cut < 0 ? cutCards.Cut + totalCards : cutCards.Cut;
                    start = (start + step * cut) % totalCards;
                }
                else
                {
                    int increment = ((DealWithIncrement)shuffleTechnique).Increment;
                    step = (step * ModPow(increment, totalCards - 2, totalCards) % totalCards);
                }
            }

            return (start, step);
        }

        private (BigInteger, BigInteger) RepeatShuffleProcess(
            BigInteger start,
            BigInteger step,
            long totalCards,
            long shuffleProcessRepetitions
        )
        {
            BigInteger lastStep = ModPow((long)step, shuffleProcessRepetitions, totalCards);
            BigInteger lastStart = (start * (1 - lastStep) * ModPow((long)(1 - step), totalCards - 2, totalCards))
                % totalCards;

            return (lastStart, lastStep);
        }

        private BigInteger ModPow(long @base, long exponent, long modulus)
        {
            BigInteger result = BigInteger.ModPow(@base, exponent, modulus);
            return result;
        }
    }
}
