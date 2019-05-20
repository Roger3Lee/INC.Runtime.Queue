using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using INC.Runtime.Queue.Delegate;

namespace INC.Runtime.Queue
{
    public sealed class Job : JobBase
    {
        private Action action;
        public Job(Action action)
        {
            this.action = action;
        }

        public override void DoAction()
        {
            this.action();
        }
    }

    public sealed class Job<T> : JobBase
    {
        private Action<T> action;
        public Job(T data, Action<T> action)
        {
            this.Data = data;
            this.action = action;
        }

        public T Data { get; }
        
        public override void DoAction()
        {
            this.action(Data);
        }
    }

    public sealed class Job<T1, T2> : JobBase, INotifyJobResultChange<T2>
    {
        private Func<T1, T2> _action;
        private T2 _result;
        public event JobDelegate.JobResultChangeEventHandler<T2> OnResultChange;

        public Job(T1 data, Func<T1, T2> action)
        {
            this.Data = data;
            this._action = action;
        }

        public T1 Data { get; }


        public T2 Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                this.OnResultChange?.Invoke(this, new JobResultChangeEventArgs<T2>() { JobResult = value });

            }
        }

        public override void DoAction()
        {
            this.Result = _action(this.Data);
        }
    }

    public abstract class JobBase
    {
        public JobBase()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public bool IsFault { get; private set; }

        public Exception Exception { get; private set; }
        
        /// <summary>
        /// set fault 
        /// </summary>
        /// <param name="ex"></param>
        public void SetFault(Exception ex)
        {
            this.IsFault = true;
            this.Exception = ex;
        }

        /// <summary>
        /// Execute job action
        /// </summary>
        public abstract void DoAction();

        public Action CompleteCallback { get; set; }
    }
}
