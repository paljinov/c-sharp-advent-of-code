using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day20
{
    public class IpAddresses
    {
        private readonly (uint Start, uint End) ipAddressesRange = (0, 4294967295);

        public uint FindLowestValuedIpAddressThatIsNotBlocked((uint, uint)[] blacklistRanges)
        {
            uint lowestValuedIpAddressThatIsNotBlocked;

            List<(uint, uint)> normalizedBlacklistRanges = GetNormalizedSortedBlacklistRanges(blacklistRanges);

            if (normalizedBlacklistRanges[0].Item1 - ipAddressesRange.Start > 0)
            {
                lowestValuedIpAddressThatIsNotBlocked = 0;
            }
            else
            {
                lowestValuedIpAddressThatIsNotBlocked = normalizedBlacklistRanges[0].Item2 + 1;
            }

            return lowestValuedIpAddressThatIsNotBlocked;
        }

        public uint CountAllowedIpAddresses((uint, uint)[] blacklistRanges)
        {
            uint allowedIps = 0;

            List<(uint, uint)> normalizedBlacklistRanges = GetNormalizedSortedBlacklistRanges(blacklistRanges);
            for (int i = 0; i < normalizedBlacklistRanges.Count - 1; i++)
            {
                allowedIps += normalizedBlacklistRanges[i + 1].Item1 - 1 - normalizedBlacklistRanges[i].Item2;
            }

            return allowedIps;
        }

        private List<(uint, uint)> GetNormalizedSortedBlacklistRanges((uint, uint)[] blacklistRanges)
        {
            List<(uint, uint)> normalizedBlacklistRanges = new List<(uint, uint)>();

            List<(uint, uint)> blacklistRangesWithRemovedSubsets = new List<(uint, uint)>();

            // Remove ranges which are subset of larger ranges
            for (int i = 0; i < blacklistRanges.Length; i++)
            {
                bool isSubset = false;
                for (int j = 0; j < blacklistRanges.Length; j++)
                {
                    if (i != j && blacklistRanges[j].Item1 < blacklistRanges[i].Item1
                        && blacklistRanges[j].Item2 > blacklistRanges[i].Item2)
                    {
                        isSubset = true;
                        break;
                    }
                }

                if (!isSubset)
                {
                    blacklistRangesWithRemovedSubsets.Add((blacklistRanges[i].Item1, blacklistRanges[i].Item2));
                }
            }

            // Sort by range start
            blacklistRangesWithRemovedSubsets.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            // Connect ranges if possible
            uint blacklistRangeStart = blacklistRangesWithRemovedSubsets[0].Item1;
            for (int i = 0; i < blacklistRangesWithRemovedSubsets.Count - 1; i++)
            {
                if (blacklistRangesWithRemovedSubsets[i].Item2 + 1 < blacklistRangesWithRemovedSubsets[i + 1].Item1)
                {
                    normalizedBlacklistRanges.Add((blacklistRangeStart, blacklistRangesWithRemovedSubsets[i].Item2));
                    blacklistRangeStart = blacklistRangesWithRemovedSubsets[i + 1].Item1;
                }

                // Last blacklist range
                if (i == blacklistRangesWithRemovedSubsets.Count - 2)
                {
                    normalizedBlacklistRanges
                        .Add((blacklistRangeStart, blacklistRangesWithRemovedSubsets[i + 1].Item2));
                }
            }

            return normalizedBlacklistRanges;
        }
    }
}
