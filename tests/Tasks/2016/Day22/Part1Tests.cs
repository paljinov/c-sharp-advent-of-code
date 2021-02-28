using System;
using App.Tasks.Year2016.Day22;
using Xunit;

namespace Tests.Tasks.Year2016.Day22
{
    public class Part1Tests
    {
        private readonly Part1 task;

        public Part1Tests()
        {
            task = new Part1();
        }

        [Fact]
        public void Solution_NodesExample_NodesViablePairsCountEquals()
        {
            string nodes = $"{Environment.NewLine}root@ebhq-gridcenter# df -h"
                + $"{Environment.NewLine}Filesystem            Size  Used  Avail  Use%"
                + $"{Environment.NewLine}/dev/grid/node-x0-y0   10T    8T     2T   80%"
                + $"{Environment.NewLine}/dev/grid/node-x0-y1   11T    6T     5T   54%"
                + $"{Environment.NewLine}/dev/grid/node-x0-y2   32T   28T     4T   87%"
                + $"{Environment.NewLine}/dev/grid/node-x1-y0    9T    7T     2T   77%"
                + $"{Environment.NewLine}/dev/grid/node-x1-y1    8T    0T     8T    0%"
                + $"{Environment.NewLine}/dev/grid/node-x1-y2   11T    7T     4T   63%"
                + $"{Environment.NewLine}/dev/grid/node-x2-y0   10T    6T     4T   60%"
                + $"{Environment.NewLine}/dev/grid/node-x2-y1    9T    8T     1T   88%"
                + $"{Environment.NewLine}/dev/grid/node-x2-y2    9T    6T     3T   66%";

            Assert.Equal(7, task.Solution(nodes));
        }
    }
}
