using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day18
{
    public class SnailfishAddition
    {
        private const char OPENING_BRACKET = '[';

        private const char CLOSING_BRACKET = ']';

        private const char COMMA = ',';

        private const int EXPLODE_WHEN_NESTED_INSIDE_TOTAL_PAIRS = 4;

        private const int EXPLODING_PAIR_REGULAR_NUMBER_REPLACEMENT = 0;

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

        public int CalculateLargestMagnitudeOfAnySumOfTwoDifferentSnailfishNumbers(string[] snailfishNumbersString)
        {
            int largestMagnitude = 0;

            List<Pair> snailfishNumbers = GetParsedSnailfishNumbers(snailfishNumbersString);

            for (int i = 0; i < snailfishNumbers.Count; i++)
            {
                for (int j = 0; j < snailfishNumbers.Count; j++)
                {
                    if (i != j)
                    {
                        Pair snailfishSum = Add(snailfishNumbers[i].Clone(), snailfishNumbers[j].Clone());
                        int magnitude = CalculateMagnitude(snailfishSum);

                        largestMagnitude = Math.Max(magnitude, largestMagnitude);
                    }
                }
            }

            return largestMagnitude;
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
            // To add two snailfish numbers, form a pair from the left and right parameters of the addition operator
            Pair sumPair = new Pair
            {
                LeftPair = left,
                RightPair = right
            };
            sumPair.LeftPair.Parent = sumPair;
            sumPair.RightPair.Parent = sumPair;

            Reduce(sumPair);

            return sumPair;
        }

        private void Reduce(Pair pair)
        {
            bool exploded = true;
            bool splitted = true;
            while (exploded || splitted)
            {
                // Explode leftmost pair which is nested required number of times
                exploded = Explode(pair);
                if (exploded)
                {
                    continue;
                }

                // Split leftmost two digit number
                splitted = Split(pair);
            }
        }

        private bool Explode(Pair pair, int depth = 0)
        {
            bool exploded = false;

            // Explode
            if (depth >= EXPLODE_WHEN_NESTED_INSIDE_TOTAL_PAIRS)
            {
                // The pair's left value is added to the first regular number
                // to the left of the exploding pair (if any)
                AddValueLeft(pair, pair.LeftNumber.Value);
                // The pair's right value is added to the first regular number
                // to the right of the exploding pair (if any)
                AddValueRight(pair, pair.RightNumber.Value);

                // If this is parent's left pair
                if (pair.Parent.LeftPair == pair)
                {
                    pair.Parent.LeftPair = null;
                    pair.Parent.LeftNumber = EXPLODING_PAIR_REGULAR_NUMBER_REPLACEMENT;
                }
                // If this is parent's right pair
                else if (pair.Parent.RightPair == pair)
                {
                    pair.Parent.RightPair = null;
                    pair.Parent.RightNumber = EXPLODING_PAIR_REGULAR_NUMBER_REPLACEMENT;
                }

                exploded = true;
            }
            else
            {
                depth++;

                if (pair.LeftPair != null)
                {
                    exploded = Explode(pair.LeftPair, depth);
                }

                if (!exploded && pair.RightPair != null)
                {
                    exploded = Explode(pair.RightPair, depth);
                }
            }

            return exploded;
        }

        private bool Split(Pair pair)
        {
            bool splitted = false;

            if (pair.LeftNumber.HasValue && pair.LeftNumber.Value >= SPLIT_NUMBER_GREATER_THAN)
            {
                double quotient = (double)pair.LeftNumber.Value / 2;
                int leftNumber = (int)Math.Floor(quotient);
                int rightNumber = (int)Math.Ceiling(quotient);

                Pair leftPair = new Pair
                {
                    LeftNumber = leftNumber,
                    RightNumber = rightNumber,
                    Parent = pair
                };

                pair.LeftNumber = null;
                pair.LeftPair = leftPair;

                splitted = true;
            }
            if (!splitted && pair.LeftPair != null)
            {
                splitted = Split(pair.LeftPair);
            }
            if (!splitted && pair.RightNumber.HasValue && pair.RightNumber.Value >= SPLIT_NUMBER_GREATER_THAN)
            {
                double quotient = (double)pair.RightNumber.Value / 2;
                int leftNumber = (int)Math.Floor(quotient);
                int rightNumber = (int)Math.Ceiling(quotient);

                Pair rightPair = new Pair
                {
                    LeftNumber = leftNumber,
                    RightNumber = rightNumber,
                    Parent = pair
                };

                pair.RightNumber = null;
                pair.RightPair = rightPair;

                splitted = true;
            }
            if (!splitted && pair.RightPair != null)
            {
                splitted = Split(pair.RightPair);
            }

            return splitted;
        }

        private void AddValueLeft(Pair pair, int value)
        {
            Pair parent = pair.Parent;
            while (parent != null)
            {
                // If parent has left number
                if (parent.LeftNumber.HasValue)
                {
                    parent.LeftNumber += value;
                    break;
                }

                // If current pair is parent's right pair
                if (parent.RightPair == pair)
                {
                    AddToPairRightmostNumber(parent.LeftPair, value);
                    break;
                }

                pair = parent;
                parent = parent.Parent;
            }
        }

        private void AddValueRight(Pair pair, int value)
        {
            Pair parent = pair.Parent;
            while (parent != null)
            {
                // If parent has right number
                if (parent.RightNumber.HasValue)
                {
                    parent.RightNumber += value;
                    break;
                }

                // If current pair is parent's left pair
                if (parent.LeftPair == pair)
                {
                    AddToPairLeftmostNumber(parent.RightPair, value);
                    break;
                }

                pair = parent;
                parent = parent.Parent;
            }
        }

        private void AddToPairRightmostNumber(Pair pair, int value)
        {
            if (pair.RightNumber.HasValue)
            {
                pair.RightNumber += value;
            }
            else if (pair.RightPair != null)
            {
                AddToPairRightmostNumber(pair.RightPair, value);
            }
        }

        private void AddToPairLeftmostNumber(Pair pair, int value)
        {
            if (pair.LeftNumber.HasValue)
            {
                pair.LeftNumber += value;
            }
            else if (pair.LeftPair != null)
            {
                AddToPairLeftmostNumber(pair.LeftPair, value);
            }
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
    }
}
