using System;
using System.Collections.Generic;

namespace App.Tasks.Year2019.Day6
{
    public class OrbitsRepository
    {
        public List<(string, string)> GetLocalOrbits(string input)
        {
            List<(string, string)> localOrbits = new List<(string, string)>();

            string[] localOrbitsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < localOrbitsString.Length; i++)
            {
                string[] localOrbit = localOrbitsString[i].Split(')');
                localOrbits.Add((localOrbit[0], localOrbit[1]));
            }

            return localOrbits;
        }
    }
}
