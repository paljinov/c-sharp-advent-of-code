using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day20
{
    public class BlacklistRepository
    {
        public (uint, uint)[] GetBlacklistRanges(string input)
        {
            string[] blacklistRangesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (uint, uint)[] blacklistRanges = new (uint, uint)[blacklistRangesString.Length];

            Regex blacklistRangeRegex = new Regex(@"^(\d+)-(\d+)$");

            for (int i = 0; i < blacklistRangesString.Length; i++)
            {
                Match match = blacklistRangeRegex.Match(blacklistRangesString[i]);
                GroupCollection groups = match.Groups;

                blacklistRanges[i] = (uint.Parse(groups[1].Value), uint.Parse(groups[2].Value));
            }

            return blacklistRanges;
        }
    }
}
