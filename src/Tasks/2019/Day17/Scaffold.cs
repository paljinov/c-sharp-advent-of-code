using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2019.Day17
{
    public class Scaffold
    {
        private const char SCAFFOLD = '#';

        private const char OPEN_SPACE = '.';

        private const char UP = '^';

        private const char DOWN = 'v';

        private const char LEFT = '<';

        private const char RIGHT = '>';

        private const int MOVEMENT_FUNCTIONS_MAX_CHARACTERS = 20;

        public int CalculateSumOfTheAlignmentParametersForTheScaffoldIntersections(long[] integersArray)
        {
            int sumOfTheAlignmentParameters = 0;

            char[,] image = GetImage(integersArray);

            for (int i = 1; i < image.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < image.GetLength(1) - 1; j++)
                {
                    // If this is scaffold intersection
                    if (IsScaffoldIntersection(i, j, image))
                    {
                        int alignmentParameter = i * j;
                        sumOfTheAlignmentParameters += alignmentParameter;
                    }
                }
            }

            return sumOfTheAlignmentParameters;
        }

        public int CalculateDustCollectedByTheVacuumRobot(long[] integersArray, int addressZeroValue)
        {
            int dust = 0;

            integersArray[0] = addressZeroValue;

            char[,] image = GetImage(integersArray);
            PrintImage(image);

            string path = FindVacuumRobotPath(image);
            Console.WriteLine(path);

            return dust;
        }

        private char[,] GetImage(long[] integersArray)
        {
            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            List<int> outputs = new List<int>();
            int outputSignal;
            bool halted = false;

            // IntCode program current index and relative base value
            long index = 0;
            long relativeBase = 0;

            int i = 0;
            int j = 0;

            while (!halted)
            {
                (outputSignal, halted) = CalculateOutputSignal(integers, 0, ref index, ref relativeBase);
                outputs.Add(outputSignal);
            }

            Dictionary<(int, int), char> imageDictionary = new Dictionary<(int, int), char>();
            foreach (int output in outputs)
            {
                if (output == (int)CameraOutput.NewLine)
                {
                    i++;
                    j = 0;
                }
                else
                {
                    if (output == (int)CameraOutput.Scaffold)
                    {
                        imageDictionary[(i, j)] = SCAFFOLD;
                    }
                    else if (output == (int)CameraOutput.OpenSpace)
                    {
                        imageDictionary[(i, j)] = OPEN_SPACE;
                    }
                    else
                    {
                        switch (output)
                        {
                            case UP:
                                imageDictionary[(i, j)] = UP;
                                break;
                            case DOWN:
                                imageDictionary[(i, j)] = DOWN;
                                break;
                            case LEFT:
                                imageDictionary[(i, j)] = LEFT;
                                break;
                            case RIGHT:
                                imageDictionary[(i, j)] = RIGHT;
                                break;
                        }
                    }

                    j++;
                }
            }

            int rows = imageDictionary.Keys.Select(p => p.Item1).Max() + 1;
            int columns = imageDictionary.Keys.Select(p => p.Item2).Max() + 1;
            char[,] image = new char[rows, columns];

            foreach (KeyValuePair<(int, int), char> pixel in imageDictionary)
            {
                image[pixel.Key.Item1, pixel.Key.Item2] = pixel.Value;
            }

            return image;
        }

        private bool IsScaffoldIntersection(int i, int j, char[,] image)
        {
            for (int k = i - 1; k <= i + 1; k++)
            {
                if (k < 0 || k >= image.GetLength(0) || image[k, j] != SCAFFOLD)
                {
                    return false;
                }
            }

            for (int h = j - 1; h <= j + 1; h++)
            {
                if (h < 0 || h >= image.GetLength(1) || image[i, h] != SCAFFOLD)
                {
                    return false;
                }
            }

            return true;
        }

        private string FindVacuumRobotPath(char[,] image)
        {
            StringBuilder path = new StringBuilder();

            VacuumRobot vacuumRobot = GetInitialVacuumRobotLocation(image);

            bool moved = true;
            Direction facing = vacuumRobot.Facing;
            int steps = 0;

            while (moved)
            {
                moved = MoveVacuumRobot(vacuumRobot, image);
                if (!moved)
                {
                    break;
                }

                // If direction changed
                if (facing != vacuumRobot.Facing)
                {
                    if (steps > 0)
                    {
                        path.Append($",{steps},");
                    }

                    switch (facing)
                    {
                        case Direction.Up:
                            if (vacuumRobot.Facing == Direction.Left)
                            {
                                path.Append('L');
                            }
                            else if (vacuumRobot.Facing == Direction.Right)
                            {
                                path.Append('R');
                            }
                            break;
                        case Direction.Down:
                            if (vacuumRobot.Facing == Direction.Left)
                            {
                                path.Append('R');
                            }
                            else if (vacuumRobot.Facing == Direction.Right)
                            {
                                path.Append('L');
                            }
                            break;
                        case Direction.Left:
                            if (vacuumRobot.Facing == Direction.Up)
                            {
                                path.Append('R');
                            }
                            else if (vacuumRobot.Facing == Direction.Down)
                            {
                                path.Append('L');
                            }
                            break;
                        case Direction.Right:
                            if (vacuumRobot.Facing == Direction.Up)
                            {
                                path.Append('L');
                            }
                            else if (vacuumRobot.Facing == Direction.Down)
                            {
                                path.Append('R');
                            }
                            break;
                    }

                    steps = 1;
                    facing = vacuumRobot.Facing;
                }
                // If direction stayed the same
                else
                {
                    steps++;
                }
            }
            path.Append($",{steps}");

            return path.ToString();
        }

        private VacuumRobot GetInitialVacuumRobotLocation(char[,] image)
        {
            VacuumRobot vacuumRobot = null;
            Direction facing = Direction.Up;
            bool isFound = false;

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    switch (image[i, j])
                    {
                        case UP:
                            facing = Direction.Up;
                            isFound = true;
                            break;
                        case DOWN:
                            facing = Direction.Down;
                            isFound = true;
                            break;
                        case LEFT:
                            facing = Direction.Left;
                            isFound = true;
                            break;
                        case RIGHT:
                            facing = Direction.Right;
                            isFound = true;
                            break;
                    }

                    if (isFound)
                    {
                        vacuumRobot = new VacuumRobot
                        {
                            X = i,
                            Y = j,
                            Facing = facing
                        };

                        return vacuumRobot;
                    }
                }
            }

            return vacuumRobot;
        }

        private bool MoveVacuumRobot(VacuumRobot vacuumRobot, char[,] image)
        {
            int i = vacuumRobot.X;
            int j = vacuumRobot.Y;

            // Try to continue without turning
            switch (vacuumRobot.Facing)
            {
                case Direction.Up:
                    if (i - 1 >= 0 && image[i - 1, j] == '#')
                    {
                        vacuumRobot.X--;
                        return true;
                    }
                    break;
                case Direction.Down:
                    if (i + 1 < image.GetLength(0) && image[i + 1, j] == '#')
                    {
                        vacuumRobot.X++;
                        return true;
                    }
                    break;
                case Direction.Left:
                    if (j - 1 >= 0 && image[i, j - 1] == '#')
                    {
                        vacuumRobot.Y--;
                        return true;
                    }
                    break;
                case Direction.Right:
                    if (j + 1 < image.GetLength(1) && image[i, j + 1] == '#')
                    {
                        vacuumRobot.Y++;
                        return true;
                    }
                    break;
            }

            // If current direction is left or right
            if (vacuumRobot.Facing == Direction.Left || vacuumRobot.Facing == Direction.Right)
            {
                if (i - 1 >= 0 && image[i - 1, j] == '#')
                {
                    vacuumRobot.X--;
                    vacuumRobot.Facing = Direction.Up;
                    return true;
                }
                if (i + 1 < image.GetLength(0) && image[i + 1, j] == '#')
                {
                    vacuumRobot.X++;
                    vacuumRobot.Facing = Direction.Down;
                    return true;
                }
            }
            // If current direction is up or down
            else
            {
                if (j - 1 >= 0 && image[i, j - 1] == '#')
                {
                    vacuumRobot.Y--;
                    vacuumRobot.Facing = Direction.Left;
                    return true;
                }
                if (j + 1 < image.GetLength(1) && image[i, j + 1] == '#')
                {
                    vacuumRobot.Y++;
                    vacuumRobot.Facing = Direction.Right;
                    return true;
                }
            }

            // End of the path
            return false;
        }

        private void PrintImage(char[,] image)
        {
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Console.Write(image[i, j]);
                }

                Console.WriteLine();
            }
        }

        private (int, bool) CalculateOutputSignal(
            Dictionary<long, long> integers,
            int input,
            ref long i,
            ref long relativeBase
        )
        {
            int outputSignal = -1;

            while (integers[i] != (int)Operation.Halt)
            {
                if (outputSignal != -1)
                {
                    return (outputSignal, false);
                }

                // Pad first instruction with leading zeros
                string instruction = integers[i].ToString("D5");

                int operation = (int)char.GetNumericValue(instruction[^1]);
                int firstParameterModeDigit = (int)char.GetNumericValue(instruction[2]);
                int secondParameterModeDigit = (int)char.GetNumericValue(instruction[1]);
                int thirdParameterModeDigit = (int)char.GetNumericValue(instruction[0]);

                ParameterMode firstParameterMode;
                ParameterMode secondParameterMode;
                ParameterMode thirdParameterMode;
                long firstParameter;
                long secondParameter;
                long thirdParameter;

                switch (operation)
                {
                    case (int)Operation.Addition:
                    case (int)Operation.Multiplication:
                    case (int)Operation.LessThan:
                    case (int)Operation.Equals:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        secondParameterMode = GetParameterMode(secondParameterModeDigit);
                        thirdParameterMode = GetParameterMode(thirdParameterModeDigit);
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, relativeBase);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, relativeBase);
                        thirdParameter = GetParameter(integers, i + 3, thirdParameterMode, relativeBase, true);
                        i += 4;

                        if (operation == (int)Operation.Addition)
                        {
                            integers[thirdParameter] = firstParameter + secondParameter;
                        }
                        else if (operation == (int)Operation.Multiplication)
                        {
                            integers[thirdParameter] = firstParameter * secondParameter;
                        }
                        else if (operation == (int)Operation.LessThan)
                        {
                            integers[thirdParameter] = 0;
                            if (firstParameter < secondParameter)
                            {
                                integers[thirdParameter] = 1;
                            }
                        }
                        else if (operation == (int)Operation.Equals)
                        {
                            integers[thirdParameter] = 0;
                            if (firstParameter == secondParameter)
                            {
                                integers[thirdParameter] = 1;
                            }
                        }
                        break;
                    case (int)Operation.Input:
                    case (int)Operation.Output:
                    case (int)Operation.RelativeBaseOffset:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        firstParameter = GetParameter(
                            integers, i + 1, firstParameterMode, relativeBase, operation == (int)Operation.Input);
                        i += 2;

                        if (operation == (int)Operation.Input)
                        {
                            integers[firstParameter] = input;
                        }
                        else if (operation == (int)Operation.Output)
                        {
                            outputSignal = (int)firstParameter;
                        }
                        else if (operation == (int)Operation.RelativeBaseOffset)
                        {
                            relativeBase += firstParameter;
                        }
                        break;
                    case (int)Operation.JumpIfTrue:
                    case (int)Operation.JumpIfFalse:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        secondParameterMode = GetParameterMode(secondParameterModeDigit);
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, relativeBase);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, relativeBase);
                        i += 3;

                        if (operation == (int)Operation.JumpIfTrue)
                        {
                            if (firstParameter != 0)
                            {
                                i = secondParameter;
                            }
                        }
                        else if (operation == (int)Operation.JumpIfFalse)
                        {
                            if (firstParameter == 0)
                            {
                                i = secondParameter;
                            }
                        }
                        break;
                }
            }

            return (outputSignal, true);
        }

        private Dictionary<long, long> InitIntegersMemory(long[] integersArray)
        {
            Dictionary<long, long> integers = new Dictionary<long, long>();
            for (int i = 0; i < integersArray.Length; i++)
            {
                integers.Add(i, integersArray[i]);
            }

            return integers;
        }

        private ParameterMode GetParameterMode(int mode)
        {
            ParameterMode parameterMode;

            switch (mode)
            {
                case 1:
                    parameterMode = ParameterMode.ImmediateMode;
                    break;
                case 2:
                    parameterMode = ParameterMode.RelativeMode;
                    break;
                default:
                    parameterMode = ParameterMode.PositionMode;
                    break;
            }

            return parameterMode;
        }

        private long GetParameter(
            Dictionary<long, long> integers,
            long i,
            ParameterMode mode,
            long relativeBase,
            bool instructionWritesTo = false
        )
        {
            long parameter;

            if (instructionWritesTo)
            {
                if (mode == ParameterMode.RelativeMode)
                {
                    return relativeBase + integers[i];
                }
                else
                {
                    return integers[i];
                }
            }

            switch (mode)
            {
                case ParameterMode.ImmediateMode:
                    parameter = integers[i];
                    break;
                case ParameterMode.RelativeMode:
                    parameter = integers.ContainsKey(relativeBase + integers[i])
                        ? integers[relativeBase + integers[i]] : 0;
                    break;
                default:
                    parameter = integers.ContainsKey(integers[i]) ? integers[integers[i]] : 0;
                    break;
            }

            return parameter;
        }
    }
}
