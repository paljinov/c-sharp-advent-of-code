namespace App.Tasks.Year2017.Day16
{
    public interface IDanceMove
    {
        DanceMoveType DanceMoveType { get; set; }
    }

    public class DanceMove<T> : IDanceMove
    {
        public DanceMoveType DanceMoveType { get; set; }
        public T ValueA { get; set; }
        public T ValueB { get; set; }
    }
}
