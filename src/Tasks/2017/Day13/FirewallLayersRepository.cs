using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2017.Day13
{
    public class FirewallLayersRepository
    {
        public List<FirewallLayer> GetFirewallLayers(string input)
        {
            List<FirewallLayer> firewallLayers = new List<FirewallLayer>();

            string[] firewallLayersString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Regex firewallLayerRegex = new Regex(@"^(\d+):\s(\d+)$");

            foreach (string firewallLayerString in firewallLayersString)
            {
                Match firewallLayerMatch = firewallLayerRegex.Match(firewallLayerString);
                GroupCollection firewallLayerGroups = firewallLayerMatch.Groups;

                FirewallLayer firewallLayer = new FirewallLayer
                {
                    Depth = int.Parse(firewallLayerGroups[1].Value),
                    Range = int.Parse(firewallLayerGroups[2].Value),
                    Scanner = 0,
                    IsMovingDown = true
                };


                firewallLayers.Add(firewallLayer);
            }

            return firewallLayers;
        }
    }
}
