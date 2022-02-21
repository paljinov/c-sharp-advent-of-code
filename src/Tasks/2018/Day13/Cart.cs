namespace App.Tasks.Year2018.Day13
{
    public struct Cart
    {
        public (int X, int Y) Location { get; set; }
        public Facing Facing { get; set; }
        public IntersectionTurnOption IntersectionTurnOption { get; set; }
    }
}
