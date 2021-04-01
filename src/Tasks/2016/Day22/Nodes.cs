using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Tasks.Year2016.Day22
{
    public class Nodes
    {
        private const int WALL_USED_PERCENTAGE = 80;

        private const int WALL_LARGER_THAN_MEDIAN_TIMES = 3;

        private const char ORDINARY_NODE = '.';

        private const char EMPTY_NODE = '_';

        private const char WALL_NODE = '#';

        private const char GOAL_NODE = 'G';

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
            int steps = 0;

            int highestX = nodes.Max(n => n.X);
            Node emptyNode = nodes.Where(n => n.Used == 0).First();
            Node goalNode = nodes.Where(n => n.X == highestX && n.Y == 0).First();

            (int x, int y) empty = (emptyNode.X, emptyNode.Y);
            (int x, int y) goal = (goalNode.X, goalNode.Y);

            char[,] nodesGrid = GetNodesGrid(nodes);

            PrintGrid(nodesGrid);
            Console.WriteLine($"Steps: {steps}");
            while (nodesGrid[0, 0] != 'G')
            {
                // Goal higher and more right than empty
                if (goal.y < empty.y && goal.x > empty.x)
                {
                    empty = (empty.x, empty.y - 1);
                    nodesGrid[empty.x, empty.y + 1] = ORDINARY_NODE;
                    nodesGrid[empty.x, empty.y] = EMPTY_NODE;
                }
                // Goal more right than empty
                else if (goal.x > empty.x)
                {
                    empty = (empty.x + 1, empty.y);
                    if (empty.x == goal.x && empty.y == goal.y)
                    {
                        nodesGrid[empty.x - 1, empty.y] = GOAL_NODE;
                        goal = (empty.x - 1, empty.y);
                    }
                    else
                    {
                        nodesGrid[empty.x - 1, empty.y] = ORDINARY_NODE;
                    }

                    nodesGrid[empty.x, empty.y] = EMPTY_NODE;
                }
                // Goal more or equally left than empty
                else if (goal.x <= empty.x)
                {
                    // If on same height
                    if (empty.y == goal.y)
                    {
                        empty = (empty.x, empty.y + 1);
                        nodesGrid[empty.x, empty.y - 1] = ORDINARY_NODE;
                        nodesGrid[empty.x, empty.y] = EMPTY_NODE;
                    }
                    // If not on same height
                    else
                    {
                        empty = (empty.x - 1, empty.y);
                        if (empty.x == goal.x && empty.y == goal.y)
                        {
                            nodesGrid[empty.x + 1, empty.y] = GOAL_NODE;
                            goal = (empty.x + 1, empty.y);
                        }
                        else
                        {
                            nodesGrid[empty.x + 1, empty.y] = ORDINARY_NODE;
                        }

                        nodesGrid[empty.x, empty.y] = EMPTY_NODE;
                    }
                }

                steps++;

                PrintGrid(nodesGrid);
                Console.WriteLine($"Steps: {steps}");
            }

            return steps;
        }

        private void PrintGrid(char[,] nodesGrid)
        {
            int previous = 0;

            StringBuilder grid = new StringBuilder();

            for (int j = 0; j < nodesGrid.GetLength(1); j++)
            {
                for (int i = 0; i < nodesGrid.GetLength(0); i++)
                {
                    if (previous != j)
                    {
                        grid.Append(Environment.NewLine);
                        previous = j;
                    }

                    grid.Append(nodesGrid[i, j]);

                }
            }

            Console.WriteLine(grid.ToString());
        }

        private char[,] GetNodesGrid(List<Node> nodes)
        {
            int highestX = nodes.Max(n => n.X);
            int highestY = nodes.Max(n => n.Y);

            char[,] nodesGrid = new char[highestX + 1, highestY + 1];
            int medianNodeSize = GetMedianNodeSize(nodes);

            foreach (Node node in nodes)
            {
                if (node.Used == 0)
                {
                    nodesGrid[node.X, node.Y] = EMPTY_NODE;
                }
                else if (node.X == highestX && node.Y == 0)
                {
                    nodesGrid[node.X, node.Y] = GOAL_NODE;
                }
                else if (IsWall(node, medianNodeSize))
                {
                    nodesGrid[node.X, node.Y] = WALL_NODE;
                }
                else
                {
                    nodesGrid[node.X, node.Y] = ORDINARY_NODE;
                }
            }

            return nodesGrid;
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
