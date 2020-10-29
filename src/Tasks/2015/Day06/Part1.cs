/*
--- Day 6: Probably a Fire Hazard ---

Because your neighbors keep defeating you in the holiday house decorating
contest year after year, you've decided to deploy one million lights in a
1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa has mailed you
instructions on how to display the ideal lighting configuration.

Lights in your grid are numbered from 0 to 999 in each direction; the lights at
each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include
whether to turn on, turn off, or toggle various inclusive ranges given as
coordinate pairs. Each coordinate pair represents opposite corners of a
rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to
9 lights in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up your lights by
doing the instructions Santa sent you in order.

For example:

- turn on 0,0 through 999,999 would turn on (or leave on) every light.
- toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning
  off the ones that were on, and turning on the ones that were off.
- turn off 499,499 through 500,500 would turn off (or leave off) the middle four
  lights.

After following the instructions, how many lights are lit?
*/

using System.Collections.Generic;

namespace App.Tasks.Year2015.Day6
{
    class Part1 : ITask<int>
    {
        private readonly LightsSetupInstructionsRepository lightsSetupInstructionsRepository;

        public Part1()
        {
            lightsSetupInstructionsRepository = new LightsSetupInstructionsRepository();
        }

        public int Solution(string input)
        {
            bool[,] lights = new bool[1000, 1000];

            List<LightsRectangle> lightsSetupInstructions = lightsSetupInstructionsRepository.GetInstructions(input);
            foreach (LightsRectangle lightsRectangle in lightsSetupInstructions)
            {
                for (int i = lightsRectangle.X1; i <= lightsRectangle.X2; i++)
                {
                    for (int j = lightsRectangle.Y1; j <= lightsRectangle.Y2; j++)
                    {
                        switch (lightsRectangle.Instruction)
                        {
                            case Instructions.TurnOn:
                                lights[i, j] = true;
                                break;
                            case Instructions.TurnOff:
                                lights[i, j] = false;
                                break;
                            case Instructions.Toggle:
                                lights[i, j] = !lights[i, j];
                                break;
                        }
                    }
                }
            }

            int litLights = CountLitLights(lights);

            return litLights;
        }

        public int CountLitLights(bool[,] lights)
        {
            int litLights = 0;

            for (int i = 0; i < lights.GetLength(0); i++)
            {
                for (int j = 0; j < lights.GetLength(1); j++)
                {
                    if (lights[i, j])
                    {
                        litLights++;
                    }
                }
            }

            return litLights;
        }
    }
}
