namespace App.Tasks
{
    public interface ITask<T>
    {
        T Solution(string input);
    }
}
