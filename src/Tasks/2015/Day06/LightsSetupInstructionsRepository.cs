using System;
using System.Collections.Generic;

namespace App.Tasks.Year2015.Day6
{
    public class LightsSetupInstructionsRepository
    {
        public List<LightsRectangle> GetInstructions(string input)
        {
            List<LightsRectangle> lightsSetupInstructions = new List<LightsRectangle>();

            string[] instructionsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var instructionString in instructionsString)
            {
                Instructions instruction;
                string rectangleString;

                if (instructionString.Contains("turn on"))
                {
                    rectangleString = instructionString.Replace("turn on ", "");
                    instruction = Instructions.TurnOn;
                }
                else if (instructionString.Contains("turn off"))
                {
                    rectangleString = instructionString.Replace("turn off ", "");
                    instruction = Instructions.TurnOff;
                }
                else
                {
                    rectangleString = instructionString.Replace("toggle ", "");
                    instruction = Instructions.Toggle;
                }

                string[] rectangleStringSplitted = rectangleString.Split(' ');
                string[] startPointCoordinates = rectangleStringSplitted[0].Split(',');
                string[] endPointCoordinates = rectangleStringSplitted[2].Split(',');

                LightsRectangle lightsRectangle = new LightsRectangle
                {
                    Instruction = instruction,
                    X1 = int.Parse(startPointCoordinates[0]),
                    Y1 = int.Parse(startPointCoordinates[1]),
                    X2 = int.Parse(endPointCoordinates[0]),
                    Y2 = int.Parse(endPointCoordinates[1])
                };

                lightsSetupInstructions.Add(lightsRectangle);
            }

            return lightsSetupInstructions;
        }
    }
}
