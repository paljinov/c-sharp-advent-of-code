using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day13
{
    public class DepartureBus
    {
        private readonly ChineseReminderTheorem chineseReminderTheorem;

        public DepartureBus()
        {
            chineseReminderTheorem = new ChineseReminderTheorem();
        }

        public int CalculateProductOfEarliestBusIdAndMinutesWaiting(int earliestDepartureTimestamp, List<int> busIds)
        {
            int departureBus = 0;
            int minutesWaiting = 0;

            int[] busesInService = busIds.Where(busId => busId > 0).ToArray();

            int timestamp = earliestDepartureTimestamp;
            while (departureBus == 0)
            {
                foreach (int busId in busesInService)
                {
                    if (timestamp % busId == 0)
                    {
                        departureBus = busId;
                        minutesWaiting = timestamp - earliestDepartureTimestamp;
                        break;
                    }
                }

                timestamp++;
            }

            return departureBus * minutesWaiting;
        }

        public long CalculateEarliestTimestampForWhichAllBusesDepartAtOffsetsMatchingTheirPositions(List<int> busIds)
        {
            int[] busesInService = busIds.Where(busId => busId > 0).ToArray();
            int[] busIdAndOffsetDiffs = new int[busesInService.Length];

            int j = 0;
            for (int i = 0; i < busIds.Count; i++)
            {
                int busId = busIds[i];
                if (busId > 0)
                {
                    busIdAndOffsetDiffs[j] = busId - i;
                    j++;
                }
            }

            long timestamp = chineseReminderTheorem.Algorithm(
                busesInService.Select(bus => (long)bus).ToArray(),
                busIdAndOffsetDiffs.Select(diff => (long)diff).ToArray()
            );

            return timestamp;
        }
    }
}
