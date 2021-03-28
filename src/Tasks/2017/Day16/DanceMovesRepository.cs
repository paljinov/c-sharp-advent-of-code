using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day16
{
    public class DanceMovesRepository
    {
        public List<IDanceMove> GetDanceMoves(string input)
        {
            List<IDanceMove> danceMoves = new List<IDanceMove>();

            string[] danceMovesString = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            Regex spinRegex = new Regex(@"^s(\d+)$");
            Regex exchangeRegex = new Regex(@"^x(\d+)\/(\d+)$");
            Regex partnerRegex = new Regex(@"^p(\w)\/(\w)$");

            foreach (string danceMove in danceMovesString)
            {
                Match spinMatch = spinRegex.Match(danceMove);
                Match exchangeMatch = exchangeRegex.Match(danceMove);
                Match partnerMatch = partnerRegex.Match(danceMove);

                if (spinMatch.Success)
                {
                    danceMoves.Add(new DanceMove<int>
                    {
                        DanceMoveType = DanceMoveType.Spin,
                        ValueA = int.Parse(spinMatch.Groups[1].Value)
                    });
                }
                else if (exchangeMatch.Success)
                {
                    danceMoves.Add(new DanceMove<int>
                    {
                        DanceMoveType = DanceMoveType.Exchange,
                        ValueA = int.Parse(exchangeMatch.Groups[1].Value),
                        ValueB = int.Parse(exchangeMatch.Groups[2].Value)
                    });
                }
                else if (partnerMatch.Success)
                {
                    danceMoves.Add(new DanceMove<char>
                    {
                        DanceMoveType = DanceMoveType.Partner,
                        ValueA = partnerMatch.Groups[1].Value[0],
                        ValueB = partnerMatch.Groups[2].Value[0]
                    });
                }
            }

            return danceMoves;
        }
    }
}
