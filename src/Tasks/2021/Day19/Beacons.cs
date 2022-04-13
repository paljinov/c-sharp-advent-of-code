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

                        Offset? offset = CalculateOffsetForWhichScannersDetectAtLeastRequiredNumberOfBeacons(
                            absolutePositions.Value, rotatedBeacons);

                        if (offset.HasValue)
                        {
                            beaconsAbsolutePositions[scanner] = AddOffset(rotatedBeacons, offset.Value);
                            break;
                        }
                    }

                    if (beaconsAbsolutePositions.ContainsKey(scanner))
                    {
                        break;
                    }
                }
            }

            int beaconsCount = CountDifferentBeacons(beaconsAbsolutePositions);

            return beaconsCount;
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

        private Offset? CalculateOffsetForWhichScannersDetectAtLeastRequiredNumberOfBeacons(
            List<Position> absolutePositions,
            List<Position> rotatedBeacons
        )
        {
            Dictionary<Offset, int> offsets = new Dictionary<Offset, int>();

            foreach (Position absolutePosition in absolutePositions)
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

            int occurrences = offsets.Values.Max();
            if (occurrences >= OVERLAP_BEACONS)
            {
                return offsets.FirstOrDefault(o => o.Value == occurrences).Key;
            }

            return null;
        }

        private List<Position> AddOffset(List<Position> rotatedBeacons, Offset offset)
        {
            List<Position> beacons = new List<Position>();

            foreach (Position beacon in rotatedBeacons)
            {
                beacons.Add(new Position
                {
                    X = beacon.X + offset.X,
                    Y = beacon.Y + offset.Y,
                    Z = beacon.Z + offset.Z,
                });
            }

            return beacons;
        }

        private int CountDifferentBeacons(Dictionary<int, List<Position>> scannersBeaconsAbsolutePositions)
        {
            Dictionary<Position, int> beacons = new Dictionary<Position, int>();

            foreach (KeyValuePair<int, List<Position>> beaconsAbsolutePositions in scannersBeaconsAbsolutePositions)
            {
                foreach (Position beacon in beaconsAbsolutePositions.Value)
                {
                    if (beacons.ContainsKey(beacon))
                    {
                        beacons[beacon]++;
                    }
                    else
                    {
                        beacons[beacon] = 1;
                    }
                }
            }

            return beacons.Count;
        }
    }
}
