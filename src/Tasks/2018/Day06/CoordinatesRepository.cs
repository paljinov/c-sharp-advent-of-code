using System;
using System.Collections.Generic;

namespace App.Tasks.Year2018.Day6
{
    public class CoordinatesRepository
    {
        public List<(int, int)> GetCoordinates(string input)
        {
            List<(int, int)> coordinates = new List<(int, int)>();

            string[] coordinatesArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string coordinateString in coordinatesArray)
            {
                string[] coordinateArray = coordinateString.Split(',');
                int x = int.Parse(coordinateArray[0].Trim());
                int y = int.Parse(coordinateArray[1].Trim());

                coordinates.Add((x, y));
            }

            return coordinates;
        }
    }
}
