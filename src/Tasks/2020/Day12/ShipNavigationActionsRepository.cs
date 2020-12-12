using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2020.Day12
{
    public class ShipNavigationActionsRepository
    {
        public List<Action> GetActions(string input)
        {
            List<Action> actions = new List<Action>();

            string[] actionsString = input.Split(Environment.NewLine);

            Regex actionsRegex = new Regex(@"^([NSEWLRF])(\d+)$");

            foreach (string actionString in actionsString)
            {
                Match match = actionsRegex.Match(actionString);
                GroupCollection groups = match.Groups;

                char directionLetter = char.Parse(groups[1].Value);
                int value = int.Parse(groups[2].Value);

                Direction direction = Direction.Forward;
                switch (directionLetter)
                {
                    case 'N':
                        direction = Direction.North;
                        break;
                    case 'S':
                        direction = Direction.South;
                        break;
                    case 'E':
                        direction = Direction.East;
                        break;
                    case 'W':
                        direction = Direction.West;
                        break;
                    case 'L':
                        direction = Direction.Left;
                        break;
                    case 'R':
                        direction = Direction.Right;
                        break;
                }

                actions.Add(new Action
                {
                    Direction = direction,
                    Value = value
                });
            }

            return actions;
        }
    }
}
