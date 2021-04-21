using System;
using System.Collections.Generic;

namespace App.Tasks.Year2017.Day24
{
    public class Bridges
    {
        public int CalculateStrongestBridgeStrength(Component[] components)
        {
            List<HashSet<Component>> bridges = new List<HashSet<Component>>();
            FindBridgesPermutations(components, 0, new HashSet<Component>(), bridges);

            int strongestBridge = 0;
            foreach (HashSet<Component> bridge in bridges)
            {
                int bridgeStrength = 0;
                foreach (Component component in bridge)
                {
                    bridgeStrength += component.Port1 + component.Port2;
                }

                strongestBridge = Math.Max(strongestBridge, bridgeStrength);
            }

            return strongestBridge;
        }

        public void FindBridgesPermutations(
            Component[] components,
            int currentPort,
            HashSet<Component> currentBridge,
            List<HashSet<Component>> bridges
        )
        {
            foreach (Component component in components)
            {
                if (!currentBridge.Contains(component)
                    && (component.Port1 == currentPort || component.Port2 == currentPort))
                {
                    HashSet<Component> extendedBridge = new HashSet<Component>(currentBridge)
                    {
                        component
                    };

                    bridges.Add(extendedBridge);

                    if (component.Port1 == currentPort)
                    {
                        FindBridgesPermutations(components, component.Port2, extendedBridge, bridges);
                    }
                    else
                    {
                        FindBridgesPermutations(components, component.Port1, extendedBridge, bridges);
                    }
                }
            }
        }
    }
}
