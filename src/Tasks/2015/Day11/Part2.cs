/*
--- Part Two ---

Santa's password expired again. What's the next one?
*/

namespace App.Tasks.Year2015.Day11
{
    class Part2 : ITask<string>
    {
        private readonly NextPassword nextPasswordFinder;

        public Part2()
        {
            nextPasswordFinder = new NextPassword();
        }

        public string Solution(string input)
        {
            string nextPassword = nextPasswordFinder.Find(input);
            nextPassword = nextPasswordFinder.Find(nextPassword);

            return nextPassword;
        }
    }
}
