using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Queue
{
    internal class QueueTaskConfiguration:IQueueTaskConfiguration
    {
        /// <summary>
        /// complete job delay time uint is ms
        /// </summary>
        public int TaskDelay { get; set; }

        /// <summary>
        /// complete job delay time 
        /// </summary>
        public int DelayTimes { get; set; }


        public static IQueueTaskConfiguration GetConfiguration(IQueueConfirguration queueConfirguration)
        {
            return new QueueTaskConfiguration()
            {
                DelayTimes = queueConfirguration.DelayTimes,
                TaskDelay = queueConfirguration.TaskDelay
            };
        }
    }
}
