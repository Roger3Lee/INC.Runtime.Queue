using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public class DefaultQueueConfirguration : IQueueConfirguration
    {
        public int TaskMaxCount { get => 4; }
        public int TaskDelay { get => 100; }
        public int DelayTimes { get => 1; }
    }
}
