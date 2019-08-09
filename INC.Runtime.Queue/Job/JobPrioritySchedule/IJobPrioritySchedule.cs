using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INC.Runtime.Queue
{
    public interface IJobPrioritySchedule
    {
        IJobPriorityScheduleConfig Config { get; set; }

        void Start();
        
        void Stop();
    }
}
