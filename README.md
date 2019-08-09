# INC.Queue

Implementent a queue basic on .net framwork C# language

You can used it like this:

```C#
var manager = new QueueManager(new QueueConfirguration(2, 10000, 1), QueueTaskMode.Task, new JobPriorityScheduleConfig(DateTime.Now, new TimeSpan(0, 1, 0)));
manager.Start();

for (int i = 0; i < 20; i++)
{
    manager.AddJob(new Job<int>(i, (index) =>
    {
        Console.WriteLine($"Thread id:{System.Threading.Thread.CurrentThread.ManagedThreadId}    SeqNo.：{index}");
    }));
}
Console.WriteLine($"Main Thread ID:{System.Threading.Thread.CurrentThread.ManagedThreadId}");
Console.ReadLine();
```
