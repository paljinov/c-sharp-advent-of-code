namespace App.Tasks.Year2021.Day8
{
    public class Digits
    {
        private readonly int[] digitsSegments = { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

        private readonly int[] easyDigits = { 1, 4, 7, 8 };

        public int CalculateEasyDigitsAppearances(SignalNote[] signalNotes)
        {
            int easyDigitsAppearances = 0;

            foreach (SignalNote signalNote in signalNotes)
            {
                foreach (string output in signalNote.OutputValues)
                {
                    foreach (int easyDigit in easyDigits)
                    {
                        if (output.Length == digitsSegments[easyDigit])
                        {
                            easyDigitsAppearances++;
                            break;
                        }
                    }
                }
            }

            return easyDigitsAppearances;
        }
    }
}
