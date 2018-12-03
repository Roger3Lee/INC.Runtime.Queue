using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public sealed class QueueTaskContainer : IDisposable
    {
        private ConcurrentBag<IQueueTask> queueTasks = new ConcurrentBag<IQueueTask>();
        private int maxCount;
        private object lockObject = new object();

        public QueueTaskContainer(int maxCount)
        {
            this.maxCount = maxCount;
        }

        /// <summary>
        /// Create new Task
        /// </summary>
        /// <param name="configuration"></param>
        public IQueueTask CreateNewTask(IQueueTaskConfiguration configuration)
        {
            lock (lockObject)
            {
                if (maxCount > queueTasks.Count)
                {
                    var queueTask = new QueueTask(configuration);

                    ///Add to task collection
                    queueTasks.Add(queueTask);
                    return queueTask;
                }
                else
                    return null;
            }
        }

        public bool RemomveTask(IQueueTask queueTask)
        {
            if (queueTasks.TryTake(out queueTask))
            {
                queueTask.Dispose();
                return true;
            }
            else
                return false;
        }

        public void Dispose()
        {
            foreach (var queueTask in queueTasks)
                queueTask.Dispose();

            queueTasks = null;
        }
    }
}
