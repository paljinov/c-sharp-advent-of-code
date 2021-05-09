namespace App.Tasks.Year2018.Day11
{
    public class FuelCells
    {
        private const int GRID_SQUARE_LENGTH = 300;

        private const int ADD_TO_RACK_ID = 10;

        private const int SUBTRACT_FROM_FUEL_CELL_POWER_LEVEL = 5;

        public (int, int) FindTopLeftFuelCellCoordinateOfThe3x3SquareWithLargestTotalPower(int gridSerialNumber)
        {
            int[,] grid = new int[GRID_SQUARE_LENGTH, GRID_SQUARE_LENGTH];
            for (int i = 0; i < GRID_SQUARE_LENGTH; i++)
            {
                for (int j = 0; j < GRID_SQUARE_LENGTH; j++)
                {
                    grid[i, j] = CalculateFuelCellPowerLevel(i + 1, j + 1, gridSerialNumber);
                }
            }

            (int x, int y) = FindTopLeftFuelCellCoordinateOfThe3x3SquareWithLargestTotalPowerForGrid(grid);

            return (x, y);
        }

        private int CalculateFuelCellPowerLevel(int x, int y, int gridSerialNumber)
        {
            int rackId = x + ADD_TO_RACK_ID;
            int fuelCellPowerLevel = rackId * y;
            fuelCellPowerLevel += gridSerialNumber;
            fuelCellPowerLevel *= rackId;

            string fuelCellPowerLevelString = $"{fuelCellPowerLevel}";
            int hundredsDigit = 0;
            if (fuelCellPowerLevelString.Length >= 3)
            {
                hundredsDigit = (int)char.GetNumericValue(fuelCellPowerLevelString[^3]);
            }

            fuelCellPowerLevel = hundredsDigit - SUBTRACT_FROM_FUEL_CELL_POWER_LEVEL;

            return fuelCellPowerLevel;
        }

        private (int x, int y) FindTopLeftFuelCellCoordinateOfThe3x3SquareWithLargestTotalPowerForGrid(int[,] grid)
        {
            int x = 0;
            int y = 0;
            int largestTotalPower = 0;

            for (int i = 0; i < GRID_SQUARE_LENGTH - 3; i++)
            {
                for (int j = 0; j < GRID_SQUARE_LENGTH - 3; j++)
                {
                    int totalPower = 0;

                    for (int k = i; k < i + 3; k++)
                    {
                        for (int h = j; h < j + 3; h++)
                        {
                            totalPower += grid[k, h];
                        }
                    }

                    if (totalPower > largestTotalPower)
                    {
                        largestTotalPower = totalPower;
                        // x and y start from 1
                        x = i + 1;
                        y = j + 1;
                    }
                }
            }

            return (x, y);
        }
    }
}
