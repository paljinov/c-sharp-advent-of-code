using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day7
{
    public class IpAddressesRepository
    {
        public List<IpAddress> GetIpAddresses(string input)
        {
            List<IpAddress> ipAddresses = new List<IpAddress>();

            string[] ipAddressesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string ipAddressPattern = @"\[([a-z]+)\]";

            foreach (string ipAddressString in ipAddressesString)
            {
                string[] sequences = Regex.Split(ipAddressString, ipAddressPattern, RegexOptions.ExplicitCapture);
                MatchCollection hypernetSequences = Regex.Matches(ipAddressString, ipAddressPattern);

                ipAddresses.Add(new IpAddress
                {
                    Sequences = sequences.ToList(),
                    HypernetSequences = hypernetSequences.Cast<Match>().Select(m => m.Groups[1].Value).ToList()
                });
            }

            return ipAddresses;
        }
    }
}
