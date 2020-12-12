using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day12
{
    public class ShipNavigationInstructionsRepository
    {
        public List<NavigationInstruction> GetNavigationInstructions(string input)
        {
            List<NavigationInstruction> navigationInstructions = new List<NavigationInstruction>();

            string[] navigationInstructionsString = input.Split(Environment.NewLine);

            Regex navigationInstructionsRegex = new Regex(@"^([NSEWLRF])(\d+)$");

            foreach (string navigationInstructionString in navigationInstructionsString)
            {
                Match match = navigationInstructionsRegex.Match(navigationInstructionString);
                GroupCollection groups = match.Groups;

                char actionLetter = char.Parse(groups[1].Value);
                int value = int.Parse(groups[2].Value);

                Action action = Action.MoveForward;
                switch (actionLetter)
                {
                    case 'N':
                        action = Action.MoveNorth;
                        break;
                    case 'S':
                        action = Action.MoveSouth;
                        break;
                    case 'E':
                        action = Action.MoveEast;
                        break;
                    case 'W':
                        action = Action.MoveWest;
                        break;
                    case 'L':
                        action = Action.TurnLeft;
                        break;
                    case 'R':
                        action = Action.TurnRight;
                        break;
                }

                navigationInstructions.Add(new NavigationInstruction
                {
                    Action = action,
                    Value = value
                });
            }

            return navigationInstructions;
        }
    }
}
