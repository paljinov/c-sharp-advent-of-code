using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day23
{
    public class CupsRepository
    {
        public LinkedList<int> GetCups(string input)
        {
            LinkedList<int> cups = new LinkedList<int>();

            foreach (char digit in input)
            {
                cups.AddLast((int)char.GetNumericValue(digit));
            }

            return cups;
        }
    }
}
