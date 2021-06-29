using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2019.Day7
{
    public class Program
    {
        private readonly Permutations permutations;

        public Program()
        {
            permutations = new Permutations();
        }

        public int CalculateHighestSignalThatCanBeSentToTheThrusters(int[] integers, int phaseFrom, int phaseTo)
        {
            int highestSignalThatCanBeSentToTheThrusters = int.MinValue;

            List<List<int>> phasesPermutations = permutations.GetPermutations(phaseFrom, phaseTo);

            foreach (List<int> phasesPermutation in phasesPermutations)
            {
                List<(int, int[])> indexWithAmplifierIntegers = new List<(int, int[])>()
                {
                    (0, integers.ToArray()),
                    (0, integers.ToArray()),
                    (0, integers.ToArray()),
                    (0, integers.ToArray()),
                    (0, integers.ToArray())
                };

                int input = 0;
                bool halted = false;
                bool fullLoop = false;

                while (!halted)
                {
                    for (int i = 0; i < phasesPermutation.Count; i++)
                    {
                        int phaseSetting = phasesPermutation[i];
                        (int index, int[] amplifierIntegers) = indexWithAmplifierIntegers[i];

                        (input, halted) = CalculateOutputSignal(
                            amplifierIntegers, fullLoop ? input : phaseSetting, input, ref index);
                        indexWithAmplifierIntegers[i] = (index, amplifierIntegers);
                    }

                    highestSignalThatCanBeSentToTheThrusters =
                        Math.Max(input, highestSignalThatCanBeSentToTheThrusters);

                    fullLoop = true;
                }
            }

            return highestSignalThatCanBeSentToTheThrusters;
        }

        public (int, bool) CalculateOutputSignal(int[] integers, int phaseSetting, int inputSignal, ref int i)
        {
            int outputSignal = 0;

            bool isPhaseSettingUsed = false;

            while (integers[i] != (int)Operation.Halt)
            {
                if (outputSignal != 0)
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
                int firstParameter;
                int secondParameter;
                int thirdParameter;

                switch (operation)
                {
                    case (int)Operation.Addition:
                    case (int)Operation.Multiplication:
                    case (int)Operation.LessThan:
                    case (int)Operation.Equals:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        secondParameterMode = GetParameterMode(secondParameterModeDigit);
                        thirdParameterMode = GetParameterMode(thirdParameterModeDigit);
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode);
                        thirdParameter = GetParameter(integers, i + 3, thirdParameterMode, true);
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
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        firstParameter = GetParameter(
                            integers, i + 1, firstParameterMode, operation == (int)Operation.Input);
                        i += 2;

                        if (operation == (int)Operation.Input)
                        {
                            if (!isPhaseSettingUsed)
                            {
                                integers[firstParameter] = phaseSetting;
                                isPhaseSettingUsed = true;
                            }
                            else
                            {
                                integers[firstParameter] = inputSignal;
                            }
                        }
                        else if (operation == (int)Operation.Output)
                        {
                            outputSignal = firstParameter;
                        }
                        break;
                    case (int)Operation.JumpIfTrue:
                    case (int)Operation.JumpIfFalse:
                        firstParameterMode = GetParameterMode(firstParameterModeDigit);
                        secondParameterMode = GetParameterMode(secondParameterModeDigit);
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode);
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

        private ParameterMode GetParameterMode(int mode)
        {
            ParameterMode parameterMode;

            switch (mode)
            {
                case 1:
                    parameterMode = ParameterMode.ImmediateMode;
                    break;
                default:
                    parameterMode = ParameterMode.PositionMode;
                    break;
            }

            return parameterMode;
        }

        private int GetParameter(int[] integers, int i, ParameterMode mode, bool instructionWritesTo = false)
        {
            int parameter;

            if (instructionWritesTo)
            {
                return integers[i];
            }

            switch (mode)
            {
                case ParameterMode.ImmediateMode:
                    parameter = integers[i];
                    break;
                default:
                    parameter = integers[integers[i]];
                    break;
            }

            return parameter;
        }
    }
}
