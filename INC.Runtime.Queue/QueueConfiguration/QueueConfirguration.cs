using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public class QueueConfirguration : IQueueConfirguration
    {
        private int taskMaxCount;
        private int taskDelay;
        private int delayTimes;

        public QueueConfirguration(int taskMaxCount, int taskDelay, int delayTimes)
        {
            this.taskMaxCount = taskMaxCount;
            this.taskDelay = taskDelay;
            this.delayTimes = delayTimes;
        }

        public int TaskMaxCount => this.taskMaxCount;

        public int TaskDelay => this.taskDelay;

        public int DelayTimes => this.delayTimes;
    }
}
