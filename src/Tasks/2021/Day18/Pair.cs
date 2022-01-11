namespace App.Tasks.Year2021.Day18
{
    public class Pair
    {
        public Pair Parent { get; set; }
        public Pair LeftPair { get; set; }
        public Pair RightPair { get; set; }
        public int? LeftNumber { get; set; }
        public int? RightNumber { get; set; }

        public Pair Clone()
        {
            Pair pair = new Pair
            {
                LeftNumber = LeftNumber,
                RightNumber = RightNumber
            };

            if (LeftPair != null)
            {
                pair.LeftPair = LeftPair.Clone();
                pair.LeftPair.Parent = pair;
            }
            if (RightPair != null)
            {
                pair.RightPair = RightPair.Clone();
                pair.RightPair.Parent = pair;
            }

            return pair;
        }
    }
}
