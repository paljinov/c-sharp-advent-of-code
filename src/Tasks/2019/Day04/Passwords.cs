namespace App.Tasks.Year2019.Day4
{
    public class Passwords
    {
        public int CountDifferentPasswordsWithinRangeWhichMeetCriteria(
            (int Start, int End) passwordsRange,
            bool adjacentMatchingDigitsAreNotPartOfLargerGroupCriteria = false
        )
        {
            int passwordsWithinRangeWhichMeetCriteria = 0;

            for (int password = passwordsRange.Start; password <= passwordsRange.End; password++)
            {
                string passwordString = password.ToString();

                if (TwoAdjacentDigitsAreSame(passwordString, adjacentMatchingDigitsAreNotPartOfLargerGroupCriteria)
                    && DigitsNeverDecrease(passwordString))
                {
                    passwordsWithinRangeWhichMeetCriteria++;
                }
            }

            return passwordsWithinRangeWhichMeetCriteria;
        }

        private bool TwoAdjacentDigitsAreSame(
            string password,
            bool adjacentMatchingDigitsAreNotPartOfLargerGroupCriteria
        )
        {
            int i = 0;
            while (i < password.Length - 1)
            {
                if (password[i] == password[i + 1])
                {
                    // If adjacent matching digits are not part of a larger group of matching digits is not criteria
                    // or group size is only two
                    if (!adjacentMatchingDigitsAreNotPartOfLargerGroupCriteria
                        || i + 2 == password.Length || password[i] != password[i + 2])
                    {
                        return true;
                    }
                    else
                    {
                        // In case of larger group move forward until digit changes
                        while (i + 1 < password.Length && password[i] == password[i + 1])
                        {
                            i++;
                        }
                    }
                }

                i++;
            }

            return false;
        }

        private bool DigitsNeverDecrease(string password)
        {
            for (int i = 0; i < password.Length - 1; i++)
            {
                if (password[i] > password[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
