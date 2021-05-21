namespace App.Tasks.Year2019.Day4
{
    public class PasswordsRangeRepository
    {
        public (int Start, int End) GetPasswordsRange(string input)
        {
            string[] passwordsRangeArray = input.Split('-');

            int start = int.Parse(passwordsRangeArray[0]);
            int end = int.Parse(passwordsRangeArray[1]);

            return (start, end);
        }
    }
}
