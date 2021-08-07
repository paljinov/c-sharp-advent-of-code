namespace App.Tasks.Year2019.Day10
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool HasCoordinates(int x, int y)
        {
            if (X == x && Y == y)
            {
                return true;
            }

            return false;
        }
    }
}
