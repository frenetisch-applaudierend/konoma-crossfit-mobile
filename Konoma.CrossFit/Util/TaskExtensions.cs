using System;
using System.Threading.Tasks;

namespace Konoma.CrossFit
{
    public static class TaskExtensions
    {
        public static void FireAndForget(this Task task)
        {
            task.ContinueWith(
                t =>
                {
                    if (t.IsFaulted)
                        Console.Error.WriteLine(
                            $"FireAndForget Task {task.GetType()} faulted with exception {task.Exception}");
                });
        }
    }
}
