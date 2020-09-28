namespace App.Tasks
{
    interface ITask<T>
    {
        T Program();
    }
}
