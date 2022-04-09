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
            int[] angles = new int[] { 0, 90, 180, 270 };

            List<List<int>> rotations = FindRotations(angles.ToList(), 0, angles.Length, 3).ToList();

            return rotations;
        }

        public IEnumerable<List<int>> FindRotations(List<int> angles, int start, int count, int choose)
        {
            if (choose == 0)
            {
                yield return angles;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    IEnumerable<List<int>> rotations = FindRotations(angles, start + 1, count - 1 - i, choose - 1);
                    foreach (List<int> combination in rotations)
                    {
                        yield return combination;
                    }

                    RotateLeft(angles, start, count);
                }
            }
        }

        private void RotateLeft(List<int> angles, int start, int count)
        {
            int tmp = angles[start];
            angles.RemoveAt(start);
            angles.Insert(start + count - 1, tmp);
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
