using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day17
{
    public class ClayVeinsRepository
    {
        public ClayVein[] GetClayVeins(string input)
        {
            string[] clayVeinsStrings = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            ClayVein[] clayVeins = new ClayVein[clayVeinsStrings.Length];

            Regex clayVeinRegex = new Regex(@"^(x|y)=(\d+),\s(x|y)=(\d+)\.\.(\d+)$");

            for (int i = 0; i < clayVeinsStrings.Length; i++)
            {
                Match clayVeinMatch = clayVeinRegex.Match(clayVeinsStrings[i]);
                GroupCollection clayVeinGroups = clayVeinMatch.Groups;

                ClayVein clayVein;

                if (clayVeinGroups[1].Value == "x")
                {
                    clayVein = new ClayVein
                    {
                        XFrom = int.Parse(clayVeinGroups[2].Value),
                        XTo = int.Parse(clayVeinGroups[2].Value),
                        YFrom = int.Parse(clayVeinGroups[4].Value),
                        YTo = int.Parse(clayVeinGroups[5].Value)
                    };
                }
                else
                {
                    clayVein = new ClayVein
                    {
                        YFrom = int.Parse(clayVeinGroups[2].Value),
                        YTo = int.Parse(clayVeinGroups[2].Value),
                        XFrom = int.Parse(clayVeinGroups[4].Value),
                        XTo = int.Parse(clayVeinGroups[5].Value)
                    };
                }

                clayVeins[i] = clayVein;
            }

            return clayVeins;
        }
    }
}
