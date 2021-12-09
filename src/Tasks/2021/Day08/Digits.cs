using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day8
{
    public class Digits
    {
        private readonly Dictionary<int, int> digitsTotalSegments = new Dictionary<int, int>()
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
                    int digit = signalsMapping[SortStringLetters(output)];
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
                if (digit.Length == digitsTotalSegments[easyDigit])
                {
                    return true;
                }
            }

            return false;
        }

        private Dictionary<string, int> GetSignalsToDigitsMappings(SignalNote signalNote)
        {
            string one = signalNote.SignalPatterns.Where(sp => sp.Length == digitsTotalSegments[1]).First();
            string four = signalNote.SignalPatterns.Where(sp => sp.Length == digitsTotalSegments[4]).First();
            string seven = signalNote.SignalPatterns.Where(sp => sp.Length == digitsTotalSegments[7]).First();
            string eight = signalNote.SignalPatterns.Where(sp => sp.Length == digitsTotalSegments[8]).First();

            char a = RemoveCharsFromString(seven, one.ToCharArray())[0];

            char[] removeLettersForNine = four.ToCharArray().Concat(new char[] { a }).ToArray();
            (char g, string nine) = FindLetterAndDigitSignal(signalNote, removeLettersForNine, 9, 1);

            char e = eight.Except(nine).First();
            (char b, string zero) = FindLetterAndDigitSignal(signalNote, new char[] { a, one[0], one[1], g, e }, 0, 1);
            string six = signalNote.SignalPatterns.Where(
                sp => sp.Length == digitsTotalSegments[6] && sp != zero && sp != nine).First();

            char d = eight.Except(zero).First();
            char c = eight.Except(six).First();
            char f = one.Except(c.ToString()).First();

            (_, string two) = FindLetterAndDigitSignal(signalNote, new char[] { a, c, d, e, g }, 2, 0);
            (_, string three) = FindLetterAndDigitSignal(signalNote, new char[] { a, c, d, f, g }, 3, 0);
            (_, string five) = FindLetterAndDigitSignal(signalNote, new char[] { a, b, d, f, g }, 3, 0);

            return new Dictionary<string, int>()
            {
                { SortStringLetters(zero), 0},
                { SortStringLetters(one), 1},
                { SortStringLetters(two), 2},
                { SortStringLetters(three), 3},
                { SortStringLetters(four), 4},
                { SortStringLetters(five), 5},
                { SortStringLetters(six), 6},
                { SortStringLetters(seven), 7},
                { SortStringLetters(eight), 8},
                { SortStringLetters(nine), 9},
            };
        }

        private (char, string) FindLetterAndDigitSignal(
            SignalNote signalNote,
            char[] removeChars,
            int digit,
            int remainingLength
        )
        {
            char letter = '\0';
            string digitSignal = string.Empty;

            foreach (string signalPattern in signalNote.SignalPatterns)
            {
                if (signalPattern.Length == digitsTotalSegments[digit])
                {
                    digitSignal = RemoveCharsFromString(signalPattern, removeChars);
                    if (digitSignal.Length == remainingLength)
                    {
                        if (remainingLength > 0)
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

        private string SortStringLetters(string @string)
        {
            return string.Concat(@string.OrderBy(c => c));
        }

        private string RemoveCharsFromString(string @string, char[] removeChars)
        {
            return string.Join("", @string.Split(removeChars));
        }
    }
}
