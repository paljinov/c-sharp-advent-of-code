using System.Linq;

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
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, false);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, false);
                        thirdParameter = GetParameter(integers, i + 3, thirdParameterMode, true);

                        integers[thirdParameter] = firstParameter + secondParameter;
                        i += 4;
                        break;
                    case (int)Operation.Multiplication:
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, false);
                        secondParameter = GetParameter(integers, i + 2, secondParameterMode, false);
                        thirdParameter = GetParameter(integers, i + 3, thirdParameterMode, true);

                        integers[thirdParameter] = firstParameter * secondParameter;
                        i += 4;
                        break;
                    case (int)Operation.Input:
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, true);
                        integers[firstParameter] = input;
                        i += 2;
                        break;
                    case (int)Operation.Output:
                        firstParameter = GetParameter(integers, i + 1, firstParameterMode, true);
                        if (integers[firstParameter] != 0)
                        {
                            diagnosticCode = integers[firstParameter];
                        }
                        i += 2;
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
