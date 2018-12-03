using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public enum QueueState
    {
        New = 0,

        Starting = 1,

        Started = 2,

        Stopped = 3
    }
}
