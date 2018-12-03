using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public interface IQueueConfirguration
    {
        /// <summary>
        /// The Max task count
        /// </summary>
        int TaskMaxCount { get; }

        /// <summary>
        /// complete job delay time uint is ms
        /// </summary>
        int TaskDelay { get; }

        /// <summary>
        /// complete job delay time 
        /// </summary>
        int DelayTimes { get; }
    }
}
