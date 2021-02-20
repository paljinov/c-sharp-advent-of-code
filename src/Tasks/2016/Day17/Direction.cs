namespace App.Tasks.Year2016.Day17
{
    public readonly struct Direction
    {
        public static readonly (int Index, char Letter) Up = (0, 'U');
        public static readonly (int Index, char Letter) Down = (1, 'D');
        public static readonly (int Index, char Letter) Left = (2, 'L');
        public static readonly (int Index, char Letter) Right = (3, 'R');
    }
}
