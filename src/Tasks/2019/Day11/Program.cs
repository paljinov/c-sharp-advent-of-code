using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2019.Day11
{
    public class Program
    {
        private const int BLACK = 0;

        private const int WHITE = 1;

        private const char BLACK_PRINT = '.';

        private const char WHITE_PRINT = '#';

        public int CountPanelsWhichArePaintAtLeastOnce(long[] integersArray)
        {
            (_, HashSet<(int, int)> totalPainted) = GetPanelGrid(integersArray, BLACK);
            return totalPainted.Count;
        }

        public string GetRegistrationIdentifierWhichIsPaintedOnHull(long[] integersArray)
        {
            (Dictionary<(int, int), int> panelsGrid, _) = GetPanelGrid(integersArray, WHITE);

            int rowMin = panelsGrid.Keys.Select(panel => panel.Item1).Min();
            int rowMax = panelsGrid.Keys.Select(panel => panel.Item1).Max();
            int columnMin = panelsGrid.Keys.Select(panel => panel.Item2).Min();
            int columnMax = panelsGrid.Keys.Select(panel => panel.Item2).Max();

            StringBuilder registrationIdentifier = new StringBuilder();

            for (int j = columnMax; j >= columnMin; j--)
            {
                registrationIdentifier.AppendLine();
                for (int i = rowMin; i <= rowMax; i++)
                {
                    if (!panelsGrid.ContainsKey((i, j)) || panelsGrid[(i, j)] == BLACK)
                    {
                        registrationIdentifier.Append(BLACK_PRINT);
                    }
                    else
                    {
                        registrationIdentifier.Append(WHITE_PRINT);
                    }
                }
            }

            return registrationIdentifier.ToString();
        }

        private (Dictionary<(int, int), int>, HashSet<(int, int)>) GetPanelGrid(
            long[] integersArray,
            int startPanelColor
            )
        {
            HashSet<(int, int)> totalPainted = new HashSet<(int, int)>();

            Dictionary<long, long> integers = InitIntegersMemory(integersArray);

            Dictionary<(int, int), int> panelsGrid = new Dictionary<(int, int), int>();
            Direction facing = Direction.Up;

            int output;
            bool halted = false;
            bool paintThePanel = true;

            int i = 0;
            int j = 0;
            panelsGrid[(i, j)] = startPanelColor;

            long index = 0;
            long relativeBase = 0;

            while (!halted)
            {
                if (!panelsGrid.ContainsKey((i, j)))
                {
                    panelsGrid[(i, j)] = BLACK;
                }

                (output, halted) = CalculateOutputSignal(integers, panelsGrid[(i, j)], ref index, ref relativeBase);

                // Halt
                if (halted)
                {
                    break;
                }
                // Paint the panel the robot is over
                else if (paintThePanel)
                {
                    if (panelsGrid[(i, j)] != output)
                    {
                        panelsGrid[(i, j)] = output;
                        totalPainted.Add((i, j));
                    }
                }
                // Turn the robot in the direction
                else
                {
                    switch (facing)
                    {
                        case Direction.Up:
                            if (output == 1)
                            {
                                facing = Direction.Right;
                                i++;
                            }
                            else
                            {
                                facing = Direction.Left;
                                i--;
                            }
                            break;
                        case Direction.Right:
                            if (output == 1)
                            {
                                facing = Direction.Down;
                                j--;
                            }
                            else
                            {
                                facing = Direction.Up;
                                j++;
                            }
                            break;
                        case Direction.Down:
                            if (output == 1)
                            {
                                facing = Direction.Left;
                                i--;
                            }
                            else
                            {
                                facing = Direction.Right;
                                i++;
                            }
                            break;
                        case Direction.Left:
                            if (output == 1)
                            {
                                facing = Direction.Up;
                                j++;
                            }
                            else
                            {
                                facing = Direction.Down;
                                j--;
                            }
                            break;
                    }
                }

                paintThePanel = !paintThePanel;
            }

            return (panelsGrid, totalPainted);
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
