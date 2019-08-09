using INC.Runtime.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace INC.Queue.Test
{
    [TestClass]
    public class QueueManagerTestClass
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

            manager.AddJob(new Job(() =>
            {
                Console.WriteLine("Hello world.");
            }));

            System.Threading.Thread.Sleep(int.MaxValue);
        }
    }
}
