using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2016.Day22
{
    public class NodesRepository
    {
        public List<Node> GetNodes(string input)
        {
            List<Node> nodes = new List<Node>();

            Regex nodeRegex = new Regex(@"^\/dev\/grid\/node\-x(\d+)\-y(\d+)\s+(\d+)T\s+(\d+)T\s+(\d+)T\s+(\d+)%$");

            string[] nodesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string nodeString in nodesString)
            {
                Match nodeMatch = nodeRegex.Match(nodeString);
                if (nodeMatch.Success)
                {
                    GroupCollection nodeGroups = nodeMatch.Groups;

                    Node node = new Node
                    {
                        X = int.Parse(nodeGroups[1].Value),
                        Y = int.Parse(nodeGroups[2].Value),
                        Size = int.Parse(nodeGroups[3].Value),
                        Used = int.Parse(nodeGroups[4].Value),
                        Available = int.Parse(nodeGroups[5].Value),
                        UsedPercentage = int.Parse(nodeGroups[6].Value)
                    };

                    nodes.Add(node);
                }
            }

            return nodes;
        }
    }
}
