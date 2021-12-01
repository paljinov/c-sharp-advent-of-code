namespace App.Tasks.Year2021.Day1
{
    public class Measurements
    {
        public int CountMeasurementsWhichAreLargerThanThePreviousMeasurement(int[] depths)
        {
            int measurementsWhichAreLargerThanThePreviousMeasurement = 0;

            for (int i = 0; i < depths.Length - 1; i++)
            {
                if (depths[i + 1] > depths[i])
                {
                    measurementsWhichAreLargerThanThePreviousMeasurement++;
                }
            }

            return measurementsWhichAreLargerThanThePreviousMeasurement;
        }

        public int CountThreeMeasurementSlidingWindowSumsWhichAreLargerThanThePreviousSum(int[] depths)
        {
            int threeMeasurementSlidingWindowSumsWhichAreLargerThanThePreviousSum = 0;

            for (int i = 0; i < depths.Length - 3; i++)
            {
                int previousSum = depths[i] + depths[i + 1] + depths[i + 2];
                int sum = depths[i + 1] + depths[i + 2] + depths[i + 3];

                if (sum > previousSum)
                {
                    threeMeasurementSlidingWindowSumsWhichAreLargerThanThePreviousSum++;
                }
            }

            return threeMeasurementSlidingWindowSumsWhichAreLargerThanThePreviousSum;
        }
    }
}
