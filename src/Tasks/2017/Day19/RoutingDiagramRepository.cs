using System;
using System.Linq;

namespace App.Tasks.Year2017.Day19
{
    public class RoutingDiagramRepository
    {
        public char[,] GetRoutingDiagram(string input)
        {
            string[] routingDiagramString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int rows = routingDiagramString.Length;
            int columns = routingDiagramString.OrderByDescending(s => s.Length).First().Length;

            char[,] routingDiagram = new char[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    routingDiagram[i, j] = ' ';
                }
            }

            for (int i = 0; i < routingDiagramString.Length; i++)
            {
                for (int j = 0; j < routingDiagramString[i].Length; j++)
                {
                    routingDiagram[i, j] = routingDiagramString[i][j];
                }
            }

            return routingDiagram;
        }
    }
}
