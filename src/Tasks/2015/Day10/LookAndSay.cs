using System.Text;

namespace App.Tasks.Year2015.Day10
{
    public class LookAndSay
    {
        public int ResultSequenceLength(string input, int repetitions)
        {
            string resultSequence = input;
            for (int i = 0; i < repetitions; i++)
            {
                resultSequence = GenerateSequence(resultSequence);
            }

            return resultSequence.Length;
        }

        private string GenerateSequence(string input)
        {
            StringBuilder sb = new StringBuilder();

            // Initialize current digit and its repetitions
            char currentDigit = input[0];
            int currentDigitRepetitions = 1;

            for (int i = 1; i <= input.Length; i++)
            {
                // If input sequence end is reached
                if (i == input.Length)
                {
                    sb.Append(currentDigitRepetitions.ToString() + currentDigit);
                }
                // If digit changed
                else if (currentDigit != input[i])
                {
                    sb.Append(currentDigitRepetitions.ToString() + currentDigit);

                    currentDigit = input[i];
                    currentDigitRepetitions = 1;
                }
                // Increase current digit repetitions
                else
                {
                    currentDigitRepetitions++;
                }
            }

            return sb.ToString();
        }
    }
}
