using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using INC.Runtime.Queue.Delegate;
using static INC.Runtime.Queue.Delegate.QueueTaskDelegate;
using System.Threading;

namespace INC.Runtime.Queue
{
    public sealed class QueueTask : IQueueTask
    {
        private Task task;
        private int currentDelayTimes = 0;
        private IQueueTaskConfiguration configuration;

        public QueueTask(IQueueTaskConfiguration queueTaskConfiguration)
        {
            this.configuration = queueTaskConfiguration;
        }


        public IQueueTaskConfiguration Configuration => configuration;

        public JobBase CurrentJob { get; set; }

        public event TaskBeginEventHander OnTaskBegin;
        public event TaskJobBeginEventHander OnTaskJobBegin;
        public event TaskJobCompleteEventHander OnTaskJobComplete;
        public event TaskWakeUpEventHander OnTaskWakeUp;
        public event TaskCompleteEventHander OnTaskComplete;

        public void Run()
        {
            this.task = new Task(() =>
            {
                ExecuteJob();
            });

            this.task.Start();
            this.task.ContinueWith((t) =>
            {
                OnTaskCompleted(t);
            }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent);

            this.task.ContinueWith(t =>
            {
                OnTaskExecuteFault(t);
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);
        }

        private void ExecuteJob()
        {
            while (!Interlocked.Equals(currentDelayTimes, configuration.DelayTimes))
            {
                ///rasie task begin evnet
                OnTaskBegin?.Invoke(this, new EventArgs());

                while (this.CurrentJob != null)
                {
                    ///Rasie job complete event
                    OnTaskJobBegin?.Invoke(this, new QueueTaskEventArgs(this.CurrentJob));

                    this.CurrentJob.DoAction();

                    ///Rasie job complete event
                    OnTaskJobComplete?.Invoke(this, new QueueTaskEventArgs(this.CurrentJob));
                    Interlocked.Exchange(ref currentDelayTimes, 0);
                }

                ///Delay task for wait job
                Task.Delay(configuration.TaskDelay).Wait();
                if (OnTaskWakeUp?.Invoke(this, new EventArgs()) == false)
                {
                    Interlocked.Increment(ref currentDelayTimes);
                }
            }
        }

        private void OnTaskCompleted(Task task)
        {
            ///rasie task begin evnet
            OnTaskComplete?.Invoke(this, new QueueTaskEventArgs(this.CurrentJob));
        }

        private void OnTaskExecuteFault(Task task)
        {
            this.CurrentJob.SetFault(task.Exception);
            OnTaskCompleted(task);
        }

        public void Dispose()
        {
            if (task.Status != TaskStatus.Faulted && task.Status != TaskStatus.Canceled)
            {
                try
                {
                    task.Wait();//task throw error.
                }
                catch (Exception)
                {
                }
            }
            task.Dispose();
        }
    }
}
