using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2017.Day13
{
    public class Firewall
    {
        public int CalculateTripSeverity(List<FirewallLayer> firewallLayers)
        {
            Dictionary<int, int> caughtInLayers = PassThroughTheFirewall(firewallLayers);

            int tripSeverity = 0;
            foreach (KeyValuePair<int, int> caughtInLayer in caughtInLayers)
            {
                tripSeverity += caughtInLayer.Key * caughtInLayer.Value;
            }

            return tripSeverity;
        }

        public int CalculateFewestNumberOfDelayedPicosecondsToAvoidBeingCaught(List<FirewallLayer> firewallLayers)
        {
            bool passedFirewallWithoutBeingCaught = false;
            int delayedPicoseconds = 0;

            while (!passedFirewallWithoutBeingCaught)
            {
                Dictionary<int, int> caughtInLayers = PassThroughTheFirewall(firewallLayers);
                if (caughtInLayers.Count > 0)
                {
                    delayedPicoseconds++;
                }
                else
                {
                    passedFirewallWithoutBeingCaught = true;
                }
            }

            return delayedPicoseconds;
        }

        private Dictionary<int, int> PassThroughTheFirewall(List<FirewallLayer> firewallLayers, bool isPassed = false)
        {
            Dictionary<int, int> caughtInLayers = new Dictionary<int, int>();
            int totalSteps = firewallLayers.Last().Depth;

            for (int step = 0; step <= totalSteps; step++)
            {
                foreach (FirewallLayer firewallLayer in firewallLayers)
                {
                    // If packet is on this layer and scanner was at the top when packet entered
                    if (step == firewallLayer.Depth && firewallLayer.Scanner == 0)
                    {
                        caughtInLayers.Add(step, firewallLayer.Range);
                        if (isPassed)
                        {
                            return caughtInLayers;
                        }
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

            return caughtInLayers;
        }
    }
}
