using System.Collections.Generic;

namespace App.Tasks.Year2019.Day23
{
    public class Computer
    {
        public Dictionary<long, long> Integers { get; set; }
        public Queue<int> Inputs { get; set; }
        public long Index { get; set; }
        public long RelativeBase { get; set; }
    }
}
