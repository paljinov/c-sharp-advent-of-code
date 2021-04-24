using System.Collections.Generic;

namespace App.Tasks.Year2018.Day3
{
    public class Fabric
    {
        public int CalculateSquareInchesOfFabricWithinTwoOrMoreClaims(List<Claim> claims)
        {
            Dictionary<(int, int), HashSet<int>> fabric = GetFabric(claims);

            int squareInchesOfFabricWithinTwoOrMoreClaims = 0;
            foreach (KeyValuePair<(int, int), HashSet<int>> square in fabric)
            {
                HashSet<int> claimIdsOnSquare = square.Value;
                if (claimIdsOnSquare.Count > 1)
                {
                    squareInchesOfFabricWithinTwoOrMoreClaims++;
                }
            }

            return squareInchesOfFabricWithinTwoOrMoreClaims;
        }

        public int FindIdOfTheOnlyClaimThatDoesntOverlap(List<Claim> claims)
        {
            int idOfTheOnlyClaimThatDoesntOverlap = 0;

            Dictionary<(int, int), HashSet<int>> fabric = GetFabric(claims);

            foreach (Claim claim in claims)
            {
                bool claimThatDoesntOverlap = true;
                foreach (KeyValuePair<(int, int), HashSet<int>> square in fabric)
                {
                    HashSet<int> claimIdsOnSquare = square.Value;
                    if (claimIdsOnSquare.Contains(claim.Id) && claimIdsOnSquare.Count > 1)
                    {
                        claimThatDoesntOverlap = false;
                        break;
                    }
                }

                if (claimThatDoesntOverlap)
                {
                    idOfTheOnlyClaimThatDoesntOverlap = claim.Id;
                    break;
                }
            }

            return idOfTheOnlyClaimThatDoesntOverlap;
        }

        private Dictionary<(int, int), HashSet<int>> GetFabric(List<Claim> claims)
        {
            Dictionary<(int, int), HashSet<int>> fabric = new Dictionary<(int, int), HashSet<int>>();

            foreach (Claim claim in claims)
            {
                for (int i = claim.FromLeftEdge; i < claim.FromLeftEdge + claim.Wide; i++)
                {
                    for (int j = claim.FromTopEdge; j < claim.FromTopEdge + claim.Tall; j++)
                    {
                        if (!fabric.ContainsKey((i, j)))
                        {
                            fabric[(i, j)] = new HashSet<int>();
                        }

                        fabric[(i, j)].Add(claim.Id);
                    }
                }
            }

            return fabric;
        }
    }
}
