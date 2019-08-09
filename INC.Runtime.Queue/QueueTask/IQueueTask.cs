using INC.Runtime.Queue.Delegate;
using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public enum QueueTaskMode
    {
        Thread = 1,
        Task = 2
    }

    public interface IQueueTask : IDisposable
    {
        QueueTaskMode Mode { get; }
        /// <summary>
        /// configuration
        /// </summary>
        IQueueTaskConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        JobBase CurrentJob { get; set; }


        #region Event

        event EventHandler<EventArgs> OnTaskBeginExecuted;
        event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskBeginEventHander OnTaskBegin;
        event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskJobBeginEventHander OnTaskJobBegin;
        event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskJobCompleteEventHander OnTaskJobComplete;
        event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskWakeUpEventHander OnTaskWakeUp;
        event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskCompleteEventHander OnTaskComplete;

        #endregion


        /// <summary>
        /// Run Queue Task
        /// </summary>
        void Run();
    }
}
