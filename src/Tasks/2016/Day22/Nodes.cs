using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2016.Day22
{
    public class Nodes
    {
        private const int WALL_USED_PERCENTAGE = 80;

        private const int WALL_LARGER_THAN_MEDIAN_TIMES = 3;

        private const int MOVE_CYCLE_STEPS = 5;

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

        public int CalculateFewestNumberOfStepsRequiredToAccessGoalData(List<Node> nodes)
        {
            // Top right x
            int highestX = nodes.Max(n => n.X);
            Node emptyNode = nodes.Where(n => n.Used == 0).First();
            Node wallNode = GetFirstWallNode(nodes);

            int fewestNumberOfStepsRequiredToAccessGoalData =
                Math.Abs(wallNode.X - emptyNode.X) + 1 + Math.Abs(wallNode.Y - emptyNode.Y);
            fewestNumberOfStepsRequiredToAccessGoalData += highestX - (wallNode.X - 1) + wallNode.Y;
            fewestNumberOfStepsRequiredToAccessGoalData += MOVE_CYCLE_STEPS * (highestX - 1);

            return fewestNumberOfStepsRequiredToAccessGoalData;
        }

        private Node GetFirstWallNode(List<Node> nodes)
        {
            int medianNodeSize = GetMedianNodeSize(nodes);

            for (int i = 1; i < nodes.Count; i++)
            {
                Node node = nodes[i];

                if (IsWall(node, medianNodeSize))
                {
                    return node;
                }
            }

            return null;
        }

        private int GetMedianNodeSize(List<Node> nodes)
        {
            List<Node> sortedNodes = nodes.OrderBy(n => n.Size).ToList();
            int mid = nodes.Count / 2;

            int medianNodeSize = nodes[mid].Size;

            return medianNodeSize;
        }

        private bool IsWall(Node node, int medianNodeSize)
        {
            if (node.UsedPercentage < WALL_USED_PERCENTAGE)
            {
                return false;
            }

            if (node.Size < medianNodeSize * WALL_LARGER_THAN_MEDIAN_TIMES)
            {
                return false;
            }

            return true;
        }
    }
}
