using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day18
{
    public class SnailfishAddition
    {
        private const char OPENING_BRACKET = '[';

        private const char CLOSING_BRACKET = ']';

        private const char COMMA = ',';

        private const int EXPLODE_NESTED_INSIDE_PAIRS = 4;

        private const int SPLIT_NUMBER_GREATER_THAN = 10;

        private const int MAGNITUDE_LEFT_ELEMENT_MULTIPLIER = 3;

        private const int MAGNITUDE_RIGHT_ELEMENT_MULTIPLIER = 2;

        public int CalculateFinalSumMagnitude(string[] snailfishNumbersString)
        {
            List<Pair> snailfishNumbers = GetParsedSnailfishNumbers(snailfishNumbersString);

            for (int i = 1; i < snailfishNumbers.Count; i++)
            {

            }

            return 0;
        }

        private List<Pair> GetParsedSnailfishNumbers(string[] snailfishNumbersString)
        {
            List<Pair> snailfishNumbers = new List<Pair>();
            for (int i = 0; i < snailfishNumbersString.Length; i++)
            {
                Pair snailfishNumber = Parse(snailfishNumbersString[i]);
                snailfishNumbers.Add(snailfishNumber);
            }

            return snailfishNumbers;
        }

        private Pair Parse(string snailfishNumberString)
        {
            Pair current = null;
            Stack<Pair> parents = new Stack<Pair>();
            bool left = true;

            for (int i = 0; i < snailfishNumberString.Length; i++)
            {
                switch (snailfishNumberString[i])
                {
                    case OPENING_BRACKET:
                        // If root
                        if (current == null)
                        {
                            current = new Pair();
                        }
                        else
                        {
                            Pair nextCurrent = new Pair();
                            if (left)
                            {
                                current.LeftPair = nextCurrent;
                            }
                            else
                            {
                                current.RightPair = nextCurrent;
                            }

                            parents.Push(current);
                            current = nextCurrent;
                        }

                        left = true;
                        break;
                    case CLOSING_BRACKET:
                        // If not root element
                        if (parents.Count > 0)
                        {
                            current = parents.Pop();
                        }
                        break;
                    case COMMA:
                        left = false;
                        break;
                    // Number
                    default:
                        int element = (int)char.GetNumericValue(snailfishNumberString[i]);
                        if (left)
                        {
                            current.LeftNumber = element;
                        }
                        else
                        {
                            current.RightNumber = element;
                        }
                        break;
                }
            }

            return current;
        }
    }
}
