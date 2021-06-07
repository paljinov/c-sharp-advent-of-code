namespace App.Tasks.Year2015.Day25
{
    public class MachineCode
    {
        private const int FIRST_CODE = 20151125;

        private const int MULTIPLIER = 252533;

        private const int DIVIDER = 33554393;

        public long GetCode(int machineCodeRow, int machineCodeColumn)
        {
            int row = 1;
            int column = 1;
            long code = FIRST_CODE;

            while (!(row == machineCodeRow && column == machineCodeColumn))
            {
                if (row == 1)
                {
                    row = column + 1;
                    column = 1;
                }
                else
                {
                    row -= 1;
                    column += 1;
                }

                code = (code * MULTIPLIER) % DIVIDER;
            }

            return code;
        }
    }
}
