using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using INC.Runtime.Queue.Delegate;
using System.Threading;

namespace INC.Runtime.Queue
{
    public sealed class QueueTask : IQueueTask
    {
        private Task task;
        private Thread _thread;//For Thread Mode
        private int currentDelayTimes = 0;
        private IQueueTaskConfiguration configuration;
        private QueueTaskMode _mode = QueueTaskMode.Thread;

        public QueueTask(IQueueTaskConfiguration queueTaskConfiguration, QueueTaskMode mode = QueueTaskMode.Thread)
        {
            this.configuration = queueTaskConfiguration;
            this._mode = mode;
        }
        public QueueTaskMode Mode
        {
            get { return _mode; }
        }

        public IQueueTaskConfiguration Configuration { get { return configuration; } } 

        public JobBase CurrentJob { get; set; }

        public event EventHandler<EventArgs> OnTaskBeginExecuted;
        public event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskBeginEventHander OnTaskBegin;
        public event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskJobBeginEventHander OnTaskJobBegin;
        public event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskJobCompleteEventHander OnTaskJobComplete;
        public event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskWakeUpEventHander OnTaskWakeUp;
        public event INC.Runtime.Queue.Delegate.QueueTaskDelegate.TaskCompleteEventHander OnTaskComplete;

        public void Run()
        {
            if (_mode == QueueTaskMode.Thread)
            {
                this._thread = new Thread(() =>
                {
                    try
                    {
                        ExecuteJob();
                    }
                    catch (Exception ex)
                    {
                        OnTaskExecuteFault(ex);
                    }
                    finally
                    {
                        OnTaskCompleted();
                    }
                });
                this._thread.IsBackground = true;
                this._thread.Start();
            }
            else
            {
                this.task = new Task(() =>
                {
                    ExecuteJob();
                });

                this.task.Start();
                this.task.ContinueWith((t) =>
                {
                    OnTaskCompleted();
                }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.AttachedToParent);

                this.task.ContinueWith(t =>
                {
                    OnTaskExecuteFault(t.Exception);
                }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);
            }
        }

		private void ExecuteJob()
		{
            this.OnTaskBeginExecuted?.Invoke(this, null);
			while (currentDelayTimes < configuration.DelayTimes)
			{
				///rasie task begin evnet
				if (OnTaskBegin != null)
					OnTaskBegin.Invoke(this, new EventArgs());

				while (this.CurrentJob != null)
				{
					///Rasie job begin event
					if (OnTaskJobBegin != null)
						OnTaskJobBegin.Invoke(this, new QueueTaskEventArgs(this.CurrentJob));

					try
					{
						this.CurrentJob.DoAction();
					}
					catch (Exception ex)
					{
						this.CurrentJob.SetFault(ex);
					}
					finally
					{
						///Rasie job complete event
						if (OnTaskJobComplete != null)
							OnTaskJobComplete.Invoke(this, new QueueTaskEventArgs(this.CurrentJob));
					}

					Interlocked.Exchange(ref currentDelayTimes, 0);
				}

				///Delay task for wait job
				Task.Delay(configuration.TaskDelay).Wait();
				if (OnTaskWakeUp != null && OnTaskWakeUp.Invoke(this, new EventArgs()) == false)
				{
					Interlocked.Increment(ref currentDelayTimes);
				}
			}
		}

        private void OnTaskCompleted()
        {
            ///rasie task begin evnet
            if (OnTaskComplete != null)
                OnTaskComplete.Invoke(this, new QueueTaskEventArgs(this.CurrentJob));
        }

        private void OnTaskExecuteFault(Exception ex)
        {
            if (this.CurrentJob != null)
                this.CurrentJob.SetFault(ex);
            OnTaskCompleted();
        }

        public void Dispose()
        {
            //do nothing
        }
    }
}
