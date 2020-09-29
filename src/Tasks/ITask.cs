namespace App.Tasks
{
    interface ITask<T>
    {
        T Solution(string input);
    }
}
