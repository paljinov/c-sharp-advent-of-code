using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day19
{
    public class BeaconsRepository
    {
        public Dictionary<int, List<Position>> GetBeaconsRelativePositions(string input)
        {
            string[] scanners = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            Dictionary<int, List<Position>> beaconsRelativePositions =
                new Dictionary<int, List<Position>>();

            for (int i = 0; i < scanners.Length; i++)
            {
                string[] scanner = scanners[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                List<Position> scannerBeaconsRelativePositions = new List<Position>();
                for (int j = 1; j < scanner.Length; j++)
                {
                    string[] positions = scanner[j].Split(',');
                    Position beaconRelativePosition = new Position
                    {
                        X = int.Parse(positions[0]),
                        Y = int.Parse(positions[1]),
                        Z = int.Parse(positions[2])
                    };
                    scannerBeaconsRelativePositions.Add(beaconRelativePosition);
                }

                beaconsRelativePositions.Add(i, scannerBeaconsRelativePositions);
            }

            return beaconsRelativePositions;
        }
    }
}
