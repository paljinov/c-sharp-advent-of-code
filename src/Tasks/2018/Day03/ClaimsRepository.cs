using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day3
{
    public class ClaimsRepository
    {
        public List<Claim> GetClaims(string input)
        {
            List<Claim> claims = new List<Claim>();

            Regex claimRegex = new Regex(@"^#(\d+)\s@\s(\d+),(\d+):\s(\d+)x(\d+)$");
            string[] claimsArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string claimString in claimsArray)
            {
                Match claimMatch = claimRegex.Match(claimString);
                GroupCollection claimGroups = claimMatch.Groups;

                Claim claim = new Claim
                {
                    Id = int.Parse(claimGroups[1].Value),
                    FromLeftEdge = int.Parse(claimGroups[2].Value),
                    FromTopEdge = int.Parse(claimGroups[3].Value),
                    Wide = int.Parse(claimGroups[4].Value),
                    Tall = int.Parse(claimGroups[5].Value)
                };

                claims.Add(claim);
            }

            return claims;
        }
    }
}
