using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day22
{
    public class ReactorReboot
    {
        public int CountTurnedOnCubesInRegion(RebootStep[] rebootSteps, Cuboid region)
        {
            int turnedOnCubesInRegion = (int)DoCountTurnedOnCubes(rebootSteps, region);
            return turnedOnCubesInRegion;
        }

        public long CountTurnedOnCubes(RebootStep[] rebootSteps)
        {
            long turnedOnCubes = DoCountTurnedOnCubes(rebootSteps);
            return turnedOnCubes;
        }

        private long DoCountTurnedOnCubes(RebootStep[] rebootSteps, Cuboid region = null)
        {
            Dictionary<(int X, int Y, int Z), bool> regionCubes = new Dictionary<(int X, int Y, int Z), bool>();

            foreach (RebootStep rebootStep in rebootSteps)
            {
                Cuboid cuboid = rebootStep.Cuboid;
                (int From, int To) xRange = (cuboid.X.From, cuboid.X.To);
                (int From, int To) yRange = (cuboid.Y.From, cuboid.Y.To);
                (int From, int To) zRange = (cuboid.Z.From, cuboid.Z.To);

                // If considering only cubes in the region
                if (region != null)
                {
                    xRange = (Math.Max(region.X.From, cuboid.X.From), Math.Min(region.X.To, cuboid.X.To));
                    yRange = (Math.Max(region.Y.From, cuboid.Y.From), Math.Min(region.Y.To, cuboid.Y.To));
                    zRange = (Math.Max(region.Z.From, cuboid.Z.From), Math.Min(region.Z.To, cuboid.Z.To));
                }

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
