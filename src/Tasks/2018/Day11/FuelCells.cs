namespace App.Tasks.Year2018.Day11
{
    public class FuelCells
    {
        private const int GRID_SQUARE_LENGTH = 300;

        private const int ADD_TO_RACK_ID = 10;

        private const int SUBTRACT_FROM_FUEL_CELL_POWER_LEVEL = 5;

        public (int x, int y) FindTopLeftFuelCellCoordinateOfThe3x3SquareWithLargestTotalPower(int gridSerialNumber)
        {
            int[,] grid = CalculateGridFuelCellsPowerLevels(gridSerialNumber);
            int squareSize = 3;

            (int x, int y, _) = FindTopLeftFuelCellCoordinateOfTheSquareWithLargestTotalPower(grid, squareSize);

            return (x, y);
        }

        public (int x, int y, int squareSize) FindIdentifierOfTheSquareWithTheLargestTotalPower(int gridSerialNumber)
        {
            int x = 1;
            int y = 1;
            int largestTotalPower = 0;
            int largestTotalPowerSquareSize = 1;

            int[,] grid = CalculateGridFuelCellsPowerLevels(gridSerialNumber);
            for (int squareSize = 1; squareSize <= GRID_SQUARE_LENGTH; squareSize++)
            {
                (int i, int j, int totalPower) =
                    FindTopLeftFuelCellCoordinateOfTheSquareWithLargestTotalPower(grid, squareSize);

                if (totalPower > largestTotalPower)
                {
                    x = i;
                    y = j;
                    largestTotalPower = totalPower;
                    largestTotalPowerSquareSize = squareSize;
                }
            }

            return (x, y, largestTotalPowerSquareSize);
        }

        private int[,] CalculateGridFuelCellsPowerLevels(int gridSerialNumber)
        {
            int[,] grid = new int[GRID_SQUARE_LENGTH, GRID_SQUARE_LENGTH];
            for (int i = 0; i < GRID_SQUARE_LENGTH; i++)
            {
                for (int j = 0; j < GRID_SQUARE_LENGTH; j++)
                {
                    grid[i, j] = CalculateFuelCellPowerLevel(i + 1, j + 1, gridSerialNumber);
                }
            }

            return grid;
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

        private (int x, int y, int largestTotalPower) FindTopLeftFuelCellCoordinateOfTheSquareWithLargestTotalPower(
            int[,] grid,
            int squareSize
        )
        {
            int x = 1;
            int y = 1;
            int largestTotalPower = 0;

            for (int i = 0; i <= GRID_SQUARE_LENGTH - squareSize; i++)
            {
                for (int j = 0; j <= GRID_SQUARE_LENGTH - squareSize; j++)
                {
                    int totalPower = 0;

                    for (int k = i; k < i + squareSize; k++)
                    {
                        for (int h = j; h < j + squareSize; h++)
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

            return (x, y, largestTotalPower);
        }
    }
}
