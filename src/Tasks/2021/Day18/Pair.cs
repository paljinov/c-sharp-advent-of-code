namespace App.Tasks.Year2021.Day18
{
    public class Pair
    {
        public Pair Parent { get; set; }
        public Pair LeftPair { get; set; }
        public Pair RightPair { get; set; }
        public int? LeftNumber { get; set; }
        public int? RightNumber { get; set; }
    }
}
