using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2021.Day22
{
    public class RebootStepsRepository
    {
        private const string TURN_ON = "on";

        public RebootStep[] GetRebootSteps(string input)
        {
            string[] rebootStepsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            RebootStep[] rebootSteps = new RebootStep[rebootStepsString.Length];

            Regex rebootStepRegex = new Regex(@"^(on|off)\s(x=-?\d+..-?\d+,y=-?\d+..-?\d+,z=-?\d+..-?\d+)$");

            for (int i = 0; i < rebootStepsString.Length; i++)
            {
                Match rebootStepMatch = rebootStepRegex.Match(rebootStepsString[i]);
                GroupCollection rebootStepGroups = rebootStepMatch.Groups;

                Action action = Action.TurnOff;
                if (rebootStepGroups[1].Value == TURN_ON)
                {
                    action = Action.TurnOn;
                }

                Cuboid cuboid = GetCuboid(rebootStepGroups[2].Value);

                RebootStep rebootStep = new RebootStep
                {
                    Action = action,
                    Cuboid = cuboid
                };

                rebootSteps[i] = rebootStep;
            }

            return rebootSteps;
        }

        public Cuboid GetCuboid(string input)
        {
            Regex cuboidRegex = new Regex(@"^x=(-?\d+)..(-?\d+),y=(-?\d+)..(-?\d+),z=(-?\d+)..(-?\d+)$");
            Match cuboidMatch = cuboidRegex.Match(input);
            GroupCollection cuboidGroups = cuboidMatch.Groups;

            Cuboid cuboid = new Cuboid
            {
                X = (int.Parse(cuboidGroups[1].Value), int.Parse(cuboidGroups[2].Value)),
                Y = (int.Parse(cuboidGroups[3].Value), int.Parse(cuboidGroups[4].Value)),
                Z = (int.Parse(cuboidGroups[5].Value), int.Parse(cuboidGroups[6].Value)),
            };

            return cuboid;
        }
    }
}
