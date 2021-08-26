namespace App.Tasks.Year2019.Day16
{
    public class InputSignalRepository
    {
        public int[] GetInputSignal(string input)
        {
            int[] inputSignal = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                inputSignal[i] = (int)char.GetNumericValue(input[i]);
            }

            return inputSignal;
        }
    }
}
