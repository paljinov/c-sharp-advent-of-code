using System.Collections.Generic;

namespace App.Tasks.Year2015.Day6
{
    public class Lights
    {
        private const int X = 1000;

        private const int Y = 1000;

        public int CountLitLights(List<LightsRectangle> lightsSetupInstructions)
        {
            bool[,] lights = new bool[X, Y];

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

            int litLights = GetLitLights(lights);

            return litLights;
        }

        public int CalculateTotalBrightnes(List<LightsRectangle> lightsSetupInstructions)
        {
            int[,] lights = new int[X, Y];

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

            int totalBrightness = GetTotalBrightness(lights);

            return totalBrightness;
        }

        public int GetLitLights(bool[,] lights)
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

        public int GetTotalBrightness(int[,] lights)
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
