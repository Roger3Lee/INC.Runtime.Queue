using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    /// <summary>
    /// 调高任务优先级的调度
    /// </summary>
    public class JobPriorityScheduleConfig : IJobPriorityScheduleConfig
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"> from date</param>
        /// <param name="interval">ms</param>
        public JobPriorityScheduleConfig(DateTime from, TimeSpan interval)
        {
            this.FromDateTime = from;
            this.Interval = interval.TotalMilliseconds;
        }

        public DateTime FromDateTime { get; private set; }

        public double Interval { get; private set; }

        public double GetTimeInterval()
        {
            var intervalms = 0;
            if ((intervalms = DateTime.Now.Subtract(this.FromDateTime).Milliseconds) >= 0)
            {
                return Interval-( intervalms % Interval);
            }
            else
            {
                return Math.Abs(intervalms);
            }
        }
    }
}
