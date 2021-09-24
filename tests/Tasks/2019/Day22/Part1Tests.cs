using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using App.Tasks.Year2019.Day22;
using Xunit;

namespace Tests.Tasks.Year2019.Day22
{
    public class Part1Tests
    {
        private const int TOTAL_CARDS = 10;

        private const int WANTED_CARD = 7;

        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();

            task.GetType()
                .GetField("wantedCard", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, WANTED_CARD);

            ShuffleCardDeck shuffleCardDeck = new ShuffleCardDeck();
            typeof(ShuffleCardDeck)
                .GetField("totalCards", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(shuffleCardDeck, TOTAL_CARDS);
            typeof(Part1)
                .GetField("shuffleCardDeck", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(task, shuffleCardDeck);
        }

        [Theory]
        [ClassData(typeof(CardDeck_CardPositionAfterShufflingDeck_TestData))]
        public void Solution_CardDeckExample_CardPositionAfterShufflingDeckEquals(
            string reactions,
            int cardPositionAfterShufflingDeck
        )
        {
            Assert.Equal(cardPositionAfterShufflingDeck, task.Solution(reactions));
        }

        public class CardDeck_CardPositionAfterShufflingDeck_TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    "deal with increment 7"
                    + $"{Environment.NewLine}deal into new stack"
                    + $"{Environment.NewLine}deal into new stack",
                    9
                };

                yield return new object[] {
                    "cut 6"
                    + $"{Environment.NewLine}deal with increment 7"
                    + $"{Environment.NewLine}deal into new stack",
                    2
                };

                yield return new object[] {
                    "deal with increment 7"
                    + $"{Environment.NewLine}deal with increment 9"
                    + $"{Environment.NewLine}cut -2",
                    3
                };

                yield return new object[] {
                    "deal into new stack"
                    + $"{Environment.NewLine}cut -2"
                    + $"{Environment.NewLine}deal with increment 7"
                    + $"{Environment.NewLine}cut 8"
                    + $"{Environment.NewLine}cut -4"
                    + $"{Environment.NewLine}deal with increment 7"
                    + $"{Environment.NewLine}cut 3"
                    + $"{Environment.NewLine}deal with increment 9"
                    + $"{Environment.NewLine}deal with increment 3"
                    + $"{Environment.NewLine}cut -1",
                    6
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
