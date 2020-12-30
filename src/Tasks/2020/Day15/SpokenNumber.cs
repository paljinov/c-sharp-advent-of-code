using System.Collections.Generic;

namespace App.Tasks.Year2020.Day15
{
    public class SpokenNumber
    {
        public int FindNumberSpokenAtPosition(int[] startingNumbers, int spokenAtPosition)
        {
            Dictionary<int, (int, int)> spokenNumbers = GetStartingSpokenNumbers(startingNumbers);
            spokenNumbers.EnsureCapacity(spokenAtPosition);
            int spokenNumber = DoFindNumberSpokenAtPosition(spokenNumbers, spokenAtPosition);

            return spokenNumber;
        }


        /// <summary>
        /// Dictionary key is spoken number, tuple represents last two turns when number was spoken.
        /// </summary>
        /// <param name="startingNumbers"></param>
        /// <returns></returns>
        private Dictionary<int, (int, int)> GetStartingSpokenNumbers(int[] startingNumbers)
        {
            Dictionary<int, (int, int)> spokenNumbers = new Dictionary<int, (int, int)>();
            for (int turn = 1; turn <= startingNumbers.Length; turn++)
            {
                spokenNumbers.Add(startingNumbers[turn - 1], (turn, 0));
            }

            return spokenNumbers;
        }

        private int DoFindNumberSpokenAtPosition(
            Dictionary<int, (int, int)> spokenNumbers,
            int spokenAtPosition
        )
        {
            int spokenNumber = 0;
            int nextSpokenNumber = 0;

            for (int turn = spokenNumbers.Count + 1; turn <= spokenAtPosition; turn++)
            {
                spokenNumber = nextSpokenNumber;

                int lastSpokenTurn = 0;
                nextSpokenNumber = 0;
                if (spokenNumbers.ContainsKey(spokenNumber))
                {
                    lastSpokenTurn = spokenNumbers[spokenNumber].Item1;
                    nextSpokenNumber = turn - lastSpokenTurn;
                }

                spokenNumbers[spokenNumber] = (turn, lastSpokenTurn);
            }

            return spokenNumber;
        }
    }
}
