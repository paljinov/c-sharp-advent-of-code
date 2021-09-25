using System;
using System.Collections.Generic;
using System.Linq;

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

        public int CalculateCardNumberAfterShufflingDeck(
            IShuffleTechnique[] shuffleTechniques,
            long totalCards,
            int wantedCardPosition,
            long shuffleProcessRepetitions
        )
        {
            Dictionary<long, long> cards = InitializeCards(totalCards);

            return cards.Count;
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
    }
}
