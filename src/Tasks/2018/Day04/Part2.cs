/*
--- Part Two ---

Strategy 2: Of all guards, which guard is most frequently asleep on the same
minute?

In the example above, Guard #99 spent minute 45 asleep more than any other guard
or minute - three times in total. (In all other cases, any guard spent any
minute asleep at most twice.)

What is the ID of the guard you chose multiplied by the minute you chose? (In
the above example, the answer would be 99 * 45 = 4455.)
*/

using System;
using System.Collections.Generic;

namespace App.Tasks.Year2018.Day4
{
    public class Part2 : ITask<int>
    {
        private readonly GuardsRecordsRepository guardsRecordsRepository;

        private readonly Guards guards;

        public Part2()
        {
            guardsRecordsRepository = new GuardsRecordsRepository();
            guards = new Guards();
        }

        public int Solution(string input)
        {
            Dictionary<DateTime, Record> guardsRecords = guardsRecordsRepository.GetGuardsRecords(input);
            int product = guards.CalculateGuardIdAndAsleepMinuteProductForSecondStrategy(guardsRecords);

            return product;
        }
    }
}
