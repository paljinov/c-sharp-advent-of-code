using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day20
{
    public class IpAddresses
    {
        private readonly (uint Start, uint End) ipAddressesRange = (0, 4294967295);

        public uint FindLowestValuedIpThatIsNotBlocked((uint, uint)[] blacklistRanges)
        {
            for (uint ipAddress = ipAddressesRange.Start; ipAddress <= ipAddressesRange.End; ipAddress++)
            {
                bool isBlacklisted = false;
                foreach ((uint start, uint end) in blacklistRanges)
                {
                    if (ipAddress >= start && ipAddress <= end)
                    {
                        isBlacklisted = true;
                        break;
                    }
                }

                if (!isBlacklisted)
                {
                    return ipAddress;
                }
            }

            return ipAddressesRange.End;
        }
    }
}
