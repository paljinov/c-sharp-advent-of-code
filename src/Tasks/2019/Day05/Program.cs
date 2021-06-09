namespace App.Tasks.Year2019.Day5
{
    public class Program
    {
        private const int HALT = 99;

        public int CalculateDiagnosticCode(int[] integers, int input)
        {
            int diagnosticCode = 0;

            int i = 0;
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
                            integers[firstParameter] = input;
                        }
                        else if (operation == (int)Operation.Output && integers[firstParameter] != 0)
                        {
                            diagnosticCode = integers[firstParameter];
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

            return diagnosticCode;
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
