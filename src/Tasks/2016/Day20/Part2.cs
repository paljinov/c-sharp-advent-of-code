/*
--- Part Two ---

How many IPs are allowed by the blacklist?
*/

namespace App.Tasks.Year2016.Day20
{
    public class Part2 : ITask<uint>
    {
        private readonly BlacklistRepository blacklistRepository;

        private readonly IpAddresses ipAddresses;

        public Part2()
        {
            blacklistRepository = new BlacklistRepository();
            ipAddresses = new IpAddresses();
        }

        public uint Solution(string input)
        {
            (uint, uint)[] blacklistRanges = blacklistRepository.GetBlacklistRanges(input);
            uint allowedIps = ipAddresses.CountAllowedIpAddresses(blacklistRanges);

            return allowedIps;
        }
    }
}
