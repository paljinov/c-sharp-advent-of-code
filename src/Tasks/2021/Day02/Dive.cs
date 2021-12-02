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

        public int CalculateProductOfFinalHorizontalPositionAndDeptWhenTrackingAim(Instruction[] instructions)
        {
            int horizontalPosition = 0;
            int depth = 0;
            int aim = 0;

            foreach (Instruction instruction in instructions)
            {
                if (instruction.Command == Command.Forward)
                {
                    horizontalPosition += instruction.Value;
                    depth += aim * instruction.Value;
                }
                else if (instruction.Command == Command.Down)
                {
                    aim += instruction.Value;
                }
                else
                {
                    aim -= instruction.Value;
                }
            }

            int productOfFinalHorizontalPositionAndDeptWhenTrackingAim = horizontalPosition * depth;

            return productOfFinalHorizontalPositionAndDeptWhenTrackingAim;
        }
    }
}
