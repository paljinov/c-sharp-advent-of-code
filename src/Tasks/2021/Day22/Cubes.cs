using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day22
{
    public class Cubes
    {
        public int CalculateTurnedOnCubesInRegion(RebootStep[] rebootSteps, Cuboid region)
        {
            Dictionary<(int X, int Y, int Z), bool> regionCubes = new Dictionary<(int X, int Y, int Z), bool>();

            foreach (RebootStep rebootStep in rebootSteps)
            {
                (int From, int To) xRange = (
                    Math.Max(region.X.From, rebootStep.Cuboid.X.From),
                    Math.Min(region.X.To, rebootStep.Cuboid.X.To)
                );

                (int From, int To) yRange = (
                    Math.Max(region.Y.From, rebootStep.Cuboid.Y.From),
                    Math.Min(region.Y.To, rebootStep.Cuboid.Y.To)
                );

                (int From, int To) zRange = (
                    Math.Max(region.Z.From, rebootStep.Cuboid.Z.From),
                    Math.Min(region.Z.To, rebootStep.Cuboid.Z.To)
                );

                for (int x = xRange.From; x <= xRange.To; x++)
                {
                    for (int y = yRange.From; y <= yRange.To; y++)
                    {
                        for (int z = zRange.From; z <= zRange.To; z++)
                        {
                            regionCubes[(x, y, z)] = false;
                            if (rebootStep.Action == Action.TurnOn)
                            {
                                regionCubes[(x, y, z)] = true;
                            }
                        }
                    }
                }
            }

            int turnedOnCubesInRegion = regionCubes.Count(c => c.Value == true);

            return turnedOnCubesInRegion;
        }
    }
}
