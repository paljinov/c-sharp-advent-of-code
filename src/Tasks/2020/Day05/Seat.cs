using System;

namespace App.Tasks.Year2020.Day5
{
    public class Seat
    {
        private const int TOTAL_ROWS = 128;

        private const int TOTAL_COLUMNS = 8;

        private const int ROW_MULTIPLIER = 8;

        public int[] GetSeatsIds(string[] seats)
        {
            int[] seatsIds = new int[seats.Length];

            for (int i = 0; i < seats.Length; i++)
            {
                string seat = seats[i];

                string rows = seat[0..^3];
                string columns = seat[^3..];

                int row = GetRow(rows);
                int column = GetColumn(columns);

                seatsIds[i] = row * ROW_MULTIPLIER + column;
            }

            return seatsIds;
        }

        private int GetRow(string rows)
        {
            int start = 0;
            int end = TOTAL_ROWS - 1;

            foreach (char half in rows)
            {
                int middle = (start + end + 1) / 2;

                if (half == 'F')
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle;
                }
            }

            return start;
        }

        private int GetColumn(string columns)
        {
            int start = 0;
            int end = TOTAL_COLUMNS - 1;

            foreach (char half in columns)
            {
                int middle = (start + end + 1) / 2;

                if (half == 'L')
                {
                    end = middle - 1;
                }
                else
                {
                    start = middle;
                }
            }

            return end;
        }
    }
}
