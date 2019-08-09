using INC.Runtime.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace INC.Queue.Test
{
    [TestClass]
    public class UnitTest1
    {
        public static int count = 0;
        [TestMethod]
        public void TestMethod1()
        {
            var count = 0;
            var task = new Task(() =>
            {
                count += 1;
            });
            task.Start();
            task.ContinueWith((t) =>
            {
                count += 1;
            }, TaskContinuationOptions.OnlyOnRanToCompletion| TaskContinuationOptions.AttachedToParent);


            System.Threading.Thread.Sleep(10000);

            Assert.Equals(count, 2);
        }

        [TestMethod]
        public void QueueManagerTest()
        {
            var manager = new QueueManager(new QueueConfirguration(5, 10000, 1), QueueTaskMode.Task, new JobPriorityScheduleConfig(DateTime.Now, new TimeSpan(0, 1, 0)));
            manager.Start();

            Parallel.For(0, 100, (i) =>
            {
                for (int j = 0; j < 10; j++)
                {
                    manager.AddJob(new Job(() =>
                    {
                        System.Threading.Interlocked.Increment(ref count);

                        var sr = System.IO.File.AppendText("C://1.txt");
                        sr.Write($"Task id{System.Threading.Thread.CurrentThread.ManagedThreadId}_{count}_{System.Environment.NewLine}");
                        sr.Flush();
                        sr.Dispose();
                    }));
                }
            });

            Parallel.For(0, 100, (i) =>
           {
               for (int j = 0; j < 10; j++)
               {
                   System.Threading.Thread.Sleep(new Random(10).Next(100));
                   manager.AddJob(new Job(() =>
                   {
                       System.Threading.Interlocked.Increment(ref count);

                       var sr = System.IO.File.AppendText("C://1.txt");
                       sr.Write($"Task id{System.Threading.Thread.CurrentThread.ManagedThreadId}_{count}_{System.Environment.NewLine}");
                       sr.Flush();
                       sr.Dispose();
                   }));
               }
           });

            System.Threading.Thread.Sleep(int.MaxValue);
        }
    }
}
