/*
--- Part Two ---

Amidst the chaos, you notice that exactly one claim doesn't overlap by even a
single square inch of fabric with any other claim. If you can somehow draw
attention to it, maybe the Elves will be able to make Santa's suit after all!

For example, in the claims above, only claim 3 is intact after all claims are
made.

What is the ID of the only claim that doesn't overlap?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2018.Day3
{
    public class Part2 : ITask<int>
    {
        private readonly ClaimsRepository claimsRepository;

        private readonly Fabric fabric;

        public Part2()
        {
            claimsRepository = new ClaimsRepository();
            fabric = new Fabric();
        }

        public int Solution(string input)
        {
            List<Claim> claims = claimsRepository.GetClaims(input);
            int idOfTheOnlyClaimThatDoesntOverlap = fabric.FindIdOfTheOnlyClaimThatDoesntOverlap(claims);

            return idOfTheOnlyClaimThatDoesntOverlap;
        }
    }
}
