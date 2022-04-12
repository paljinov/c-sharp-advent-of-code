using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day19
{
    public class Beacons
    {
        private const int OVERLAP_BEACONS = 12;

        public int CountBeacons(Dictionary<int, List<Position>> beaconsRelativePositions)
        {
            Dictionary<int, List<Position>> beaconsAbsolutePositions = new Dictionary<int, List<Position>>
            {
                [0] = beaconsRelativePositions[0]
            };

            List<Rotation> rotations = new List<Rotation>()
            {
                new Rotation{X = "x", Y = "y", Z = "z"},
                new Rotation{X = "x", Y = "-z", Z = "y"},
                new Rotation{X = "x", Y = "-y", Z = "-z"},
                new Rotation{X = "x", Y = "z", Z = "-y"},
                new Rotation{X = "-y", Y = "x", Z = "z"},
                new Rotation{X = "z", Y = "x", Z = "y"},
                new Rotation{X = "y", Y = "x", Z = "-z"},
                new Rotation{X = "-z", Y = "x", Z = "-y"},
                new Rotation{X = "-x", Y = "-y", Z = "z"},
                new Rotation{X = "-x", Y = "-z", Z = "-y"},
                new Rotation{X = "-x", Y = "y", Z = "-z"},
                new Rotation{X = "-x", Y = "z", Z = "y"},
                new Rotation{X = "y", Y = "-x", Z = "z"},
                new Rotation{X = "z", Y = "-x", Z = "-y"},
                new Rotation{X = "-y", Y = "-x", Z = "-z"},
                new Rotation{X = "-z", Y = "-x", Z = "y"},
                new Rotation{X = "-z", Y = "y", Z = "x"},
                new Rotation{X = "y", Y = "z", Z = "x"},
                new Rotation{X = "z", Y = "-y", Z = "x"},
                new Rotation{X = "-y", Y = "-z", Z = "x"},
                new Rotation{X = "-z", Y = "-y", Z = "-x"},
                new Rotation{X = "-y", Y = "z", Z = "-x"},
                new Rotation{X = "z", Y = "y", Z = "-x"},
                new Rotation{X = "y", Y = "-z", Z = "-x"}
            };

            for (int scanner = 1; scanner < beaconsRelativePositions.Count; scanner++)
            {
                foreach (KeyValuePair<int, List<Position>> absolutePositions in beaconsAbsolutePositions)
                {
                    foreach (Rotation rotation in rotations)
                    {
                        List<Position> rotatedBeacons = new List<Position>();
                        foreach (Position beacon in beaconsRelativePositions[scanner])
                        {
                            Position rotatedBeacon = RotateBeacon(beacon, rotation);
                            rotatedBeacons.Add(rotatedBeacon);
                        }

                        if (CountSameBeacons(absolutePositions.Value, rotatedBeacons) >= OVERLAP_BEACONS)
                        {
                            beaconsAbsolutePositions[scanner] = rotatedBeacons;
                            break;
                        }
                    }

                    if (beaconsAbsolutePositions.ContainsKey(scanner))
                    {
                        break;
                    }
                }
            }

            return beaconsRelativePositions.Count;
        }

        public int CalculateLargestManhattanDistanceBetweenAnyTwoScanners(
            Dictionary<int, List<Position>> beaconsRelativePositions
        )
        {
            return beaconsRelativePositions.Count;
        }

        private Position RotateBeacon(Position position, Rotation rotation)
        {
            int x = RotateAxis(rotation.X, position);
            int y = RotateAxis(rotation.Y, position);
            int z = RotateAxis(rotation.Z, position);

            return new Position
            {
                X = x,
                Y = y,
                Z = z
            };
        }

        private int RotateAxis(string rotateAxis, Position position)
        {
            char rotatedAxis = rotateAxis[^1];

            int axis = rotatedAxis == 'x' ? position.X : (rotatedAxis == 'y' ? position.Y : position.Z);
            if (rotateAxis[0] == '-')
            {
                axis = -axis;
            }

            return axis;
        }

        private int CountSameBeacons(List<Position> firstBeacons, List<Position> secondBeacons)
        {
            int sameBeacons = 0;

            foreach (Position first in firstBeacons)
            {
                foreach (Position second in secondBeacons)
                {
                    if (first.X == second.X && first.Y == second.Y && first.Z == second.Z)
                    {
                        sameBeacons++;
                    }
                }
            }

            return sameBeacons;
        }
    }
}
