using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public sealed class QueueTaskContainer : IDisposable
    {
        private ConcurrentBag<IQueueTask> _queueTasks = new ConcurrentBag<IQueueTask>();
        private int _maxCount;
        private readonly QueueTaskMode _queueTaskMode;
        private readonly object _lockObject = new object();

        public QueueTaskContainer(int maxCount, QueueTaskMode mode)
        {
            this._maxCount = maxCount;
            this._queueTaskMode = mode;
        }

        /// <summary>
        /// Create new Task
        /// </summary>
        /// <param name="configuration"></param>
        public IQueueTask CreateNewTask(IQueueTaskConfiguration configuration)
        {
            lock (_lockObject)
            {
                if (_maxCount > _queueTasks.Count)
                {
                    var queueTask = new QueueTask(configuration, _queueTaskMode);

                    ///Add to task collection
                    _queueTasks.Add(queueTask);
                    return queueTask;
                }
                else
                    return null;
            }
        }

        public bool RemomveTask(IQueueTask queueTask)
        {
            if (_queueTasks.TryTake(out queueTask))
            {
                queueTask.Dispose();
                return true;
            }
            else
                return false;
        }

        public void Dispose()
        {
            foreach (var queueTask in _queueTasks)
                queueTask.Dispose();

            _queueTasks = null;
        }
    }
}
