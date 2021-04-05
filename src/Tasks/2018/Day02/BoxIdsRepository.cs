using System;

namespace App.Tasks.Year2018.Day2
{
    public class BoxIdsRepository
    {
        public string[] GetBoxIds(string input)
        {
            string[] boxIds = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return boxIds;
        }
    }
}
