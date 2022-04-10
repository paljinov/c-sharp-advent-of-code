using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2021.Day19
{
    public class Beacons
    {
        private const int OVERLAP_BEACONS = 12;

        public int CountBeacons(Dictionary<int, List<BeaconRelativePosition>> scannersBeaconsRelativePositions)
        {
            HashSet<(int x, int y, int z)> beacons = new HashSet<(int x, int y, int z)>();

            List<List<int>> rotations = GetRotations();

            foreach (KeyValuePair<int, List<BeaconRelativePosition>> scannerBeaconsRelativePositions
                in scannersBeaconsRelativePositions)
            {
                foreach (BeaconRelativePosition beaconsRelativePositions in scannerBeaconsRelativePositions.Value)
                {
                    foreach (List<int> rotation in rotations)
                    {
                        (int x, int y, int z) beacon = Rotate(
                            beaconsRelativePositions.X,
                            beaconsRelativePositions.Y,
                            beaconsRelativePositions.Z,
                            rotation[0],
                            rotation[1],
                            rotation[2]
                        );

                        beacons.Add(beacon);
                    }
                }
            }

            return scannersBeaconsRelativePositions.Count;
        }

        public int CalculateLargestManhattanDistanceBetweenAnyTwoScanners(
            Dictionary<int, List<BeaconRelativePosition>> beaconsRelativePositions
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
                    a => angles.Where(o => !a.Contains(o)),
                    (a1, a2) => a1.Concat(new int[] { a2 })
                );
        }

        private (int X, int Y, int Z) Rotate(int x, int y, int z, int angleX, int angleY, int angleZ)
        {
            // Rotate vector around the x axis
            y = y * (int)Math.Cos(angleX) - z * (int)Math.Sin(angleX);
            z = y * (int)Math.Sin(angleX) + z * (int)Math.Cos(angleX);

            // Rotate vector around the y axis
            z = z * (int)Math.Cos(angleY) - x * (int)Math.Sin(angleY);
            y = z * (int)Math.Sin(angleY) + x * (int)Math.Cos(angleY);

            // Rotate vector around the z axis
            x = x * (int)Math.Cos(angleZ) - y * (int)Math.Sin(angleZ);
            y = x * (int)Math.Sin(angleZ) + y * (int)Math.Cos(angleZ);

            return (x, y, z);
        }
    }
}
