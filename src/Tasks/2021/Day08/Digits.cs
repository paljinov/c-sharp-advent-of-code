using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day8
{
    public class Digits
    {
        private readonly Dictionary<int, int> digitsSegments = new Dictionary<int, int>()
        {
            { 0, 6},
            { 1, 2},
            { 2, 5},
            { 3, 5},
            { 4, 4},
            { 5, 5},
            { 6, 6},
            { 7, 3},
            { 8, 7},
            { 9, 6}
        };

        private readonly int[] easyDigits = { 1, 4, 7, 8 };

        public int CalculateEasyDigitsAppearances(SignalNote[] signalNotes)
        {
            int easyDigitsAppearances = 0;

            foreach (SignalNote signalNote in signalNotes)
            {
                foreach (string output in signalNote.OutputValues)
                {
                    if (IsEasyDigit(output))
                    {
                        easyDigitsAppearances++;
                    }
                }
            }

            return easyDigitsAppearances;
        }

        public int CalculateOutputValuesSum(SignalNote[] signalNotes)
        {
            int outputValuesSum = 0;

            foreach (SignalNote signalNote in signalNotes)
            {
                Dictionary<string, int> signalsMapping = GetSignalsToDigitsMappings(signalNote);

                string outputValue = string.Empty;
                foreach (string output in signalNote.OutputValues)
                {
                    int digit = signalsMapping[string.Concat(output.OrderBy(c => c))];
                    outputValue += digit;
                }

                outputValuesSum += int.Parse(outputValue);
            }

            return outputValuesSum;
        }

        private bool IsEasyDigit(string digit)
        {
            foreach (int easyDigit in easyDigits)
            {
                if (digit.Length == digitsSegments[easyDigit])
                {
                    return true;
                }
            }

            return false;
        }

        private Dictionary<string, int> GetSignalsToDigitsMappings(SignalNote signalNote)
        {
            string one = signalNote.SignalPatterns.Where(sp => sp.Length == digitsSegments[1]).First();
            string four = signalNote.SignalPatterns.Where(sp => sp.Length == digitsSegments[4]).First();
            string seven = signalNote.SignalPatterns.Where(sp => sp.Length == digitsSegments[7]).First();
            string eight = signalNote.SignalPatterns.Where(sp => sp.Length == digitsSegments[8]).First();

            char a = string.Join("", seven.Split(one.ToCharArray()))[0];

            char[] removeLettersForNine = four.ToCharArray().Concat(new char[] { a }).ToArray();
            (char g, string nine) = FindLetterAndDigitSignal(signalNote, removeLettersForNine, 9, 1);

            char e = eight.Except(nine).First();
            (char b, string zero) = FindLetterAndDigitSignal(signalNote, new char[] { a, one[0], one[1], g, e }, 0, 1);
            string six = signalNote.SignalPatterns.Where(sp => sp.Length == digitsSegments[6]
                && sp != zero && sp != nine).First();

            char d = eight.Except(zero).First();
            char c = eight.Except(six).First();
            char f = one.Except(c.ToString()).First();

            (_, string two) = FindLetterAndDigitSignal(signalNote, new char[] { a, c, d, e, g }, 2, 0);
            (_, string three) = FindLetterAndDigitSignal(signalNote, new char[] { a, c, d, f, g }, 3, 0);
            (_, string five) = FindLetterAndDigitSignal(signalNote, new char[] { a, b, d, f, g }, 3, 0);

            return new Dictionary<string, int>()
            {
                { string.Concat(zero.OrderBy(c => c)), 0},
                { string.Concat(one.OrderBy(c => c)), 1},
                { string.Concat(two.OrderBy(c => c)), 2},
                { string.Concat(three.OrderBy(c => c)), 3},
                { string.Concat(four.OrderBy(c => c)), 4},
                { string.Concat(five.OrderBy(c => c)), 5},
                { string.Concat(six.OrderBy(c => c)), 6},
                { string.Concat(seven.OrderBy(c => c)), 7},
                { string.Concat(eight.OrderBy(c => c)), 8},
                { string.Concat(nine.OrderBy(c => c)), 9},
            };

        }

        private (char, string) FindLetterAndDigitSignal(SignalNote signalNote, char[] removeChars, int digit, int length)
        {
            char letter = '\0';
            string digitSignal = string.Empty;

            foreach (string signalPattern in signalNote.SignalPatterns)
            {
                if (signalPattern.Length == digitsSegments[digit])
                {
                    digitSignal = string.Join("", signalPattern.Split(removeChars));
                    if (digitSignal.Length == length)
                    {
                        if (length > 0)
                        {
                            letter = digitSignal[0];
                        }

                        digitSignal = signalPattern;
                        break;
                    }
                }
            }

            return (letter, digitSignal);
        }
    }
}
