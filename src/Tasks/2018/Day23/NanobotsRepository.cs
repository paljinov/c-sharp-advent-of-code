using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day23
{
    public class NanobotsRepository
    {
        public Nanobot[] GetNanobots(string input)
        {
            string[] nanobotsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Nanobot[] nanobots = new Nanobot[nanobotsString.Length];

            Regex nanobotRegex = new Regex(@"^pos=<(-?\d+),(-?\d+),(-?\d+)>,\sr=(\d+)$");

            for (int i = 0; i < nanobotsString.Length; i++)
            {
                Match nanobotMatch = nanobotRegex.Match(nanobotsString[i]);
                GroupCollection nanobotGroups = nanobotMatch.Groups;

                Nanobot nanobot = new Nanobot
                {
                    Position = new Position
                    {
                        X = int.Parse(nanobotGroups[1].Value),
                        Y = int.Parse(nanobotGroups[2].Value),
                        Z = int.Parse(nanobotGroups[3].Value)
                    },
                    SignalRadius = int.Parse(nanobotGroups[4].Value)
                };

                nanobots[i] = nanobot;
            }

            return nanobots;
        }
    }
}
