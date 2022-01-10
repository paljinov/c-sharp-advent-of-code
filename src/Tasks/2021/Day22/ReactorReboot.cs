using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day22
{
    public class ReactorReboot
    {
        public int CountTurnedOnCubesInRegion(RebootStep[] rebootSteps, Cuboid region)
        {
            List<(int Operation, Cuboid Cuboid)> intersections = GetCuboidsIntersections(rebootSteps);
            int turnedOnCubesInRegion = (int)CountTurnedOnCubes(intersections, region);

            return turnedOnCubesInRegion;
        }

        public long CountTurnedOnCubes(RebootStep[] rebootSteps)
        {
            List<(int Operation, Cuboid Cuboid)> intersections = GetCuboidsIntersections(rebootSteps);
            long turnedOnCubes = CountTurnedOnCubes(intersections);

            return turnedOnCubes;
        }

        private List<(int Operation, Cuboid Cuboid)> GetCuboidsIntersections(RebootStep[] rebootSteps)
        {
            List<(int Operation, Cuboid Cuboid)> intersections = new List<(int Operation, Cuboid Cuboid)>();

            // Iterate each reboot step
            foreach (RebootStep rebootStep in rebootSteps)
            {
                List<(int Operation, Cuboid Cuboid)> rebootStepIntersections =
                    new List<(int Operation, Cuboid Cuboid)>();
                if (rebootStep.Action == Action.TurnOn)
                {
                    rebootStepIntersections.Add((1, rebootStep.Cuboid));
                }

                // Iterate turned on cuboids
                foreach ((int Operation, Cuboid Cuboid) intersection in intersections)
                {
                    // Get interesections between reboot step and turned on cuboid
                    Cuboid newIntersection = GetCuboidIntersection(rebootStep.Cuboid, intersection.Cuboid);
                    if (newIntersection != null)
                    {
                        rebootStepIntersections.Add((-intersection.Operation, newIntersection));
                    }
                }

                intersections.AddRange(rebootStepIntersections);
            }

            return intersections;
        }

        private Cuboid GetCuboidIntersection(Cuboid first, Cuboid second)
        {
            // If there is no intersection between cuboids
            if (first.X.From > second.X.To || first.X.To < second.X.From
                || first.Y.From > second.Y.To || first.Y.To < second.Y.From
                || first.Z.From > second.Z.To || first.Z.To < second.Z.From)
            {
                return null;
            }

            Cuboid intersection = new Cuboid
            {
                X = (Math.Max(first.X.From, second.X.From), Math.Min(first.X.To, second.X.To)),
                Y = (Math.Max(first.Y.From, second.Y.From), Math.Min(first.Y.To, second.Y.To)),
                Z = (Math.Max(first.Z.From, second.Z.From), Math.Min(first.Z.To, second.Z.To))
            };

            return intersection;
        }

        private long CountTurnedOnCubes(List<(int Operation, Cuboid Cuboid)> intersections, Cuboid considerOnlyCubesInRegion = null)
        {
            long turnedOnCubes = 0;

            foreach ((int Operation, Cuboid Cuboid) intersection in intersections)
            {
                int fromX = intersection.Cuboid.X.From;
                int toX = intersection.Cuboid.X.To;
                int fromY = intersection.Cuboid.Y.From;
                int toY = intersection.Cuboid.Y.To;
                int fromZ = intersection.Cuboid.Z.From;
                int toZ = intersection.Cuboid.Z.To;

                // If considering only cubes in the region
                if (considerOnlyCubesInRegion != null)
                {
                    // Fully outside region
                    if (considerOnlyCubesInRegion.X.From > toX || considerOnlyCubesInRegion.X.To < fromX)
                    {
                        continue;
                    }
                    else
                    {
                        fromX = considerOnlyCubesInRegion.X.From > fromX ? considerOnlyCubesInRegion.X.From : fromX;
                        toX = considerOnlyCubesInRegion.X.To < toX ? considerOnlyCubesInRegion.X.To : toX;
                    }

                    // Fully outside region
                    if (considerOnlyCubesInRegion.Y.From > toY || considerOnlyCubesInRegion.Y.To < fromY)
                    {
                        continue;
                    }
                    else
                    {
                        fromY = considerOnlyCubesInRegion.Y.From > fromY ? considerOnlyCubesInRegion.Y.From : fromY;
                        toY = considerOnlyCubesInRegion.Y.To < toY ? considerOnlyCubesInRegion.Y.To : toY;
                    }

                    // Fully outside region
                    if (considerOnlyCubesInRegion.Z.From > toZ || considerOnlyCubesInRegion.Z.To < fromZ)
                    {
                        continue;
                    }
                    else
                    {
                        fromZ = considerOnlyCubesInRegion.Z.From > fromZ ? considerOnlyCubesInRegion.Z.From : fromZ;
                        toZ = considerOnlyCubesInRegion.Z.To < toZ ? considerOnlyCubesInRegion.Z.To : toZ;
                    }
                }

                turnedOnCubes += (long)intersection.Operation
                    * (toX - fromX + 1) * (toY - fromY + 1) * (toZ - fromZ + 1);
            }

            return turnedOnCubes;
        }
    }
}
