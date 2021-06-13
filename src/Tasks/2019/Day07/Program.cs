using System;
using System.Collections.Generic;

namespace App.Tasks.Year2019.Day7
{
    public class Program
    {
        private const int PHASE_FROM = 0;

        private const int PHASE_TO = 4;

        private const int HALT = 99;

        private readonly Permutations permutations;

        public Program()
        {
            permutations = new Permutations();
        }

        public int CalculateHighestSignalThatCanBeSentToTheThrusters(int[] integers)
        {
            int highestSignalThatCanBeSentToTheThrusters = int.MinValue;

            List<List<int>> phasesPermutations = permutations.GetPermutations(PHASE_FROM, PHASE_TO);

            foreach (List<int> phasesPermutation in phasesPermutations)
            {
                int input = 0;
                foreach (int phase in phasesPermutation)
                {
                    input = CalculateOutputSignal(integers, phase, input);
                }

                highestSignalThatCanBeSentToTheThrusters = Math.Max(input, highestSignalThatCanBeSentToTheThrusters);
            }

            return highestSignalThatCanBeSentToTheThrusters;
        }

        public int CalculateOutputSignal(int[] integers, int phaseSetting, int inputSignal)
        {
            int outputSignal = 0;

            int i = 0;
            bool isPhaseSettingUsed = false;

            while (integers[i] != HALT)
            {
                // Pad first instruction with leading zeros
                string instruction = integers[i].ToString("D5");

                int operation = (int)char.GetNumericValue(instruction[^1]);
                int firstParameterMode = (int)char.GetNumericValue(instruction[2]);
                int secondParameterMode = (int)char.GetNumericValue(instruction[1]);
                int thirdParameterMode = (int)char.GetNumericValue(instruction[0]);

                int firstParameter = 0;
                int secondParameter = 0;
                int thirdParameter = 0;

                switch (operation)
                {
                    case (int)Operation.Addition:
                    case (int)Operation.Multiplication:
                    case (int)Operation.LessThan:
                    case (int)Operation.Equals:
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, false);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, false);
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
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, true);
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
                        else if (operation == (int)Operation.Output && integers[firstParameter] != 0)
                        {
                            outputSignal = integers[firstParameter];
                        }
                        break;
                    case (int)Operation.JumpIfTrue:
                    case (int)Operation.JumpIfFalse:
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, false);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, false);
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

            return outputSignal;
        }

        private int GetParameter(int[] integers, int i, int mode, bool isOutput)
        {
            int parameter;

            if (isOutput)
            {
                parameter = integers[i];
            }
            // If immediate mode
            else if (mode == 1)
            {
                parameter = integers[i];
            }
            else
            {

                parameter = integers[integers[i]];
            }

            return parameter;
        }
    }
}
