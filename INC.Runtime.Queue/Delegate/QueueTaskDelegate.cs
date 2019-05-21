using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue.Delegate
{
	public static class QueueTaskDelegate
	{
		public delegate void TaskBeginEventHander(object sender, EventArgs args);

		public delegate void TaskJobBeginEventHander(object sender, QueueTaskEventArgs args);

		public delegate void TaskJobCompleteEventHander(object sender, QueueTaskEventArgs args);

		public delegate bool TaskWakeUpEventHander(object sender, EventArgs args);

		public delegate void TaskCompleteEventHander(object sender, EventArgs args);
	}

	public class QueueTaskEventArgs : EventArgs
	{
		public QueueTaskEventArgs(JobBase job)
		{
			this.Job = job;
		}

		public JobBase Job { get; private set; }
	}
}
