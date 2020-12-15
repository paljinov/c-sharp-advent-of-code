namespace App.Tasks.Year2020.Day15
{
    public class StartingNumbersRepository
    {
        public int[] GetStartingNumbers(string input)
        {
            string[] startingNumbersString = input.Split(",");
            int[] startingNumbers = new int[startingNumbersString.Length];

            for (int i = 0; i < startingNumbersString.Length; i++)
            {
                startingNumbers[i] = int.Parse(startingNumbersString[i]);
            }

            return startingNumbers;
        }
    }
}
