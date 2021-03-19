using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day13
{
    public class Firewall
    {
        public int CalculateTripSeverity(List<FirewallLayer> firewallLayers)
        {
            Dictionary<int, int> caughtInLayers = new Dictionary<int, int>();

            for (int picosecond = 0; picosecond <= firewallLayers.Last().Depth; picosecond++)
            {
                foreach (FirewallLayer firewallLayer in firewallLayers)
                {
                    // If packet is on this layer and scanner was at the top when packet entered
                    if (picosecond == firewallLayer.Depth && firewallLayer.Scanner == 0)
                    {
                        caughtInLayers.Add(picosecond, firewallLayer.Range);
                    }

                    if (firewallLayer.IsMovingDown)
                    {
                        firewallLayer.Scanner++;
                        if (firewallLayer.Scanner == firewallLayer.Range - 1)
                        {
                            firewallLayer.IsMovingDown = false;
                        }
                    }
                    else
                    {
                        firewallLayer.Scanner--;
                        if (firewallLayer.Scanner == 0)
                        {
                            firewallLayer.IsMovingDown = true;
                        }
                    }
                }
            }

            int tripSeverity = 0;
            foreach (KeyValuePair<int, int> caughtInLayer in caughtInLayers)
            {
                tripSeverity += caughtInLayer.Key * caughtInLayer.Value;
            }

            return tripSeverity;
        }
    }
}
