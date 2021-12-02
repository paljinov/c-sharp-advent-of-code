namespace App.Tasks.Year2021.Day2
{
    public class Dive
    {
        public int CalculateProductOfFinalHorizontalPositionAndDept(Instruction[] instructions)
        {
            int horizontalPosition = 0;
            int depth = 0;

            foreach (Instruction instruction in instructions)
            {
                if (instruction.Command == Command.Forward)
                {
                    horizontalPosition += instruction.Value;
                }
                else if (instruction.Command == Command.Down)
                {
                    depth += instruction.Value;
                }
                else
                {
                    depth -= instruction.Value;
                }
            }

            int productOfFinalHorizontalPositionAndDept = horizontalPosition * depth;

            return productOfFinalHorizontalPositionAndDept;
        }
    }
}
