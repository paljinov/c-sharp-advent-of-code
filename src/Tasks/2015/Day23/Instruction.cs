using System;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2015.Day23
{
    public class Instruction
    {
        public InstructionType InstructionType { get; set; }
        public char? Register { get; set; }
        public int? Offset { get; set; }

    }
}
