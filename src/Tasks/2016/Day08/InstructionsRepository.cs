using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day8
{
    public class InstructionsRepository
    {
        public List<RectangleInstructions> GetInstructions(string input)
        {
            List<RectangleInstructions> instructions = new List<RectangleInstructions>();

            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Regex rectangleDimensionsRegex = new Regex(@"rect\s(\d+)x(\d+)");
            Regex rotateAxisRegex = new Regex(@"rotate\s(?:row|column)\s(x|y)=(\d+)\sby\s(\d+)");

            RectangleInstructions rectangleInstructions = new RectangleInstructions
            {
                Rectangle = new Rectangle
                {
                    X = 0,
                    Y = 0
                },
                AxesRotations = new List<Axis>()
            };

            foreach (string instructionString in instructionsString)
            {
                Match rectangleDimensionsMatch = rectangleDimensionsRegex.Match(instructionString);
                if (rectangleDimensionsMatch.Success)
                {
                    rectangleInstructions = new RectangleInstructions
                    {
                        Rectangle = new Rectangle
                        {
                            X = int.Parse(rectangleDimensionsMatch.Groups[2].Value),
                            Y = int.Parse(rectangleDimensionsMatch.Groups[1].Value),
                        },
                        AxesRotations = new List<Axis>()
                    };

                    instructions.Add(rectangleInstructions);
                }
                else
                {
                    Match rotateAxisMatch = rotateAxisRegex.Match(instructionString);

                    Axis axis = new Axis
                    {
                        Name = rotateAxisMatch.Groups[1].Value[0],
                        Value = int.Parse(rotateAxisMatch.Groups[2].Value),
                        RotateBy = int.Parse(rotateAxisMatch.Groups[3].Value),
                    };

                    rectangleInstructions.AxesRotations.Add(axis);
                }
            }

            return instructions;
        }
    }
}
