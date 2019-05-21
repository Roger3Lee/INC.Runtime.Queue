using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
	public class DefaultQueueConfirguration : IQueueConfirguration
	{
		public int TaskMaxCount { get { return 4; } }
		public int TaskDelay { get { return 100; } }
		public int DelayTimes { get { return 1; } }
	}
}
