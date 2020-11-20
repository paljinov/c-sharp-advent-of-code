using System;
using System.Collections.Generic;

namespace App.Tasks.Year2015.Day2
{
    public class BoxesRepository
    {
        public static List<Box> GetBoxes(string input)
        {
            List<Box> boxes = new List<Box>();

            string[] boxesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string boxString in boxesString)
            {
                string[] box = boxString.Split('x');
                boxes.Add(new Box { Length = int.Parse(box[0]), Width = int.Parse(box[1]), Height = int.Parse(box[2]) });
            }

            return boxes;
        }
    }
}
