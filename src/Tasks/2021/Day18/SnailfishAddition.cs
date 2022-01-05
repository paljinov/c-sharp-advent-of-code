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

            Pair snailfishSum = snailfishNumbers[0];
            for (int i = 1; i < snailfishNumbers.Count; i++)
            {
                snailfishSum = Add(snailfishSum, snailfishNumbers[i]);
            }

            int finalSumMagnitude = CalculateMagnitude(snailfishSum);

            return finalSumMagnitude;
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
                            Pair nextCurrent = new Pair { Parent = current };
                            if (left)
                            {
                                current.LeftPair = nextCurrent;
                            }
                            else
                            {
                                current.RightPair = nextCurrent;
                            }

                            current = nextCurrent;
                        }

                        left = true;
                        break;
                    case CLOSING_BRACKET:
                        // If not root element
                        if (current.Parent != null)
                        {
                            current = current.Parent;
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

        private Pair Add(Pair left, Pair right)
        {
            Console.WriteLine(GetPairString(left));
            Console.WriteLine(GetPairString(right));

            // To add two snailfish numbers, form a pair from the left and right parameters of the addition operator
            Pair sumPair = new Pair
            {
                LeftPair = left,
                RightPair = right
            };
            sumPair.LeftPair.Parent = sumPair;
            sumPair.RightPair.Parent = sumPair;

            Console.WriteLine(GetPairString(sumPair));

            Reduce(sumPair);
            Console.WriteLine(GetPairString(sumPair));

            return sumPair;
        }

        private void Reduce(Pair pair)
        {
            bool splitted = true;
            while (splitted)
            {
                Explode(pair);
                Console.WriteLine(GetPairString(pair));
                splitted = Split(pair);
                Console.WriteLine(GetPairString(pair));
            }
        }

        private void Explode(Pair pair, int depth = 0)
        {
            // Explode
            if (depth >= EXPLODE_NESTED_INSIDE_PAIRS)
            {
                int leftNumber = 0;
                if (pair.Parent.LeftNumber.HasValue)
                {
                    leftNumber = pair.LeftNumber.Value + pair.Parent.LeftNumber.Value;
                }

                int rightNumber = 0;
                if (pair.Parent.RightNumber.HasValue)
                {
                    rightNumber = pair.RightNumber.Value + pair.Parent.RightNumber.Value;
                }

                pair.Parent.LeftPair = null;
                pair.Parent.RightPair = null;

                pair.Parent.LeftNumber = leftNumber;
                pair.Parent.RightNumber = rightNumber;
            }
            else
            {
                if (pair.LeftPair != null)
                {
                    Console.WriteLine($"Left: {GetPairString(pair)}");
                    Explode(pair.LeftPair, depth + 1);
                }
                if (pair.RightPair != null)
                {
                    Console.WriteLine($"Right: {GetPairString(pair)}");
                    Explode(pair.RightPair, depth + 1);
                }
            }
        }

        private bool Split(Pair pair)
        {
            bool isSplitted = false;

            if (pair.LeftNumber.HasValue && pair.LeftNumber.Value >= SPLIT_NUMBER_GREATER_THAN)
            {
                int leftNumber = (int)Math.Floor((double)pair.LeftNumber.Value);
                int rightNumber = (int)Math.Ceiling((double)pair.LeftNumber.Value);

                Pair leftPair = new Pair
                {
                    LeftNumber = leftNumber,
                    RightNumber = rightNumber,
                    Parent = pair
                };

                pair.LeftNumber = null;
                pair.LeftPair = leftPair;

                isSplitted = true;
            }
            else if (pair.RightNumber.HasValue && pair.RightNumber.Value >= SPLIT_NUMBER_GREATER_THAN)
            {
                int leftNumber = (int)Math.Floor((double)pair.RightNumber.Value);
                int rightNumber = (int)Math.Ceiling((double)pair.RightNumber.Value);

                Pair rightPair = new Pair
                {
                    LeftNumber = leftNumber,
                    RightNumber = rightNumber,
                    Parent = pair
                };

                pair.RightNumber = null;
                pair.RightPair = rightPair;

                isSplitted = true;
            }
            else if (pair.LeftPair != null)
            {
                isSplitted = Split(pair.LeftPair);
            }
            else if (pair.RightPair != null)
            {
                isSplitted = Split(pair.RightPair);
            }

            return isSplitted;
        }

        private int CalculateMagnitude(Pair pair)
        {
            int magnitude = 0;

            if (pair.LeftNumber.HasValue)
            {
                magnitude += MAGNITUDE_LEFT_ELEMENT_MULTIPLIER * pair.LeftNumber.Value;
            }
            if (pair.RightNumber.HasValue)
            {
                magnitude += MAGNITUDE_RIGHT_ELEMENT_MULTIPLIER * pair.RightNumber.Value;
            }
            if (pair.LeftPair != null)
            {
                magnitude += MAGNITUDE_LEFT_ELEMENT_MULTIPLIER * CalculateMagnitude(pair.LeftPair);
            }
            if (pair.RightPair != null)
            {
                magnitude += MAGNITUDE_RIGHT_ELEMENT_MULTIPLIER * CalculateMagnitude(pair.RightPair);
            }

            return magnitude;
        }

        private string GetPairString(Pair pair)
        {
            string pairString = "";

            if (pair.LeftNumber.HasValue)
            {
                pairString += OPENING_BRACKET;
                pairString += pair.LeftNumber.Value;
                pairString += COMMA;
            }
            if (pair.RightNumber.HasValue)
            {
                pairString += pair.RightNumber.Value;
                pairString += CLOSING_BRACKET;
            }
            if (pair.LeftPair != null)
            {
                pairString += OPENING_BRACKET;
                pairString += GetPairString(pair.LeftPair);
                pairString += COMMA;
            }
            if (pair.RightPair != null)
            {
                pairString += GetPairString(pair.RightPair);
                pairString += CLOSING_BRACKET;
            }

            return pairString;
        }
    }
}
