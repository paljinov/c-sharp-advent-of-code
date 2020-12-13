using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2020.Day13
{
    public class DepartureBus
    {
        public int CalculateProductOfEarliestBusIdAndMinutesWaiting(int earliestDepartureTimestamp, List<int> busIds)
        {
            int departureBus = 0;
            int minutesWaiting = 0;

            List<int> workingBusIds = busIds.Where(busId => busId > 0).ToList();

            int timestamp = earliestDepartureTimestamp;
            while (departureBus == 0)
            {
                foreach (int busId in workingBusIds)
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
            bool wantedSequence = false;

            long timestamp = 0;
            while (wantedSequence == false)
            {
                int offset = 0;
                wantedSequence = true;
                foreach (int busId in busIds)
                {
                    if (busId != 0)
                    {
                        if ((timestamp + offset) % busId != 0)
                        {
                            wantedSequence = false;
                        }
                    }

                    offset++;
                }

                if (!wantedSequence)
                {
                    timestamp++;
                }
            }

            return timestamp;
        }
    }
}
