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
    }
}
