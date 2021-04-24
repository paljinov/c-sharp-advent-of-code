using System.Collections.Generic;

namespace App.Tasks.Year2018.Day3
{
    public class Fabric
    {
        public int CalculateSquareInchesOfFabricWithinTwoOrMoreClaims(List<Claim> claims)
        {
            Dictionary<(int, int), int> fabric = new Dictionary<(int, int), int>();

            foreach (Claim claim in claims)
            {
                for (int i = claim.FromLeftEdge; i < claim.FromLeftEdge + claim.Wide; i++)
                {
                    for (int j = claim.FromTopEdge; j < claim.FromTopEdge + claim.Tall; j++)
                    {
                        if (fabric.ContainsKey((i, j)))
                        {
                            fabric[(i, j)]++;
                        }
                        else
                        {
                            fabric[(i, j)] = 1;
                        }
                    }
                }
            }

            int squareInchesOfFabricWithinTwoOrMoreClaims = 0;
            foreach (KeyValuePair<(int, int), int> square in fabric)
            {
                if (square.Value > 1)
                {
                    squareInchesOfFabricWithinTwoOrMoreClaims++;
                }
            }

            return squareInchesOfFabricWithinTwoOrMoreClaims;
        }
    }
}
