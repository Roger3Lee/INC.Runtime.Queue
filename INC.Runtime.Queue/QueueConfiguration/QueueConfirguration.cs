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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskMaxCount">Thread count</param>
        /// <param name="taskDelay">thread free sleep time</param>
        /// <param name="delayTimes">sleep times</param>
		public QueueConfirguration(int taskMaxCount, int taskDelay, int delayTimes)
		{
			this.taskMaxCount = taskMaxCount;
			this.taskDelay = taskDelay;
			this.delayTimes = delayTimes;
		}

		public int TaskMaxCount { get { return this.taskMaxCount; } }

		public int TaskDelay { get { return this.taskDelay; } }

		public int DelayTimes { get { return this.delayTimes; } }
	}
}
