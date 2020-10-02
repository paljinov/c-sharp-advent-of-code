/*
--- Part Two ---

You just finish implementing your winning light pattern when you realize you
mistranslated Santa's message from Ancient Nordic Elvish.

The light grid you bought actually has individual brightness controls; each
light can have a brightness of zero or more. The lights all start at zero.

The phrase turn on actually means that you should increase the brightness of
those lights by 1.

The phrase turn off actually means that you should decrease the brightness of
those lights by 1, to a minimum of zero.

The phrase toggle actually means that you should increase the brightness of
those lights by 2.

What is the total brightness of all lights combined after following Santa's
instructions?

For example:

- turn on 0,0 through 0,0 would increase the total brightness by 1.
- toggle 0,0 through 999,999 would increase the total brightness by 2000000.
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day6
{
    class Part2 : ITask<int>
    {
        public int Solution(string input)
        {
            int[,] lights = new int[1000, 1000];

            List<LightsRectangle> lightsSetupInstructions = LightsSetupInstructions.GetInstructions(input);
            foreach (LightsRectangle lightsRectangle in lightsSetupInstructions)
            {
                for (int i = lightsRectangle.X1; i <= lightsRectangle.X2; i++)
                {
                    for (int j = lightsRectangle.Y1; j <= lightsRectangle.Y2; j++)
                    {
                        switch (lightsRectangle.Instruction)
                        {
                            case Instructions.TurnOn:
                                lights[i, j] += 1;
                                break;
                            case Instructions.TurnOff:
                                if (lights[i, j] > 0)
                                {
                                    lights[i, j] -= 1;
                                }
                                break;
                            case Instructions.Toggle:
                                lights[i, j] += 2;
                                break;
                        }
                    }
                }
            }

            int totalBrightness = CalculateTotalBrightness(lights);

            return totalBrightness;
        }

        public int CalculateTotalBrightness(int[,] lights)
        {
            int totalBrightness = 0;

            for (int i = 0; i < lights.GetLength(0); i++)
            {
                for (int j = 0; j < lights.GetLength(1); j++)
                {
                    totalBrightness += lights[i, j];
                }
            }

            return totalBrightness;
        }
    }
}
