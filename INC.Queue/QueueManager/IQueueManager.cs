using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Queue
{
    interface IQueueManager
    {
        QueueState State { get; }

        void AddJob(JobBase job);

        void Start();

        void Stop();
    }
}
