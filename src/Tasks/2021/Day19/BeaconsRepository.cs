using System;
using System.Collections.Generic;

namespace App.Tasks.Year2021.Day19
{
    public class BeaconsRepository
    {
        public Dictionary<int, List<BeaconRelativePosition>> GetBeaconsRelativePositions(string input)
        {
            string[] scanners = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            Dictionary<int, List<BeaconRelativePosition>> beaconsRelativePositions =
                new Dictionary<int, List<BeaconRelativePosition>>();

            for (int i = 0; i < scanners.Length; i++)
            {
                string[] scanner = scanners[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                List<BeaconRelativePosition> scannerBeaconsRelativePositions = new List<BeaconRelativePosition>();
                for (int j = 1; j < scanner.Length; j++)
                {
                    string[] positions = scanner[j].Split(',');
                    BeaconRelativePosition beaconRelativePosition = new BeaconRelativePosition
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
