namespace App.Tasks.Year2019.Day19
{
    public class IntegersRepository
    {
        public long[] GetIntegers(string input)
        {
            string[] integersString = input.Split(',');

            long[] integers = new long[integersString.Length];
            for (int i = 0; i < integersString.Length; i++)
            {
                integers[i] = long.Parse(integersString[i]);
            }

            return integers;
        }
    }
}
