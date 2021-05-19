using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2019.Day3
{
    public class WiresPathsRepository
    {
        private const char LEFT = 'L';

        private const char RIGHT = 'R';

        private const char UP = 'U';

        private const char DOWN = 'D';

        public Dictionary<int, List<Instruction>> GetWiresPaths(string input)
        {
            Dictionary<int, List<Instruction>> wiresPaths = new Dictionary<int, List<Instruction>>();

            string[] wiresPathsArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex instructionRegex = new Regex($@"^([{LEFT}{RIGHT}{UP}{DOWN}]{{1}})(\d+)$");

            for (int i = 0; i < wiresPathsArray.Length; i++)
            {
                List<Instruction> instructions = new List<Instruction>();

                string[] instructionsArray = wiresPathsArray[i].Split(',');
                foreach (string instruction in instructionsArray)
                {
                    Match match = instructionRegex.Match(instruction);
                    GroupCollection groups = match.Groups;

                    char turn = groups[1].Value[0];
                    int steps = int.Parse(groups[2].Value);

                    TurnDirection turnDirection;
                    switch (turn)
                    {
                        case LEFT:
                            turnDirection = TurnDirection.Left;
                            break;
                        case RIGHT:
                            turnDirection = TurnDirection.Right;
                            break;
                        case UP:
                            turnDirection = TurnDirection.Up;
                            break;
                        default:
                            turnDirection = TurnDirection.Down;
                            break;
                    }

                    instructions.Add(new Instruction
                    {
                        TurnDirection = turnDirection,
                        Steps = steps
                    });
                }

                wiresPaths.Add(i + 1, instructions);
            }

            return wiresPaths;
        }
    }
}
