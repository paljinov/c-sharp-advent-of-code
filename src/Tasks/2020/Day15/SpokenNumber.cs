using System.Collections.Generic;

namespace App.Tasks.Year2020.Day15
{
    public class SpokenNumber
    {
        public int FindNumberSpokenAtPosition(int[] startingNumbers, int spokenAtPosition)
        {
            int spokenNumber = 0;
            int lastSpokenNumber = 0;

            // Dictionary key is number, tuple represents last 2 turns number was last spoken
            Dictionary<int, (int, int)> numbers = new Dictionary<int, (int, int)>();
            for (int turn = 1; turn <= startingNumbers.Length; turn++)
            {
                numbers.Add(startingNumbers[turn - 1], (turn, 0));
                if (turn == startingNumbers.Length)
                {
                    lastSpokenNumber = startingNumbers[turn - 1];
                }
            }

            for (int turn = startingNumbers.Length + 1; turn <= spokenAtPosition; turn++)
            {
                (int lastSpokenTurn, int lastSpokenTurnBeforeThen) = numbers[lastSpokenNumber];

                spokenNumber = 0;
                if (lastSpokenTurnBeforeThen > 0)
                {
                    spokenNumber = lastSpokenTurn - lastSpokenTurnBeforeThen;
                }

                if (numbers.ContainsKey(spokenNumber))
                {
                    numbers[spokenNumber] = (turn, numbers[spokenNumber].Item1);
                }
                else
                {
                    numbers.Add(spokenNumber, (turn, 0));
                }

                lastSpokenNumber = spokenNumber;
            }

            return spokenNumber;
        }
    }
}
