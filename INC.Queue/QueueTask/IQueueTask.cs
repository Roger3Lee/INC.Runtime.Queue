using INC.Queue.Delegate;
using System;
using System.Collections.Generic;
using System.Text;
using static INC.Queue.Delegate.QueueTaskDelegate;

namespace INC.Queue
{
    public interface IQueueTask : IDisposable
    {
        /// <summary>
        /// configuration
        /// </summary>
        IQueueTaskConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        JobBase CurrentJob { get; set; }


        #region Event

        event TaskBeginEventHander OnTaskBegin;
        event TaskJobBeginEventHander OnTaskJobBegin;
        event TaskJobCompleteEventHander OnTaskJobComplete;
        event TaskWakeUpEventHander OnTaskWakeUp;
        event TaskCompleteEventHander OnTaskComplete;

        #endregion


        /// <summary>
        /// Run Queue Task
        /// </summary>
        void Run();
    }
}
