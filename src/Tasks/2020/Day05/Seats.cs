namespace App.Tasks.Year2020.Day5
{
    public class Seats
    {
        private const int TOTAL_ROWS = 128;

        private const int TOTAL_COLUMNS = 8;

        private const int ROW_MULTIPLIER = 8;

        public int[] GetSeatsIds(string[] boardingPasses)
        {
            int[] seatsIds = new int[boardingPasses.Length];

            for (int i = 0; i < boardingPasses.Length; i++)
            {
                string boardingPass = boardingPasses[i];

                string rows = boardingPass[0..^3];
                string columns = boardingPass[^3..];

                int seatRow = GetSeatRow(rows);
                int seatColumn = GetSeatColumn(columns);

                seatsIds[i] = seatRow * ROW_MULTIPLIER + seatColumn;
            }

            return seatsIds;
        }

        private int GetSeatRow(string rows)
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

        private int GetSeatColumn(string columns)
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
