using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public interface IQueueTaskConfiguration
    {
        int TaskDelay { get; set; }

        /// <summary>
        /// complete job delay time 
        /// </summary>
        int DelayTimes { get; set; }
    }
}
