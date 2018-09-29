# INC.Queue

An implement queue basic on .net framwork C# language

Used it like that:

<code>
var manager = new QueueManager(new QueueConfirguration(5, 10000, 1));
<br>
manager.Start();
<br>
manager.AddJob(new Job(() =>{Console.WriteLine(....);}))
<br>
</code>
