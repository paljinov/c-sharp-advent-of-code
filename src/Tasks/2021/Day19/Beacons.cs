using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day19
{
    public class Beacons
    {
        private const int OVERLAP_BEACONS = 12;

        public int CountBeacons(Dictionary<int, List<Position>> scannersBeaconsRelativePositions)
        {
            Dictionary<int, List<Position>> scannersBeaconsAbsolutePositions = new Dictionary<int, List<Position>>
            {
                [0] = scannersBeaconsRelativePositions[0]
            };

            List<List<int>> rotations = GetRotations();

            for (int scanner = 1; scanner < scannersBeaconsRelativePositions.Count; scanner++)
            {
                foreach (KeyValuePair<int, List<Position>> absolutePositions in scannersBeaconsAbsolutePositions)
                {
                    foreach (List<int> rotation in rotations)
                    {
                        List<Position> rotatedBeacons = new List<Position>();
                        foreach (Position beaconsRelativePositions in scannersBeaconsRelativePositions[scanner])
                        {
                            Position beacon = Rotate(beaconsRelativePositions, rotation);
                            rotatedBeacons.Add(beacon);
                        }

                        if (CountSameBeacons(absolutePositions.Value, rotatedBeacons) >= OVERLAP_BEACONS)
                        {
                            scannersBeaconsAbsolutePositions[scanner] = rotatedBeacons;
                            break;
                        }
                    }

                    if (scannersBeaconsAbsolutePositions.ContainsKey(scanner))
                    {
                        break;
                    }
                }
            }

            return scannersBeaconsRelativePositions.Count;
        }

        public int CalculateLargestManhattanDistanceBetweenAnyTwoScanners(
            Dictionary<int, List<Position>> beaconsRelativePositions
        )
        {
            return beaconsRelativePositions.Count;
        }

        private List<List<int>> GetRotations()
        {
            List<int> angles = new List<int> { 0, 90, 180, 270 };
            IEnumerable<IEnumerable<int>> rotations = FindRotationsPermutations(angles, 3).ToList();

            return rotations.Select(r => r.ToList()).ToList();
        }

        private IEnumerable<IEnumerable<int>> FindRotationsPermutations(IEnumerable<int> angles, int length)
        {
            if (length == 1)
            {
                return angles.Select(a => new int[] { a });
            }

            return FindRotationsPermutations(angles, length - 1)
                .SelectMany(
                    a => angles,
                    (a1, a2) => a1.Concat(new int[] { a2 })
                );
        }

        private Position Rotate(Position position, List<int> rotation)
        {
            int x = position.X;
            int y = position.Y;
            int z = position.Z;

            int angleX = rotation[0];
            int angleY = rotation[1];
            int angleZ = rotation[2];

            // Rotate vector around the x axis
            y = y * (int)Math.Cos(angleX) - z * (int)Math.Sin(angleX);
            z = y * (int)Math.Sin(angleX) + z * (int)Math.Cos(angleX);

            // Rotate vector around the y axis
            z = z * (int)Math.Cos(angleY) - x * (int)Math.Sin(angleY);
            x = z * (int)Math.Sin(angleY) + x * (int)Math.Cos(angleY);

            // Rotate vector around the z axis
            x = x * (int)Math.Cos(angleZ) - y * (int)Math.Sin(angleZ);
            y = x * (int)Math.Sin(angleZ) + y * (int)Math.Cos(angleZ);

            return new Position
            {
                X = x,
                Y = y,
                Z = z
            };
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
