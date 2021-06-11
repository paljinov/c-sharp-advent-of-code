using System;
using System.Collections.Generic;

namespace App.Tasks.Year2019.Day6
{
    public class OrbitsRepository
    {
        public List<(string, string)> GetOrbits(string input)
        {
            List<(string, string)> orbits = new List<(string, string)>();

            string[] orbitsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < orbitsString.Length; i++)
            {
                string[] orbit = orbitsString[i].Split(')');
                orbits.Add((orbit[0], orbit[1]));
            }

            return orbits;
        }
    }
}
