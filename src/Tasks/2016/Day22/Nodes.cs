using System.Collections.Generic;

namespace App.Tasks.Year2016.Day22
{
    public class Nodes
    {
        public int CountNodesViablePairs(List<Node> nodes)
        {
            int nodesViablePairs = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (nodes[i].Used > 0 && i != j && nodes[i].Used <= nodes[j].Available)
                    {
                        nodesViablePairs++;
                    }
                }
            }

            return nodesViablePairs;
        }
    }
}
