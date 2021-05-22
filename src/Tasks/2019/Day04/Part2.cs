/*
--- Part Two ---

An Elf just remembered one more important detail: the two adjacent matching
digits are not part of a larger group of matching digits.

Given this additional criterion, but still ignoring the range rule, the
following are now true:

- 112233 meets these criteria because the digits never decrease and all repeated
  digits are exactly two digits long.
- 123444 no longer meets the criteria (the repeated 44 is part of a larger group
  of 444).
- 111122 meets the criteria (even though 1 is repeated more than twice, it still
  contains a double 22).

How many different passwords within the range given in your puzzle input meet
all of the criteria?
*/

namespace App.Tasks.Year2019.Day4
{
    public class Part2 : ITask<int>
    {
        private readonly PasswordsRangeRepository passwordsRangeRepository;

        private readonly Passwords passwords;

        public Part2()
        {
            passwordsRangeRepository = new PasswordsRangeRepository();
            passwords = new Passwords();
        }

        public int Solution(string input)
        {
            (int Start, int End) passwordsRange = passwordsRangeRepository.GetPasswordsRange(input);
            int differentPasswordsWithinRangeWhichMeetCriteria =
                passwords.CountDifferentPasswordsWithinRangeWhichMeetCriteria(passwordsRange, true);

            return differentPasswordsWithinRangeWhichMeetCriteria;
        }
    }
}
