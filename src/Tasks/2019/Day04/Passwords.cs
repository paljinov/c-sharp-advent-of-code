namespace App.Tasks.Year2019.Day4
{
    public class Passwords
    {
        public int CountDifferentPasswordsWithinRangeWhichMeetCriteria((int Start, int End) passwordsRange)
        {
            int passwordsWithinRangeWhichMeetCriteria = 0;

            for (int password = passwordsRange.Start; password <= passwordsRange.End; password++)
            {
                if (TwoAdjacentDigitsAreSame(password) && DigitsNeverDecrease(password))
                {
                    passwordsWithinRangeWhichMeetCriteria++;
                }
            }

            return passwordsWithinRangeWhichMeetCriteria;
        }

        private bool TwoAdjacentDigitsAreSame(int password)
        {
            string passwordString = password.ToString();

            for (int i = 0; i < passwordString.Length - 1; i++)
            {
                if (passwordString[i] == passwordString[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        private bool DigitsNeverDecrease(int password)
        {
            string passwordString = password.ToString();

            for (int i = 0; i < passwordString.Length - 1; i++)
            {
                if (passwordString[i] > passwordString[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
