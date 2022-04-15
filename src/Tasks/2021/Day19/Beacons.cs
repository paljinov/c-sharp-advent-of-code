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
            (_, Dictionary<int, List<Position>> beaconsAbsolutePositions) =
                FindScannersAndBeaconsAbsolutePositions(beaconsRelativePositions);

            List<Position> beacons = FindDifferentBeacons(beaconsAbsolutePositions);

            return beacons.Count;
        }

        public int CalculateLargestManhattanDistanceBetweenAnyTwoScanners(
            Dictionary<int, List<Position>> beaconsRelativePositions
        )
        {
            (Dictionary<int, Position> scanners, _) =
                FindScannersAndBeaconsAbsolutePositions(beaconsRelativePositions);

            int largestManhattanDistance = CalculateLargestManhattanDistanceBetweenPositions(scanners.Values.ToList());

            return largestManhattanDistance;
        }

        private (Dictionary<int, Position>, Dictionary<int, List<Position>>) FindScannersAndBeaconsAbsolutePositions(
            Dictionary<int, List<Position>> beaconsRelativePositions
        )
        {
            // All "absolute" positions will be expressed relative to scanner 0
            Dictionary<int, Position> scannersAbsolutePositions = new Dictionary<int, Position>
            {
                { 0, new Position { X = 0, Y = 0, Z = 0 } }
            };

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

            while (beaconsAbsolutePositions.Count < beaconsRelativePositions.Count)
            {
                for (int scanner = 0; scanner < beaconsRelativePositions.Count; scanner++)
                {
                    if (beaconsAbsolutePositions.ContainsKey(scanner))
                    {
                        continue;
                    }

                    foreach (Rotation rotation in rotations)
                    {
                        List<Position> rotatedBeacons = new List<Position>();
                        foreach (Position beacon in beaconsRelativePositions[scanner])
                        {
                            Position rotatedBeacon = RotateBeacon(beacon, rotation);
                            rotatedBeacons.Add(rotatedBeacon);
                        }

                        Offset? offset = CalculateOffsetForWhichScannersDetectAtLeastRequiredNumberOfBeacons(
                            beaconsAbsolutePositions, rotatedBeacons);

                        if (offset.HasValue)
                        {
                            scannersAbsolutePositions[scanner] =
                                CalculateAbsolutePosition(scannersAbsolutePositions[0], offset.Value);
                            beaconsAbsolutePositions[scanner] =
                                CalculateBeaconsAbsolutePositions(rotatedBeacons, offset.Value);
                            break;
                        }
                    }

                    if (beaconsAbsolutePositions.ContainsKey(scanner))
                    {
                        break;
                    }
                }
            }

            return (scannersAbsolutePositions, beaconsAbsolutePositions);
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

        private Offset? CalculateOffsetForWhichScannersDetectAtLeastRequiredNumberOfBeacons(
            Dictionary<int, List<Position>> beaconsAbsolutePositions,
            List<Position> rotatedBeacons
        )
        {
            Dictionary<Offset, int> offsets = new Dictionary<Offset, int>();

            foreach (KeyValuePair<int, List<Position>> absolutePositions in beaconsAbsolutePositions)
            {
                foreach (Position absolutePosition in absolutePositions.Value)
                {
                    foreach (Position rotatedBeacon in rotatedBeacons)
                    {
                        Offset offset = new Offset
                        {
                            X = absolutePosition.X - rotatedBeacon.X,
                            Y = absolutePosition.Y - rotatedBeacon.Y,
                            Z = absolutePosition.Z - rotatedBeacon.Z
                        };

                        if (offsets.ContainsKey(offset))
                        {
                            offsets[offset]++;
                        }
                        else
                        {
                            offsets[offset] = 1;
                        }
                    }
                }
            }

            int occurrences = offsets.Values.Max();
            if (occurrences >= OVERLAP_BEACONS)
            {
                return offsets.FirstOrDefault(o => o.Value == occurrences).Key;
            }

            return null;
        }

        private Position CalculateAbsolutePosition(Position position, Offset offset)
        {
            return new Position
            {
                X = position.X + offset.X,
                Y = position.Y + offset.Y,
                Z = position.Z + offset.Z,
            };
        }

        private List<Position> CalculateBeaconsAbsolutePositions(List<Position> rotatedBeacons, Offset offset)
        {
            List<Position> beacons = new List<Position>();

            foreach (Position beacon in rotatedBeacons)
            {
                beacons.Add(CalculateAbsolutePosition(beacon, offset));
            }

            return beacons;
        }

        private List<Position> FindDifferentBeacons(
            Dictionary<int, List<Position>> scannersBeaconsAbsolutePositions
        )
        {
            HashSet<Position> beacons = new HashSet<Position>();

            foreach (KeyValuePair<int, List<Position>> beaconsAbsolutePositions in scannersBeaconsAbsolutePositions)
            {
                foreach (Position beacon in beaconsAbsolutePositions.Value)
                {
                    beacons.Add(beacon);
                }
            }

            return beacons.ToList();
        }

        private int CalculateLargestManhattanDistanceBetweenPositions(List<Position> positions)
        {
            int largestManhattanDistance = 0;
            foreach (Position first in positions)
            {
                foreach (Position second in positions)
                {
                    int manhattanDistance = Math.Abs(first.X - second.X)
                        + Math.Abs(first.Y - second.Y)
                        + Math.Abs(first.Z - second.Z);

                    largestManhattanDistance = Math.Max(largestManhattanDistance, manhattanDistance);
                }
            }

            return largestManhattanDistance;
        }
    }
}
