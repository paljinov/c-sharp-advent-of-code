using System.Text;

namespace App.Tasks.Year2015.Day10
{
    class LookAndSay
    {
        public static string GenerateSequence(string input)
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
