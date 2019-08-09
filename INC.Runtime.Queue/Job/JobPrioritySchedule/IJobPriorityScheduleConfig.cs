using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INC.Runtime.Queue
{
    /// <summary>
    /// 调高任务优先级的调度
    /// </summary>
    public interface IJobPriorityScheduleConfig
    {
        double GetTimeInterval();
    }
}
