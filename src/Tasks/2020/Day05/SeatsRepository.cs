using System;

namespace App.Tasks.Year2020.Day5
{
    public class SeatsRepository
    {
        public string[] GetSeats(string input)
        {
            string[] seats = input.Split(Environment.NewLine);

            return seats;
        }
    }
}
